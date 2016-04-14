﻿using UnityEngine;
using System.Collections;

public class PurpleSphereTrigger : MonoBehaviour {

    public bool triggered = false;
    public GameObject sphere;
    public GameObject door;
    float originalYValue;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "PurpleObject")
        {
            TriggerAnimation(true);
            PurpleObjectMovement sphereMovement = (PurpleObjectMovement)sphere.GetComponent(typeof(PurpleObjectMovement));
            DoorMovement doorMove = (DoorMovement)door.GetComponent(typeof(DoorMovement));
            doorMove.triggered = true;

            if (!sphereMovement.triggered)
            {
                sphereMovement.triggered = true;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (triggered)
        {
            MoveTowardsTarget(originalYValue + 2F);
        }
        else if (!triggered)
        {
            MoveTowardsTarget(originalYValue);
        }
    }

    // Use this for initialization
    void Start()
    {
        originalYValue = (this.transform.position.y);
    }

    private void TriggerAnimation(bool on = false)
    {
        if (on)
        {
            triggered = true;
        }
        else
        {
            triggered = false;
        }
    }

    //move towards a target at a set speed.
    private void MoveTowardsTarget(float yValue)
    {
        //the speed, in units per second, we want to move towards the target
        float speed = 5;

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
