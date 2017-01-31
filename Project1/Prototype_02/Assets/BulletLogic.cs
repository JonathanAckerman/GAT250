using UnityEngine;
using System.Collections;

public class BulletLogic : MonoBehaviour {
    public float decayTime;
    float decayTimer = 0.0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (decayTimer < decayTime)
        {
            decayTimer += Time.deltaTime;
        }
        else
        {
            Destroy(gameObject);
        }
	}

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.name == "Wall")
        {
            // bounce off wall
        }
    }
}
