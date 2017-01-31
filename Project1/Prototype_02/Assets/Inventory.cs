using UnityEngine;
using System.Collections;

public class Inventory : MonoBehaviour {
    int total = 0;

	// Use this for initialization
	void Start ()
    {
	}

    void OnEnable()
    {
        CharacterController.CampCallbacks += IncreaseTotal;
    }

    void OnDisable()
    {
        CharacterController.CampCallbacks -= IncreaseTotal;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (total <= 0 && !gameObject.GetComponent<CharacterController>().attackOrderActive)
        {
            gameObject.GetComponent<SpriteRenderer>().color = new Vector4(1, 1, 1, 1);
        }
	}

    void IncreaseTotal(int amount)
    {
        total += amount;
        if (total > 0)
        {
            gameObject.GetComponent<SpriteRenderer>().color = new Vector4(0, 1, 0, 1);
        }
    }

    public int GetTotal()
    {
        return total;
    }

    public void ShotOrb()
    {
        total -= 1;
    }
}
