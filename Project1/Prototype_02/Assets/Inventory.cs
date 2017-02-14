using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Inventory : MonoBehaviour {
    int redTotal = 0;
    int greenTotal = 0;
    int blueTotal = 0;
    public Text redText;
    public Text greenText;
    public Text blueText;

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
	}

    public void IncreaseTotal(int amount, ResourceColor color)
    {
        switch (color)
        {
            case ResourceColor.Red:
                redTotal += amount;
                break;
            case ResourceColor.Green:
                greenTotal += amount;
                break;
            case ResourceColor.Blue:
                blueTotal += amount;
                break;
        }
        SetInventoryText();
    }

    public int GetTotal(ResourceColor color)
    {
        switch (color)
        {
            case ResourceColor.Red:
                return redTotal;
            case ResourceColor.Green:
                return greenTotal;
            case ResourceColor.Blue:
                return blueTotal;
        }
        return -1;
    }

    public void ShotOrb(int charge, ResourceColor color)
    {
        switch (color)
        {
            case ResourceColor.Red:
                redTotal -= charge;
                break;
            case ResourceColor.Green:
                greenTotal -= charge;
                break;
            case ResourceColor.Blue:
                blueTotal -= charge;
                break;
        }
        SetInventoryText();
    }

    void SetInventoryText()
    {
        redText.text = redTotal.ToString();
        greenText.text = greenTotal.ToString();
        blueText.text = blueTotal.ToString();
    }
}
