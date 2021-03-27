using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TrackCheckpoints : MonoBehaviour
{
    private List<CheckPoint> checkpointList;
    [SerializeField] private List<Transform> carTransformList;
    private List<int> nextCheckpointIndexList;
    [SerializeField] private GameObject checkPointText;
    private void Awake()
    {
        Transform checkpointsTransform = this.transform;
        checkpointList = new List<CheckPoint>();
        foreach (Transform checkpointSingleTransform in checkpointsTransform)
        {
            CheckPoint checkpoint = checkpointSingleTransform.GetComponent<CheckPoint>();
            checkpoint.SetTrackCheckpoints(this);
            checkpointList.Add(checkpoint);
        }
        nextCheckpointIndexList = new List<int>();
        foreach (Transform carTransform in carTransformList)
        {
            nextCheckpointIndexList.Add(0);
        }
    }
    
    public void CarThroughCheckpoint(CheckPoint checkpoint, Transform carTransform)
    {
        CarAgent carAgent = carTransform.GetComponent<CarAgent>();
        int nextCheckpointIndex = nextCheckpointIndexList[carTransformList.IndexOf(carTransform)];
        if (checkpointList.IndexOf(checkpoint) == nextCheckpointIndex) //correct check point
        {
            Debug.Log("correct");
            carAgent.AddReward(1f);
            nextCheckpointIndex = (nextCheckpointIndex + 1) % checkpointList.Count;
            nextCheckpointIndexList[carTransformList.IndexOf(carTransform)] = nextCheckpointIndex;
            checkPointText.GetComponent<TMP_Text>().text = "Checkpoints: " + nextCheckpointIndex.ToString();
            carAgent.resetInnerTimer();
        }
        else //wrong check point
        {
            carAgent.AddReward(-1f);
            carAgent.EndEpisode();
        }
    }

    public void ResetCheckpoints(Transform carTransform)
    {
        nextCheckpointIndexList[carTransformList.IndexOf(carTransform)] = 0;
    }

    public CheckPoint GetNextCheckpoint(Transform carTransform)
    {
        int nextCheckpointIndex =  nextCheckpointIndexList[carTransformList.IndexOf(carTransform)];
        return checkpointList[nextCheckpointIndex];
    }
}
