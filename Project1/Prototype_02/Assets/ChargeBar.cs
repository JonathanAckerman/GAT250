using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ChargeBar : MonoBehaviour {
    public ResourceColor Color;
    public Text chargeText;
    public Image chargeBackground;
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
        SetBarColor();
        SetBarText();
    }

    void SetBarColor()
    {

    }

    void SetBarText()
    {
        chargeText.text = playerRef.GetComponent<CharacterController>().GetChargeAmount().ToString() + "/" + playerRef.GetComponent<Inventory>().GetTotal().ToString();
    }
}
