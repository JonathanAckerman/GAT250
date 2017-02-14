using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ChargeBar : MonoBehaviour {
    ResourceColor resColor;
    public Text chargeText;
    public Image chargeBackground;
    public GameObject barRef;
    public GameObject playerRef;

	// Use this for initialization
	void Start ()
    {
        SetBarColor();
        SetBarText();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (playerRef.GetComponent<Inventory>().GetTotal() == 0)
        {
            barRef.SetActive(false);
        }
        else
        {
            SetBarColor();
            SetBarText();
        }
    }

    void SetBarColor()
    {
        resColor = playerRef.GetComponent<CharacterController>().curColorSelection;
        switch (resColor)
        {
            case ResourceColor.Red:
                chargeBackground.GetComponent<Image>().color = Color.red;
                break;
            case ResourceColor.Green:
                chargeBackground.GetComponent<Image>().color = Color.green;
                break;
            case ResourceColor.Blue:
                chargeBackground.GetComponent<Image>().color = Color.blue;
                break;
        }
    }

    void SetBarText()
    {
        chargeText.text = playerRef.GetComponent<CharacterController>().GetChargeAmount().ToString() + "/" + playerRef.GetComponent<Inventory>().GetTotal().ToString();
    }
}
