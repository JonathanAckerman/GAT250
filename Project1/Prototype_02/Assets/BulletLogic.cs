using UnityEngine;
using System.Collections;

public class BulletLogic : MonoBehaviour {
    public float decayTime;
    float decayTimer = 0.0f;
    public ResourceColor bulletColor;
    public GameObject playerRef;
    int playerCollisionCounter = 0;
    public int ballSize;

    // Use this for initialization
    void Start()
    {
        playerRef = GameObject.FindGameObjectWithTag("Player");
        Physics2D.IgnoreCollision(gameObject.GetComponent<CircleCollider2D>(), playerRef.GetComponent<Collider2D>());
        switch (bulletColor)
        {
            case ResourceColor.Red:
                gameObject.GetComponent<SpriteRenderer>().color = Color.red;
                break;
            case ResourceColor.Green:
                gameObject.GetComponent<SpriteRenderer>().color = Color.green;
                break;
            case ResourceColor.Blue:
                gameObject.GetComponent<SpriteRenderer>().color = Color.blue;
                break;
        }
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

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "BallCollector")
        {
            if (playerCollisionCounter > 0)
            {
                Destroy(other.gameObject);
                playerRef.GetComponent<Inventory>().IncreaseTotal(ballSize);
            }
            else
            {
                ++playerCollisionCounter;
            }
        }
    }
}
