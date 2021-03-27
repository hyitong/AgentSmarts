using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RayPerceptionSensor : MonoBehaviour
{
    [Header("Sensors")]
    public float sensorLength = 35f;
    public Vector3 frontSensorPosition = new Vector3(0f, 0.2f, 2f);
    public float frontSideSensorPosition = 0.8f;
    public float frontSensorInnerAngle = 12f;
    public float frontSensorAngle = 30f;
    public float sensorMidCenter;
    public float sensorMidLeft;
    public float sensorMidRight;
    public float sensorAngleLeft;
    public float sensorAngleRight;

    public Text sensorText;

    private void Sensors()
    {
        RaycastHit hit;
        Vector3 sensorStartingPos = transform.position;
        sensorStartingPos += transform.forward * frontSensorPosition.z;
        sensorStartingPos += transform.up * frontSensorPosition.y;

        String midcenter = sensorLength.ToString("0.00");
        String midleft = sensorLength.ToString("0.00");
        String midright = sensorLength.ToString("0.00");
        String angleleft = sensorLength.ToString("0.00");
        String angleright = sensorLength.ToString("0.00");

        this.sensorMidCenter = this.sensorLength;
        this.sensorMidLeft = this.sensorLength;
        this.sensorMidRight = this.sensorLength;
        this.sensorAngleLeft = this.sensorLength;
        this.sensorAngleRight = this.sensorLength;

        // front center sensor
        if (Physics.Raycast(sensorStartingPos, transform.forward, out hit, sensorLength))
        {
            Debug.DrawLine(sensorStartingPos, hit.point, Color.red);
            midcenter = hit.distance.ToString("0.00");
            this.sensorMidCenter = hit.distance;
        }
        else
        {
            Debug.DrawRay(sensorStartingPos, transform.forward * sensorLength, Color.white);
        }

        // front right sensor
        sensorStartingPos += transform.right * frontSideSensorPosition;
        if (Physics.Raycast(sensorStartingPos, Quaternion.AngleAxis(frontSensorInnerAngle, transform.up) * transform.forward, out hit, sensorLength))
        {
            Debug.DrawLine(sensorStartingPos, hit.point, Color.red);
            midright = hit.distance.ToString("0.00");
            this.sensorMidRight = hit.distance;
        }
        else
        {
            Debug.DrawRay(sensorStartingPos, Quaternion.AngleAxis(frontSensorInnerAngle, transform.up) * transform.forward * sensorLength, Color.white);
        }

        // front right angle sensor
        if (Physics.Raycast(sensorStartingPos, Quaternion.AngleAxis(frontSensorAngle, transform.up) * transform.forward, out hit, sensorLength))
        {
            Debug.DrawLine(sensorStartingPos, hit.point, Color.red);
            angleright = hit.distance.ToString("0.00");
            this.sensorAngleRight = hit.distance;
        }
        else
        {
            Debug.DrawRay(sensorStartingPos, Quaternion.AngleAxis(frontSensorAngle, transform.up) * transform.forward * sensorLength, Color.white);
        }

        // front left sensor
        sensorStartingPos -= transform.right * frontSideSensorPosition * 2;
        if (Physics.Raycast(sensorStartingPos, Quaternion.AngleAxis(-frontSensorInnerAngle, transform.up) * transform.forward, out hit, sensorLength))
        {
            Debug.DrawLine(sensorStartingPos, hit.point, Color.red);
            midleft = hit.distance.ToString("0.00");
            this.sensorMidLeft = hit.distance;
        }
        else
        {
            Debug.DrawRay(sensorStartingPos, Quaternion.AngleAxis(-frontSensorInnerAngle, transform.up) * transform.forward * sensorLength, Color.white);
        }

        // front left angle sensor
        if (Physics.Raycast(sensorStartingPos, Quaternion.AngleAxis(-frontSensorAngle, transform.up) * transform.forward, out hit, sensorLength))
        {
            Debug.DrawLine(sensorStartingPos, hit.point, Color.red);
            angleleft = hit.distance.ToString("0.00");
            this.sensorAngleLeft = hit.distance;
        }
        else
        {
            Debug.DrawRay(sensorStartingPos, Quaternion.AngleAxis(-frontSensorAngle, transform.up) * transform.forward * sensorLength, Color.white);
        }

        sensorText.text = "Sensor Reading: \n    MiddleCenter    " + midcenter + "\n    MiddleLeft    " + midleft + "\n    MiddleRight    " + midright + "\n    AngleLeft    " + angleleft + "\n    AngleRight    " + angleright;

    }
}
