using UnityEngine;
using System.Collections;

public class Hoop : MonoBehaviour {
    public GameObject playerRef;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Bullet")
        {
            // Point scored
            Destroy(other.gameObject);
            playerRef.GetComponent<CharacterController>().MadeShot();
            print(playerRef.GetComponent<CharacterController>().GetScore());
        }
    }
}
