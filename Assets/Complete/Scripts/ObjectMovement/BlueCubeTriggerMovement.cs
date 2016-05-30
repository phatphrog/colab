using UnityEngine;
using System.Collections;

public class BlueCubeTriggerMovement : MonoBehaviour
{

    public bool triggered = false;
    public float originalXValue;

    // Use this for initialization
    void Start()
    {
        originalXValue = (this.transform.position.x);
    }

    // Update is called once per frame
    void Update()
    {
        if (triggered)
        {
            MoveTowardsTarget(originalXValue+5F);
            
            if (Mathf.Abs((transform.position.x - (originalXValue + 5F))) <= 0.3F)
            {
                BlueCubeCollider cubeCollider = (BlueCubeCollider)GetComponent(typeof(BlueCubeCollider));
                cubeCollider.triggered = false;
                triggered = false;
                GetComponent<Renderer>().material.color = new Color(0F, 1F, 0.996F);
            }
        }

    }

    //move towards a target at a set speed.
    private void MoveTowardsTarget(float xValue)
    {
        //the speed, in units per second, we want to move towards the target
        float speed = 1.4F;

        //move door down
        Vector3 currentPosition = this.transform.position;
        Vector3 targetPosition = new Vector3(xValue, currentPosition.y, currentPosition.z);

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
