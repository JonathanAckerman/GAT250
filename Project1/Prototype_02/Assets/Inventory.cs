using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Inventory : MonoBehaviour {
    int total = 0;
    public Text inventoryText;

	// Use this for initialization
	void Start ()
    {
        SetInventoryText();
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
            //gameObject.GetComponent<SpriteRenderer>().color = new Vector4(1, 1, 1, 1);
        }
	}

    public void IncreaseTotal(int amount)
    {
        total += amount;
        SetInventoryText();
    }

    public int GetTotal()
    {
        return total;
    }

    public void ShotOrb(int charge)
    {
        total -= charge;
        SetInventoryText();
    }

    void SetInventoryText()
    {
        inventoryText.text = "Shots Left: " + total.ToString();
    }
}
