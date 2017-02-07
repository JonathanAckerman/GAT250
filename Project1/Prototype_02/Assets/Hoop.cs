using UnityEngine;
using System.Collections;

public enum ResourceColor { Red, Green, Blue, NUM_COLORS };

public class Hoop : MonoBehaviour {
    public GameObject playerRef;
    public ResourceColor hoopColor;
    public int countBetweenCooldown;
    public float cooldownDuration;
    int cooldownCounter = 0;
    float cooldownTimer = 0.0f;
    bool isOnCooldown = false;
	// Use this for initialization
	void Start ()
    {
        SetCooldown(false);
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (cooldownCounter == countBetweenCooldown && !isOnCooldown)
        {
            cooldownTimer = cooldownDuration;
            SetCooldown(true);
        }
	    if (cooldownTimer > 0.0f)
        {
            cooldownTimer -= Time.deltaTime;
        }
        else if (cooldownTimer <= 0.0f && isOnCooldown)
        {
            SetCooldown(false);
        }
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Bullet")
        {
            if (cooldownCounter < countBetweenCooldown)
            {
                if (other.gameObject.GetComponent<BulletLogic>().bulletColor == hoopColor)
                {
                    // Point scored
                    Destroy(other.gameObject);
                    playerRef.GetComponent<CharacterController>().MadeShot();
                    ++cooldownCounter;

                }
                else
                {
                    // No Point cause wrong color, relevant for @@@FEEDBACK
                    Destroy(other.gameObject);
                }
            }
            else
            {
                // No Point Scored cause on cooldown, relevant for @@@FEEDBACK
                Destroy(other.gameObject);
            }
        }
    }
    void SetCooldown(bool shouldCooldown)
    {
        isOnCooldown = shouldCooldown;
        if (shouldCooldown)
        {
            gameObject.GetComponent<SpriteRenderer>().color = Color.grey;
        }
        else
        {
            switch (hoopColor)
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
            cooldownCounter = 0;
        }
    }
}
