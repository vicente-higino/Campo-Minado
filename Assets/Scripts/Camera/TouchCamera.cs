﻿// Just add this script to your camera. It doesn't need any configuration.

using UnityEngine;

public class TouchCamera : MonoBehaviour {
    public Camera camera;


    Vector2?[] oldTouchPositions = {
		null,
		null
	};
	Vector2 oldTouchVector;
	float oldTouchDistance;

    void Start()
    {
            
    }

    void Update() {
        if (!GameObject.Find("GameControl").GetComponent<GameControl>().isOver)
        {
            if (Input.touchCount == 0)
            {
                oldTouchPositions[0] = null;
                oldTouchPositions[1] = null;
            }
            else if (Input.touchCount == 1)
            {
                if (oldTouchPositions[0] == null || oldTouchPositions[1] != null)
                {
                    oldTouchPositions[0] = Input.GetTouch(0).position;
                    oldTouchPositions[1] = null;
                }
                else
                {
                    if (camera.orthographicSize < 200)
                    {
                        Vector2 newTouchPosition = Input.GetTouch(0).position;

                        transform.position += transform.TransformDirection((Vector3)((oldTouchPositions[0] - newTouchPosition) * camera.orthographicSize / camera.pixelHeight * 2f));

                        oldTouchPositions[0] = newTouchPosition;
                    }
                    
                }
            }
            else
            {
                if (oldTouchPositions[1] == null)
                {
                    oldTouchPositions[0] = Input.GetTouch(0).position;
                    oldTouchPositions[1] = Input.GetTouch(1).position;
                    oldTouchVector = (Vector2)(oldTouchPositions[0] - oldTouchPositions[1]);
                    oldTouchDistance = oldTouchVector.magnitude;
                }
                else
                {
                    Vector2 screen = new Vector2(camera.pixelWidth, camera.pixelHeight);

                    Vector2[] newTouchPositions = {
                    Input.GetTouch(0).position,
                    Input.GetTouch(1).position
                };
                    Vector2 newTouchVector = newTouchPositions[0] - newTouchPositions[1];
                    float newTouchDistance = newTouchVector.magnitude;

                    transform.position += transform.TransformDirection((Vector3)((oldTouchPositions[0] + oldTouchPositions[1] - screen) * camera.orthographicSize / screen.y));
                    //transform.localRotation *= Quaternion.Euler(new Vector3(0, 0, Mathf.Asin(Mathf.Clamp((oldTouchVector.y * newTouchVector.x - oldTouchVector.x * newTouchVector.y) / oldTouchDistance / newTouchDistance, -1f, 1f)) / 0.0174532924f));
                    camera.orthographicSize *= oldTouchDistance / newTouchDistance;
                    transform.position -= transform.TransformDirection((newTouchPositions[0] + newTouchPositions[1] - screen) * camera.orthographicSize / screen.y);

                    oldTouchPositions[0] = newTouchPositions[0];
                    oldTouchPositions[1] = newTouchPositions[1];
                    oldTouchVector = newTouchVector;
                    oldTouchDistance = newTouchDistance;
                }
            }
        }
		
	}
}
