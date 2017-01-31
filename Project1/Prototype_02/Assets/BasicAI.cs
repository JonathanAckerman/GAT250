using UnityEngine;
using System.Collections;

public class BasicAI : MonoBehaviour {
    public GameObject playerRef;
    public GameObject hoopRef;
    float distToPlayer;
    public float moveSpeed;
    public float aggroRange;
    public float deaggroRange;
    public float minDist;
    bool shouldChase = false;
    // true = shuffle right, false = shuffle left
    bool shuffleRight = true;
    float shuffleTime = 1.0f;
    float shuffleTimer = 0.0f;

	// Use this for initialization
	void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        Transform playerTransform = playerRef.GetComponent<Transform>();
        Transform hoopTransform = hoopRef.GetComponent<Transform>();
        Vector3 hoopToPlayer = playerTransform.position - hoopTransform.position;
        hoopToPlayer.Scale(new Vector3(0.5f, 0.5f, 1.0f));
        Vector3 dirVector = (hoopTransform.position + hoopToPlayer) - transform.position;

        distToPlayer = (playerTransform.position - transform.position).magnitude;
	    if (distToPlayer < aggroRange)
        {
            shouldChase = true;
        }
        if (distToPlayer > deaggroRange) //|| distToPlayer < minDist)
        {
            shouldChase = false;
        }
        if (shouldChase)
        {
            ChasePlayer(dirVector);
        }
        //////////////////
        // Shuffle Timer
        //////////////////
        if (shuffleTimer < shuffleTime)
        {
            shuffleTimer += Time.deltaTime;
        }
        else
        {
            shuffleRight = !shuffleRight;
            shuffleTimer = 0.0f;
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
        Shuffle();
    }

    void Shuffle()
    {
        Vector3 forward = playerRef.transform.position - transform.position;
        Vector3 right = Vector3.Cross(forward, new Vector3(0, 0, -1));
        if (shuffleRight)
        {
            transform.position += right.normalized * moveSpeed * Time.deltaTime;
        }
        else
        {
            transform.position += -right.normalized * moveSpeed * Time.deltaTime;
        }
    }
}
