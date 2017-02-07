using UnityEngine;
using System.Collections;

public class BasicAI : MonoBehaviour {
    public GameObject playerRef;
    public GameObject hoopRef;
    float distToPlayer;
    public float moveSpeed; //used for shuffle but when i rework the AI this shit is outie 5000
    public float aggroRange;
    public float deaggroRange;
    public float minDist;
    bool shouldChase = false;
    // true = shuffle right, false = shuffle left
    bool shuffleRight = true;
    float shuffleTime = 1.0f;
    float shuffleTimer = 0.0f;
    public float ShoveSpeed;
    public float PushBackDist;
    public float StunDuration;

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
        Vector3 halfDistVec = new Vector3(hoopToPlayer.x * 0.5f, hoopToPlayer.y * 0.5f, hoopToPlayer.z);
        distToPlayer = (playerTransform.position - transform.position).magnitude;
	    if (distToPlayer < aggroRange)
        {
            shouldChase = true;
        }
        if (distToPlayer > deaggroRange) //|| distToPlayer < minDist) for minDist ie allowplayer to pass or avoid getting shoved
        {
            shouldChase = false;
        }
        if (shouldChase)
        {
            gameObject.GetComponent<Moveable>().SetDestinationWithLook(hoopTransform.position + halfDistVec, hoopTransform.position + hoopToPlayer);
            Shuffle();
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
        if (other.gameObject.tag == "Player")
        {
            Vector3 shoveTarget = (other.transform.position - transform.position).normalized;
            shoveTarget.z = -1.0f;
            shoveTarget.Scale(new Vector3(PushBackDist, PushBackDist, 0));
            Moveable mover = other.gameObject.GetComponent<Moveable>();
            mover.SetDestination(other.transform.position + shoveTarget, false);
            mover.SetTemporarySpeed(ShoveSpeed, StunDuration);
            other.GetComponent<Stunable>().StunTarget(other.gameObject, StunDuration);
        }
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
