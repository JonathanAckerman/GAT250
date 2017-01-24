using UnityEngine;
using System.Collections;

public class EventManager : MonoBehaviour {
    public delegate void CollectAction();
    public static event CollectAction CollectionEvent;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
