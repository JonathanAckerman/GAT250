using UnityEngine;
using System.Collections;

public class CharacterController : MonoBehaviour {
    float moveSpeed = 5.0f;
    Vector3 newPos = new Vector3();
    public Camera cam;
    // Use this for initialization
    void Start ()
    {
	}
	
	// Update is called once per frame
	void Update ()
    {
        // Mouse Right Click Moves To Position
        Vector3 curPos = gameObject.GetComponent<Transform>().position;
        if (Input.GetMouseButtonDown(1))
        {
            newPos = cam.ScreenToWorldPoint(Input.mousePosition);
            newPos.z = curPos.z;
        }
        gameObject.GetComponent<Transform>().position = Vector3.Lerp(curPos, newPos, Time.deltaTime * moveSpeed);
	
	}
}
