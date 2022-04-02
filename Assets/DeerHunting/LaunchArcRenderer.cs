using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class LaunchArcRenderer : MonoBehaviour
{
    LineRenderer lr;

    public int numSegments; //how many segments in arc line

    float velocity;
    float angle;
    float g; //gravity value
    float radianAngle; //angle converted to radians

    void Awake() 
    {
        lr = GetComponent<LineRenderer>();
        g = Mathf.Abs(Physics.gravity.y);
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    //adjusts LR settings for correct arc
    //called by atlatl when spear is loaded
    public void RenderArc(float vel, float arcAngle) 
    {
        velocity = vel;
        angle = arcAngle;
        lr.positionCount = numSegments + 1;
        lr.SetPositions(CalculateArcArray());
    }

    public void ClearArc() {
        lr.positionCount = 0;
    }

    //creates array of positions for arc vertices
    //called by RenderArc()
    Vector3[] CalculateArcArray() 
    {
        Vector3[] arcArray = new Vector3[numSegments + 1];

        radianAngle = Mathf.Deg2Rad * angle;
        float maxDistance = (Mathf.Pow(velocity, 2) * Mathf.Sin(2 * radianAngle)) / g;

        for(int i = 0; i <= numSegments; i++) 
        {
            float t = (float)i / (float)numSegments; //how far from last point
            arcArray[i] = CalculateArcPoint (t, maxDistance);
        }

        return arcArray;
    }


    //calculate position of 1 line vertex
    Vector3 CalculateArcPoint (float t, float maxDistance)
    {
        float distance = t * maxDistance;
        float height = distance * Mathf.Tan(radianAngle) - ((g * Mathf.Pow(distance, 2)) / (2 * Mathf.Pow(velocity * Mathf.Cos(radianAngle), 2)));
        return new Vector3 (distance, height);
    }

}
