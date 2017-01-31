using UnityEngine;
using System.Collections;

public class BasicAI : MonoBehaviour {
    public GameObject playerRef;
    float distToPlayer;
    public float moveSpeed;
    public float aggroRange;
    public float deaggroRange;
    public float minDist;
    bool shouldChase = false;
	// Use this for initialization
	void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        Vector3 dirVector = playerRef.GetComponent<Transform>().position - transform.position;
        distToPlayer = dirVector.magnitude;
	    if (distToPlayer < aggroRange)
        {
            shouldChase = true;
        }
        if (distToPlayer > deaggroRange || distToPlayer < minDist)
        {
            shouldChase = false;
        }
        if (shouldChase)
        {
            ChasePlayer(dirVector);
        }
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Bullet")
        {
            Destroy(other.gameObject);
        }
    }

    void ChasePlayer(Vector3 dir)
    {
        transform.position += dir.normalized * moveSpeed * Time.deltaTime;
    }
}
