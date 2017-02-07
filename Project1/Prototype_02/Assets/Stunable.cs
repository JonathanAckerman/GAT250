using UnityEngine;
using System.Collections;

public class Stunable : MonoBehaviour {
    bool isStunned = false;
    float stunTimer;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (stunTimer > 0.0)
        {
            stunTimer -= Time.deltaTime;
        }
        else
        {
            isStunned = false;
        }
	}

    public void StunTarget(GameObject target, float duration)
    {
        if (target.GetComponent<Stunable>() != null)
        {
            stunTimer = duration;
            target.GetComponent<Stunable>().isStunned = true;
        }
    }

    public bool GetStatus()
    {
        return isStunned;
    }
}
