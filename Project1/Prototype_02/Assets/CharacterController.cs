using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CharacterController : MonoBehaviour {
    public bool attackOrderActive = false;
    bool issuedAttack = false;
    Vector3 attackTarget;
    public Camera cam;
    int score = 0;
    ResourceColor curColorSelection = ResourceColor.Red;
    public Text scoreText;
    public Image spellIcon1;

    // holy fuck look at how bloated this file is getting kill me now pls
    public Texture2D defaultCursor;
    public Texture2D attackCursor;

    public GameObject bulletPrefab;
    public Transform bulletSpawn;
    public float bulletMoveSpeed;

    bool isInsideCamp = false;
    float collectionTimer = 0.0f;
    NeutralCreep CurCreep;

    // Events for player
    public delegate void CampDetection(int amount);
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
                if (GetComponent<Inventory>().GetTotal() > 0)
                {
                    //gameObject.GetComponent<SpriteRenderer>().color = new Vector4(0, 1, 0, 1);
                }
                else
                {
                    //gameObject.GetComponent<SpriteRenderer>().color = new Vector4(1, 1, 1, 1);
                }
            }
            ////////////////////////
            // Attack
            /////////////////////////
            if (Input.GetKeyDown(KeyCode.A))
            {
                attackOrderActive = true;
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
                    CampCallbacks(CurCreep.resourceAmount);
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
        int curTotal = inventory.GetTotal();
        if (curTotal > 0)
        {
            GameObject bullet = (GameObject)Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);
            bullet.GetComponent<BulletLogic>().bulletColor = curColorSelection;
            Vector3 dir = target - bulletSpawn.position;
            dir.z = bulletSpawn.position.z;
            bullet.GetComponent<Rigidbody2D>().velocity = dir.normalized * bulletMoveSpeed;
            inventory.ShotOrb();
            //gameObject.GetComponent<SpriteRenderer>().color = new Vector4(0, 1, 0, 1);
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

    public void MadeShot()
    {
        score += 1;
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
}
