using UnityEngine;
using System.Collections;

public class PurpleDoorMovement : MonoBehaviour {

    public bool trigger1 = false;
    public bool trigger2 = false;
    public float originalYValue;

    // Use this for initialization
    void Start () {
        originalYValue = (this.transform.position.y);
    }
	
	// Update is called once per frame
	void Update () {
	    if(Triggered())
        {
            MoveTowardsTarget(-originalYValue);
        } else if (!Triggered())
        {
            MoveTowardsTarget(originalYValue);
        }
	}

    public bool Triggered()
    {
        if (trigger1 || trigger2)
        {
            return true;
        } 
            return false;
    }

    //move towards a target at a set speed.
    private void MoveTowardsTarget(float yValue)
    {
        //the speed, in units per second, we want to move towards the target
        float speed = 4.2F;

        //move door down
        Vector3 currentPosition = this.transform.position;
        Vector3 targetPosition = new Vector3(currentPosition.x, yValue, currentPosition.z);

        //first, check to see if we're close enough to the target
        if (Vector3.Distance(currentPosition, targetPosition) > .1f)
        {
            Vector3 directionOfTravel = targetPosition - currentPosition;
            //now normalize the direction, since we only want the direction information
            directionOfTravel.Normalize();
            //scale the movement on each axis by the directionOfTravel vector components

            this.transform.Translate(
                (directionOfTravel.x * speed * Time.deltaTime),
                (directionOfTravel.y * speed * Time.deltaTime),
                (directionOfTravel.z * speed * Time.deltaTime),
                Space.World);
        }
    }
}
