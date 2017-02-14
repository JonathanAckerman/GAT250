using UnityEngine;
using System.Collections;

public class NeutralCreep : MonoBehaviour {
    public float timeToComplete;
    public int resourceAmount;
    public ResourceColor resourceColor;
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
            gameObject.GetComponent<SpriteRenderer>().color = new Vector4(1f, 1f, 1f, 0.4f);
        }
	}

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject != null)
        {
            if (other.gameObject.tag != "Neutral Camp")
            {
                isOccupied = true;
            }
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
