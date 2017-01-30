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
	}

    void IncreaseTotal(int amount)
    {
        total += amount;
        print(total);
    }
}
