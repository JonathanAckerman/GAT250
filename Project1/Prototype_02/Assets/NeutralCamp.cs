using UnityEngine;
using System.Collections;

public class NeutralCamp : MonoBehaviour {
    public float timeToComplete;
    public int resourceAmount;
    public float cooldown;
    float cooldownTimer = 0.0f;
    bool isOccupied = false;
    public bool hasResources = false;

    // Use this for initialization
    void Start ()
    {

    }
	
	// Update is called once per frame
	void Update ()
    {
        if (isOccupied && !hasResources)
        {
            gameObject.GetComponent<SpriteRenderer>().color = new Vector4(1, 0, 0, 0.5f);
        }
        else if (!hasResources)
        {
            gameObject.GetComponent<SpriteRenderer>().color = new Vector4(1f, 1f, 1f, 1f);
        }
        if (!isOccupied && !hasResources)
        {
            if (cooldownTimer < cooldown)
            {
                cooldownTimer += Time.deltaTime;
            }
            else if (cooldownTimer >= cooldown && !isOccupied)
            {
                gameObject.GetComponent<SpriteRenderer>().color = new Vector4(0, 1, 0, 1);
                cooldownTimer = 0.0f;
                hasResources = true;
            }
        }
	}

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject != null)
        {
            isOccupied = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject != null)
        {
            isOccupied = false;
        }
    }
}
