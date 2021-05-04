using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;
using TMPro;

public class CarAgent : Agent
{
    [SerializeField] private TrackCheckpoints trackCheckpoints;
    private ArcadeCar arcadeCar;
    private float innerCheckpointTimer;
    private GameObject scoretext;

    private void Awake()
    {
        arcadeCar = GetComponent<ArcadeCar>();
        scoretext = GameObject.Find("Canvas").transform.Find("Score").gameObject;
    }

    public override void OnEpisodeBegin()
    {
        Debug.Log("Episode begin");
        arcadeCar.Reset(new Vector3(-172.1f, 2.5f, 150.4f));
        trackCheckpoints.ResetCheckpoints(this.transform);
        this.innerCheckpointTimer = 0.0f;
        Score.scoreNum = 0;
        scoretext.GetComponent<TMP_Text>().text = "Stars: " + Score.scoreNum.ToString();
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        Transform checkpoint = trackCheckpoints.GetNextCheckpoint(this.transform).transform;
        float directionDot = Vector3.Dot(transform.forward, checkpoint.forward);
        float distance = Vector3.Distance(transform.position, checkpoint.position);
        sensor.AddObservation(directionDot);
        sensor.AddObservation(distance);
        sensor.AddObservation(arcadeCar.GetSpeed());
    }

    public override void OnActionReceived(ActionBuffers actionBuffers)
    {
        float v = 0f;
        float h = 0f;
        switch (actionBuffers.DiscreteActions[0])
        {
            case 0: v = 0f; break;
            case 1: v = +1f; break;
            case 2: v = -1f; break;
        }
        switch (actionBuffers.DiscreteActions[1])
        {
            case 0: h = 0f; break;
            case 1: h = +1f; break;
            case 2: h = -1f; break;
        }
        arcadeCar.UpdateInput(v, h);
        this.innerCheckpointTimer += Time.deltaTime;
        if (this.innerCheckpointTimer > 200.0f)
        {
            EndEpisode();
        }
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        int forwardAction = 0;
        if (Input.GetKey(KeyCode.UpArrow)) forwardAction = 1;
        if (Input.GetKey(KeyCode.DownArrow)) forwardAction = 2;

        int turnAction = 0;
        if (Input.GetKey(KeyCode.RightArrow)) turnAction = 1;
        if (Input.GetKey(KeyCode.LeftArrow)) turnAction = 2;
        var discreteActionsOut = actionsOut.DiscreteActions;
        discreteActionsOut[0] = forwardAction;
        discreteActionsOut[1] = turnAction;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Equals("Wall"))
        {
            AddReward(-0.5f);
            EndEpisode();
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag.Equals("Wall"))
        {
            AddReward(-0.1f);
        }
    }

    public void resetInnerTimer()
    {
        this.innerCheckpointTimer = 0.0f;
    }
}
