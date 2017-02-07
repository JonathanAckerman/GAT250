using UnityEngine;
using System.Collections;

public class BulletLogic : MonoBehaviour {
    public float decayTime;
    float decayTimer = 0.0f;
    public ResourceColor bulletColor;

    // Use this for initialization
    void Start()
    {
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

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.name == "Wall")
        {
            // bounce off wall
        }
    }
}
