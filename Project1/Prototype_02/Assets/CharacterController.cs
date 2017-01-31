using UnityEngine;
using System.Collections;

public class CharacterController : MonoBehaviour {
    float moveSpeed = 5.0f;
    Vector3 newPos = new Vector3();
    Vector3 dir;
    float totalDist = 0.0f;
    float curDist = 0.0f;
    bool arrived = true;
    public bool attackOrderActive = false;
    public Camera cam;
    int score = 0;

    public GameObject bulletPrefab;
    public Transform bulletSpawn;
    public float bulletMoveSpeed;

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
        ///////////////////////
        // Movement
        //////////////////////
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

        ////////////////////////
        // Attack
        /////////////////////////
        if (Input.GetKeyDown(KeyCode.A))
        {
            attackOrderActive = true;
            gameObject.GetComponent<SpriteRenderer>().color = new Vector4(0.2f, 0.2f, 0.9f, 1);
        }
        if (attackOrderActive && Input.GetMouseButtonDown(0))
        {
            Vector3 target = cam.ScreenToWorldPoint(Input.mousePosition);
            ShootAtTarget(target);
            attackOrderActive = false;
        }

        // Neutral Camp Logic
        if (isInsideCamp)
        {
            if (CurCamp.hasResources)
            {
                collectionTimer += Time.deltaTime;
                //print(collectionTimer);
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
    void ShootAtTarget(Vector3 target)
    {
        Inventory inventory = gameObject.GetComponent<Inventory>();
        int curTotal = inventory.GetTotal();
        if (curTotal > 0)
        {
            GameObject bullet = (GameObject)Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);
            Vector3 dir = target - bulletSpawn.position;
            dir.z = bulletSpawn.position.z;
            bullet.GetComponent<Rigidbody2D>().velocity = dir.normalized * bulletMoveSpeed;
            inventory.ShotOrb();
            gameObject.GetComponent<SpriteRenderer>().color = new Vector4(0, 1, 0, 1);
        }
    }

    ////////////////////////////
    /// Collision with camps
    ////////////////////////////
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Neutral Camp")
        {
            CurCamp = other.gameObject.GetComponent<NeutralCamp>();
            isInsideCamp = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Neutral Camp")
        {
            isInsideCamp = false;
            collectionTimer = 0.0f;
        }
    }

    public void MadeShot()
    {
        score += 1;
    }

    public int GetScore()
    {
        return score;
    }
}
