using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private Transform target;
    [SerializeField] private float distanceToTarget = 1.5f;
    [SerializeField] private float distanceStep = 0.1f;
    [SerializeField] private float minDistanceFromTarget = 0.0f;
    [SerializeField] private float maxDistanceFromTarget = 3.0f;
    private Vector3 targetPoint;
    private Vector3 previousPosition;

    public void Refocus(Vector3 position)
    {
        targetPoint = position;  
        SetCameraPosition(0, 0);
    }

    void SetCameraPosition(float rotationAroundXAxis, float rotationAroundYAxis) 
    {
        cam.transform.position = targetPoint;

        cam.transform.Rotate(new Vector3(1, 0, 0), rotationAroundXAxis);
        cam.transform.Rotate(new Vector3(0, 1, 0), rotationAroundYAxis, Space.World); // <— This is what makes it work!

        cam.transform.Translate(new Vector3(0, 0, -distanceToTarget));
    }
    // Start is called before the first frame update
    void Start()
    {
        targetPoint = target.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        bool shouldRefreshCam = false;
        float rotationAroundXAxis = 0f;
        float rotationAroundYAxis = 0f;
        if (Input.mouseScrollDelta.y != 0) 
        {
            distanceToTarget += distanceStep * Input.mouseScrollDelta.y;
            if (distanceToTarget < minDistanceFromTarget) 
                distanceToTarget = minDistanceFromTarget;
            if (distanceToTarget > maxDistanceFromTarget)
                distanceToTarget = maxDistanceFromTarget;
            shouldRefreshCam = true;
        }   
        if (Input.GetMouseButtonDown(0))
        {
            previousPosition = cam.ScreenToViewportPoint(Input.mousePosition);
        }
        else if (Input.GetMouseButton(0))
        {
            Vector3 newPosition = cam.ScreenToViewportPoint(Input.mousePosition);
            Vector3 direction = previousPosition - newPosition;
            
            rotationAroundYAxis = -direction.x * 180; // camera moves horizontally
            rotationAroundXAxis = direction.y * 180; // camera moves vertically
            shouldRefreshCam = true;
            previousPosition = newPosition;
        }     

        if (shouldRefreshCam)            
            SetCameraPosition(rotationAroundXAxis, rotationAroundYAxis);
    }
}
