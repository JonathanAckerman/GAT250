using UnityEngine;
using System.Collections;

public class NeutralCamp : MonoBehaviour {
    float timeToComplete = 1.0f;
    // Use this for initialization
    void Start ()
    {
        //collider = gameObject.GetComponent<Collider2D>();
        EventManager.CollectionEvent += OnCollectOrb;
    }
	
	// Update is called once per frame
	void Update ()
    {
	}

    void OnCollisionStay2D(Collision2D col)
    {
        if (col.gameObject.name == "Player")
        {
            print(true);
        }
    }

    void OnCollectOrb()
    {
    }
}
