using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;

public class CarAgent : Agent
{
    [SerializeField] private TrackCheckpoints trackCheckpoints;
    private ArcadeCar arcadeCar;
    private float innerCheckpointTimer;

    private void Awake()
    {
        arcadeCar = GetComponent<ArcadeCar>();
    }

    public override void OnEpisodeBegin()
    {
        Debug.Log("Episode begin");
        arcadeCar.Reset(new Vector3(-172.1f, 3.2f, 150.4f));
        trackCheckpoints.ResetCheckpoints(this.transform);
        this.innerCheckpointTimer = 0.0f;
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        //Vector3 checkpointForward = trackCheckpoints.GetNextCheckpoint(this.transform).transform.forward;
        //float directionDot = Vector3.Dot(transform.forward, checkpointForward);
        //sensor.AddObservation(directionDot);
    }

    public override void OnActionReceived(ActionBuffers actionBuffers)
    {
        arcadeCar.UpdateInput(actionBuffers.ContinuousActions[0], actionBuffers.ContinuousActions[1]);
        this.innerCheckpointTimer += Time.deltaTime;
        if (this.innerCheckpointTimer > 300.0f)
        {
            EndEpisode();
        }
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        var continuousActionsOut = actionsOut.ContinuousActions;
        continuousActionsOut[0] = Input.GetAxis("Vertical");
        continuousActionsOut[1] = Input.GetAxis("Horizontal");
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Equals("Wall"))
        {
            AddReward(-0.5f);
            //EndEpisode();
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
