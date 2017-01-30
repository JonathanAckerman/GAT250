using UnityEngine;
using System.Collections;

public class CharacterController : MonoBehaviour {
    float moveSpeed = 5.0f;
    Vector3 newPos = new Vector3();
    Vector3 dir;
    float totalDist = 0.0f;
    float curDist = 0.0f;
    bool arrived = true;
    bool attackOrderActive = false;
    public Camera cam;

    bool isInsideCamp = false;
    float collectionTimer = 0.0f;
    NeutralCamp CurCamp;

    // Events for player
    public delegate void CampDetection(int amount);
    public static event CampDetection CampCallbacks;

    // Use this for initialization
    void Start ()
    {
	}
	
	// Update is called once per frame
	void Update ()
    {
        // Listen for right clicks and set the destination and move increment
        Vector3 curPos = gameObject.GetComponent<Transform>().position;
        if (Input.GetMouseButtonDown(1))
        {
            arrived = false;
            newPos = cam.ScreenToWorldPoint(Input.mousePosition);
            newPos.z = curPos.z;
            dir = newPos - curPos;
            totalDist = dir.magnitude;
            dir.Normalize();
            dir.Scale(new Vector3(moveSpeed, moveSpeed, 1.0f));
            curDist = 0.0f;
        }
        // Check if we have arrived at the destination so we dont over shoot
        if (!arrived)
        {
            gameObject.GetComponent<Transform>().position += dir * Time.deltaTime;
            curDist += dir.magnitude * Time.deltaTime;
        }
        // stop when we reach the destination
        if (curDist >= totalDist)
        {
            arrived = true;
            curDist = 0.0f;
        }
        // Attack
        if (Input.GetKeyDown(KeyCode.A))
        {
            attackOrderActive = true;
            gameObject.GetComponent<SpriteRenderer>().color = new Vector4(1, 0, 0, 1);
        }
        if (attackOrderActive && Input.GetMouseButtonDown(0))
        {
            Vector3 target = cam.ScreenToWorldPoint(Input.mousePosition);
            ShootAtTarget(target);
            attackOrderActive = false;
            gameObject.GetComponent<SpriteRenderer>().color = new Vector4(1, 1, 1, 1);
        }

        // Neutral Camp Logic
        if (isInsideCamp)
        {
            if (CurCamp.hasResources)
            {
                collectionTimer += Time.deltaTime;
                print(collectionTimer);
                if (collectionTimer >= CurCamp.timeToComplete)
                {
                    CurCamp.hasResources = false;
                    collectionTimer = 0.0f;
                    CampCallbacks(CurCamp.resourceAmount);
                }
            }
        }
    }

    ////////////////
    /// Shooting 
    ////////////////
    public GameObject bulletPrefab;
    public Transform bulletSpawn;
    public float bulletMoveSpeed;
    void ShootAtTarget(Vector3 target)
    {
        GameObject bullet = (GameObject)Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);
        bullet.GetComponent<Rigidbody2D>().velocity = target.normalized;
        //print(bullet.GetComponent<Rigidbody2D>().velocity);
    }

    ////////////////////////////
    /// Collision with camps
    ////////////////////////////
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Neutral Camp")
        {
            print("STARTING");
            CurCamp = other.gameObject.GetComponent<NeutralCamp>();
            isInsideCamp = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.name == "Neutral Camp")
        {
            print("RESETTING");
            isInsideCamp = false;
            collectionTimer = 0.0f;
        }
    }
}
