using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrajectoryTest : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public Vector3 startingPoint;
    public Vector3 endPoint;
    public Vector3 centerPoint;
    public float Curvature;

    Vector3 relCenter;
    Vector3 aimCenter;

    // Start is called before the first frame update
    void Start()
    {
        // Line Renderer Setup
        lineRenderer.startWidth = 0.3f;
        lineRenderer.endWidth = 0.3f;
        lineRenderer.positionCount = 25;
    }

    // Update is called once per frame
    void Update()
    {
        Rendering();
    }

    void Rendering()
    {
        // Get center of trajectory
        centerPoint = (startingPoint + endPoint) * 0.5f;
        centerPoint -= new Vector3(0f, Curvature, 0f);

        // Get Rel & aIm
        relCenter = startingPoint - centerPoint;
        aimCenter = endPoint - centerPoint;

        // Draw the arc line starting from the launcher
        for (float index = 0.0f, interval = -0.0417f; interval < 1.0f;)
        {
            var theArc = Vector3.Slerp(relCenter, aimCenter, interval += 0.0417f);
            lineRenderer.SetPosition((int)index++, theArc + centerPoint);
        }
    }
}
