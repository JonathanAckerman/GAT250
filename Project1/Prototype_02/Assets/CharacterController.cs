using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CharacterController : MonoBehaviour {
    public bool attackOrderActive = false;
    bool issuedAttack = false;
    Vector3 attackTarget;
    float chargeAmount = 1.0f;
    float chargeDelay = 0.5f;
    float chargeTimer = 0.0f;
    float chargeResetTimer = 5.0f;
    float chargeResetDelay = 5.0f;
    bool hasResetCharge = true;

    public Camera cam;
    int score = 0;
    public ResourceColor curColorSelection = ResourceColor.Red;
    public Text scoreText;
    public Image spellIcon1;

    // holy fuck look at how bloated this file is getting kill me now pls
    public Texture2D defaultCursor;
    public Texture2D attackCursor;
    public GameObject barRef;

    public GameObject bulletPrefab;
    public Transform bulletSpawn;
    public float bulletMoveSpeed;

    bool isInsideCamp = false;
    float collectionTimer = 0.0f;
    NeutralCreep CurCreep;

    // Events for player
    public delegate void CampDetection(int amount, ResourceColor color);
    public static event CampDetection CampCallbacks;

    // Use this for initialization
    void Start ()
    {
        SetScoreText();
        SetSpellIcon(1);
    }

    // Update is called once per frame
    void Update()
    {
        ////////////////
        // charge reset
        ////////////////
        if (chargeResetTimer < chargeResetDelay)
        {
            chargeResetTimer += Time.deltaTime;
        }
        else
        {
            if (!hasResetCharge)
            {
                chargeAmount = 1.0f;
                hasResetCharge = true;
                barRef.SetActive(false);
            }
        }
        //////////////////////////////////////////////////////////////////////////
        // Color Selection, side note why the fuck didnt i write an input mgmr :(
        //////////////////////////////////////////////////////////////////////////
        if (Input.GetKeyDown(KeyCode.Q))
        {
            curColorSelection += 1;
            if (curColorSelection == ResourceColor.NUM_COLORS)
            {
                curColorSelection = 0;
            }
            SetSpellIcon(1);
        }
        SetCursorImage();
        ///////////
        // Quit
        ///////////
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
        ///////////////////////
        // Movement
        ///////////////////////
        if (!gameObject.GetComponent<Stunable>().GetStatus())
        {
            if (Input.GetMouseButtonDown(1))
            {
                Vector3 target = cam.ScreenToWorldPoint(Input.mousePosition);
                target.z = transform.position.z;
                gameObject.GetComponent<Moveable>().SetDestination(target);
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                attackOrderActive = false;
                gameObject.GetComponent<Moveable>().StopMovement();
                chargeAmount = 1.0f;
                chargeTimer = 0.0f;
                barRef.SetActive(false);
            }
            ////////////////////////
            // Attack
            /////////////////////////
            if (Input.GetKey(KeyCode.R))
            {
                attackOrderActive = true;
                if (chargeTimer < chargeDelay)
                {
                    chargeTimer += Time.deltaTime;
                }
                else
                {
                    if (chargeAmount <= gameObject.GetComponent<Inventory>().GetTotal(curColorSelection))
                    {
                        chargeAmount += Time.deltaTime;
                        barRef.SetActive(true);
                    }
                }
            }
            if (Input.GetKeyUp(KeyCode.R))
            {
                chargeTimer = 0.0f;
                chargeResetTimer = 0.0f;
                hasResetCharge = false;
            }
            if (attackOrderActive && Input.GetMouseButtonDown(0))
            {
                issuedAttack = true;
                attackOrderActive = false;
                attackTarget = cam.ScreenToWorldPoint(Input.mousePosition);
            }
            if (issuedAttack)
            {
                gameObject.GetComponent<Moveable>().StopMovement();
                bool isDoneRot = gameObject.GetComponent<Moveable>().RotateTowardsTarget(attackTarget);
                if (isDoneRot)
                {
                    ShootAtTarget(attackTarget);
                    issuedAttack = false;
                }
            }
        }
        // Neutral Camp Logic
        if (isInsideCamp)
        {
            if (CurCreep.hasResources)
            {
                collectionTimer += Time.deltaTime;
                if (collectionTimer >= CurCreep.timeToComplete)
                {
                    CurCreep.hasResources = false;
                    collectionTimer = 0.0f;
                    CampCallbacks(CurCreep.resourceAmount, CurCreep.resourceColor);
                }
            }
        }
    }
    ////////////////////
    // Cursor shit
    ////////////////////
    void SetCursorImage()
    {
        if (attackOrderActive)
        {
            Cursor.SetCursor(attackCursor, new Vector2(), CursorMode.Auto);
        }
        else
        {
            Cursor.SetCursor(defaultCursor, new Vector2(), CursorMode.Auto);
        }
    }

    ////////////////
    /// Shooting 
    ////////////////
    void ShootAtTarget(Vector3 target)
    {
        Inventory inventory = gameObject.GetComponent<Inventory>();
        int curTotal = inventory.GetTotal(curColorSelection);
        if (curTotal > 0)
        {
            GameObject bullet = (GameObject)Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);
            bullet.GetComponent<BulletLogic>().ballSize = (int)Mathf.Floor(chargeAmount); // ugh why is this a float
            bullet.GetComponent<BulletLogic>().bulletColor = curColorSelection;
            Vector3 dir = target - bulletSpawn.position;
            dir.z = bulletSpawn.position.z;
            bullet.GetComponent<Rigidbody2D>().velocity = dir.normalized * bulletMoveSpeed;
            inventory.ShotOrb((int)Mathf.Floor(chargeAmount), curColorSelection);
            chargeTimer = 0.0f;
            chargeAmount = 1.0f;
        }
    }

    ////////////////////////////
    /// Collision with camps
    ////////////////////////////
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Creep")
        {
            CurCreep = other.gameObject.GetComponent<NeutralCreep>();
            isInsideCamp = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Creep")
        {
            isInsideCamp = false;
            collectionTimer = 0.0f;
        }
    }
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Wall")
        {
            Moveable move = GetComponent<Moveable>();
            move.StopMovement();
            transform.position += other.GetComponent<Wall>().normal * 0.01f;
        }
    }

    public void MadeShot(int size)
    {
        score += size;
        SetScoreText();
    }

    public int GetScore()
    {
        return score;
    }
    void SetSpellIcon(int spellNum)
    {
        // just end it now, this isnt real programming this is designer bullshit code
        switch (spellNum)
        {
            case 1:
                switch (curColorSelection)
                {
                    case ResourceColor.Red:
                        spellIcon1.color = Color.red;
                        break;
                    case ResourceColor.Green:
                        spellIcon1.color = Color.green;
                        break;
                    case ResourceColor.Blue:
                        spellIcon1.color = Color.blue;
                        break;
                }
                break;
        }
    }
    void SetScoreText()
    {
        scoreText.text = "Score: " + score.ToString();
    }

    public int GetChargeAmount()
    {
        return (int)Mathf.Floor(chargeAmount);
    }
}
