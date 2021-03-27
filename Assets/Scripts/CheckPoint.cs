using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CheckPoint : MonoBehaviour
{
    private TrackCheckpoints trackCheckpoints;

    public void SetTrackCheckpoints(TrackCheckpoints trackCheckpoints) 
    {
        this.trackCheckpoints = trackCheckpoints;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Car")
        {
            trackCheckpoints.CarThroughCheckpoint(this, other.transform);
        }
    }
}
