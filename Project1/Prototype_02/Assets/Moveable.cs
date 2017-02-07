using UnityEngine;
using System.Collections;

public class Moveable : MonoBehaviour {
    public float moveSpeed;
    public float rotationSpeed;
    float tempSpeed;
    bool isSpeedBuffed = false;
    float tempSpeedTimer = 0.0f;
    float totalDist = 0.0f;
    float curDist = 0.0f;
    bool arrived = true;
    Vector3 destination;
    Vector3 lookPoint;
    bool isDoneRotating = true;
    bool shouldRotate = true;

    // Use this for initialization
    void Start ()
    {
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (tempSpeedTimer > 0.0)
        {
            tempSpeedTimer -= Time.deltaTime;
        }
        else
        {
            isSpeedBuffed = false;
        }
        if (!arrived)
        {
            // This is perhaps the most disgusting thing ive ever put to paper
            if (shouldRotate)
            {
                if (gameObject.GetComponent<BasicAI>() != null)
                {
                    isDoneRotating = AIRotateTowardsPlayer();
                }
                else
                {
                    isDoneRotating = RotateTowardsTarget();
                }
                if (isDoneRotating)
                {
                    if (isSpeedBuffed)
                    {
                        MoveToTarget(destination, tempSpeed);
                    }
                    else
                    {
                        MoveToTarget(destination, moveSpeed);
                    }
                }
            }
            else
            {
                if (isSpeedBuffed)
                {
                    MoveToTarget(destination, tempSpeed);
                }
                else
                {
                    MoveToTarget(destination, moveSpeed);
                }
            }
        }
    }
    /**************************************************************************/
    /* Assumes rotation should occur before moving
    /**************************************************************************/
    public void SetDestination(Vector3 newDestination)
    {
        destination = newDestination;
        shouldRotate = true;
        arrived = false;
    }
    public void SetDestination(Vector3 newDestination, bool shouldRot)
    {
        destination = newDestination;
        shouldRotate = shouldRot;
        arrived = false;
    }

    public void SetDestinationWithLook(Vector3 newDestination, Vector3 lookAt)
    {
        destination = newDestination;
        lookPoint = lookAt;
        arrived = false;
    }

    public void SetTemporarySpeed(float speed, float duration)
    {
        tempSpeed = speed;
        tempSpeedTimer = duration;
        isSpeedBuffed = true;
    }

    public void StopMovement()
    {
        arrived = true;
        curDist = 0.0f;
    }

    void MoveToTarget(Vector3 target, float speed)
    {
        Vector3 dir = target - transform.position;
        totalDist = dir.magnitude;
        dir.Normalize();
        dir.Scale(new Vector3(speed, speed, 1.0f));
        curDist = 0.0f;
        transform.position += dir * Time.deltaTime;
        curDist += dir.magnitude * Time.deltaTime;
        // stop when we reach the destination
        if (curDist >= totalDist)
        {
            arrived = true;
            curDist = 0.0f;
        }
    }

    bool RotateTowardsTarget()
    {
        Vector3 targetFacing = destination - transform.position;
        targetFacing.Normalize();
        float rot_z = Mathf.Atan2(targetFacing.y, targetFacing.x) * Mathf.Rad2Deg;
        Vector3 euler = transform.rotation.eulerAngles;
        float new_z = Mathf.MoveTowardsAngle(euler.z, rot_z, rotationSpeed * Time.deltaTime);
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, new_z));
        if (Mathf.Ceil(new_z) == Mathf.Ceil(rot_z))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public bool RotateTowardsTarget(Vector3 target)
    {
        Vector3 targetFacing = target - transform.position;
        targetFacing.Normalize();
        float rot_z = Mathf.Atan2(targetFacing.y, targetFacing.x) * Mathf.Rad2Deg;
        Vector3 euler = transform.rotation.eulerAngles;
        float new_z = Mathf.MoveTowardsAngle(euler.z, rot_z, rotationSpeed * Time.deltaTime);
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, new_z));
        if (Mathf.Ceil(new_z) == Mathf.Ceil(rot_z))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    /***************************************************/
    /* Rotate function to get AIs to look at the player,
     * not their target b/t the player and hoop
    /***************************************************/
    bool AIRotateTowardsPlayer()
    {
        Vector3 targetFacing = lookPoint - transform.position;
        targetFacing.Normalize();
        float rot_z = Mathf.Atan2(targetFacing.y, targetFacing.x) * Mathf.Rad2Deg;
        Vector3 euler = transform.rotation.eulerAngles;
        float new_z = Mathf.MoveTowardsAngle(euler.z, rot_z, rotationSpeed * Time.deltaTime);
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, new_z));
        if (Mathf.Ceil(new_z) == Mathf.Ceil(rot_z))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
