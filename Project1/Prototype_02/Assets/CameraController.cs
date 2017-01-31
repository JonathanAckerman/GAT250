using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {
    public GameObject playerRef;
    Camera cam;
    public float camMoveSpeed;
    float panLeftX;
    float panRightX;
    float panUpY;
    float panDownY;

	// Use this for initialization
	void Start ()
    {
        cam = gameObject.GetComponent<Camera>();
        panRightX = cam.pixelWidth / 2;
        panLeftX = -panRightX;
        panUpY = cam.pixelHeight / 2;
        panDownY = -panUpY;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.mousePosition.x <= 20.0f)
        {
            transform.position += new Vector3(-1,0,0) * camMoveSpeed * Time.deltaTime;
        }
        if (Input.mousePosition.x >= panRightX * 2 - 20)
        {
            transform.position += new Vector3(1, 0, 0) * camMoveSpeed * Time.deltaTime;
        }
        if (Input.mousePosition.y >= panUpY * 2 - 20)
        {
            transform.position += new Vector3(0, 1, 0) * camMoveSpeed * Time.deltaTime;
        }
        if (Input.mousePosition.y <= 20.0f)
        {
            transform.position += new Vector3(0, -1, 0) * camMoveSpeed * Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.F1))
        {
            Vector3 playerPos = playerRef.transform.position;
            playerPos.z = transform.position.z;
            transform.position = playerPos;
        }
    }
}
