using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class LaunchArcRenderer : MonoBehaviour
{
    Mesh mesh;
    public float meshWidth;

    public int numSegments; //how many segments in arc line

    float g; //gravity value
    float radianAngle; //angle converted to radians

    void Awake() 
    {
        mesh = GetComponent<MeshFilter>().mesh;
        g = Mathf.Abs(Physics.gravity.y);
    }


    //creates mesh showing spear launch arc
    //called by atlatl when spear is pulled back
   public void MakeArcMesh(float velocity, float angle) {
       Debug.Log("MakeArcMesh()");
       Vector3[] arcVerts = CalculateArcArray(velocity, angle); // get mesh arc points
       ClearArc(); //make sure mesh is empty
       Vector3[] vertices = new Vector3[(numSegments + 1) * 2];
       int[] triVerts = new int[numSegments * 12]; //4 tris per segment (2-sided quad)

       for (int i = 0; i <= numSegments; i++) {

           //set vertices (one side at a time)
           vertices[i * 2] = new Vector3(meshWidth * 0.5f, arcVerts[i].y, arcVerts[i].x);
           vertices [i * 2 + 1] = new Vector3(meshWidth * -0.5f, arcVerts[i].y, arcVerts[i].x);

           //set triangles for this segment
           if (i != numSegments) {

               // top of mesh
               triVerts[i * 12] = i * 2;
               triVerts[i * 12 + 1] = triVerts[i * 12 + 4] = i * 2 + 1;
               triVerts[i * 12 + 2] = triVerts[i * 12 + 3] = (i + 1) * 2;
               triVerts[i * 12 + 5] = (i + 1) * 2 + 1;

               // bottom of mesh
               triVerts[i * 12 + 6] = i * 2;
               triVerts[i * 12 + 7] = triVerts[i * 12 + 10] = (i + 1) * 2;
               triVerts[i * 12 + 8] = triVerts[i * 12 + 9] = i * 2 + 1;
               triVerts[i * 12 + 11] = (i + 1) * 2 + 1;
           }

           //set mesh
           mesh.vertices = vertices;
           mesh.triangles = triVerts;
       }
   }

    //clears the mesh
   public void ClearArc() {
       Debug.Log("ClearArc");
       mesh.Clear();
   }


    //creates array of positions for arc sections
    //called by MakeArcMesh()
    Vector3[] CalculateArcArray(float velocity, float angle) 
    {
        Debug.Log("CalculateArcArray()");
        Vector3[] arcArray = new Vector3[numSegments + 1];

        radianAngle = Mathf.Deg2Rad * angle;
        float maxDistance = (Mathf.Pow(velocity, 2) * Mathf.Sin(2 * radianAngle)) / g;

        for(int i = 0; i <= numSegments; i++) 
        {
            float t = (float)i / (float)numSegments; //how far from last point
            arcArray[i] = CalculateArcPoint (t, maxDistance, velocity);
        }

        return arcArray;
    }


    //calculate end position of 1 arc section
    //called by CalculateArcArray()
    Vector3 CalculateArcPoint (float t, float maxDistance, float velocity)
    {
        Debug.Log("CalculateArcPoint");
        float distance = t * maxDistance;
        float height = distance * Mathf.Tan(radianAngle) - ((g * Mathf.Pow(distance, 2)) / (2 * Mathf.Pow(velocity * Mathf.Cos(radianAngle), 2)));
        return new Vector3 (distance, height);
    }

}
