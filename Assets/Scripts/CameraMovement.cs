using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {

    private Camera camera;
    public Vector2 startPos;
    public Vector2 direction;
    public bool directionChosen;
    int x;
    int y;
    public float perspectiveZoomSpeed = 0.5f;        // The rate of change of the field of view in perspective mode.
    public float orthoZoomSpeed = 0.5f;
    // Use this for initialization
    void Start () {
        camera = GetComponent<Camera>();
        x = GameObject.Find("GameControl").GetComponent<GameControl>().x;
        y = GameObject.Find("GameControl").GetComponent<GameControl>().y;
        camera.transform.position = new Vector3((13 * x )/ 2, (13 * y) / 2,-1);
	}
	
	// Update is called once per frame
	void Update () {
        if (!GameObject.Find("GameControl").GetComponent<GameControl>().isOver)
        {
            if ((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) && camera.transform.position.y < y * 13)
            {
                camera.transform.position += new Vector3(0, 10);
            }
            if ((Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) && transform.position.y > 100)
            {
                camera.transform.position += new Vector3(0, -10);
            }
            if ((Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) && transform.position.x > 100)
            {
                camera.transform.position += new Vector3(-10, 0);
            }
            if ((Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) && camera.transform.position.x < x * 13)
            {
                camera.transform.position += new Vector3(10, 0);
            }
            if (Input.GetAxis("Mouse ScrollWheel") > 0f && camera.orthographicSize > 10)
            {

                camera.orthographicSize -= 10;
            }
            if (Input.GetAxis("Mouse ScrollWheel") < 0f && camera.orthographicSize < 200)
            {

                camera.orthographicSize += 10;
            }
        }
        
        

    }
}
