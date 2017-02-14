using UnityEngine;
using System.Collections;

public class NeutralCamp : MonoBehaviour {
    public float cooldown;
    float cooldownTimer = 0.0f;
    bool isOccupied = false;
    public NeutralCreep[] CreepList;
    int creepListSize;

    // Use this for initialization
    void Start ()
    {
        creepListSize = CreepList.GetLength(0);
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (!isOccupied)
        {
            if (CountEmptyCreeps() == creepListSize)
            {
                if (cooldownTimer < cooldown)
                {
                    cooldownTimer += Time.deltaTime;
                }
                else if (cooldownTimer >= cooldown)
                {
                    cooldownTimer = 0.0f;
                    for (int i = 0; i < CreepList.GetLength(0); ++i)
                    {
                        CreepList[i].hasResources = true;
                        CreepList[i].gameObject.GetComponent<SpriteRenderer>().color = new Vector4(1, 1, 1, 1);
                    }
                }
            }
        }
    }

    int CountEmptyCreeps()
    {
        int count = 0;
        for (int i = 0; i < CreepList.GetLength(0); ++i)
        {
            if (!CreepList[i].hasResources)
            {
                ++count;
            }
        }

        return count;
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject != null)
        {
            if (other.gameObject.tag != "Creep")
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
