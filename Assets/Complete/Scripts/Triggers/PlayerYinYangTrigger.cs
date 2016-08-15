using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class PlayerYinYangTrigger : MonoBehaviour {

    public int playerNo = 1;
    public float speed = 60F;
    private bool triggered = false;
    public float originalYValue;
    private float circleSpeed = 0.2F;
    private float circleSize = 1F;
    private float circleGrowSpeed = 0.3F;
    private float xValue = 0;
    private float zValue = 0;
    public AudioClip tinkshaClip;


    // Use this for initialization
    void Start()
    {
        originalYValue = (transform.position.y);
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "PlayerYinYangTrigger")
        {
            Complete.PlayerMovement playerMovement = other.GetComponentInParent<Complete.PlayerMovement>();

            if (playerMovement.playerNumber == playerNo)
            {
                playerMovement.disabled = true;
                
                 foreach(Collider c in transform.parent.GetComponentsInChildren<Collider> ()) 
                 {
                        c.enabled = false;
                 }
                 triggered = true;
                 playerMovement.triggered = true;
                 AudioSource.PlayClipAtPoint(tinkshaClip, transform.position);
            }    
        }
    }

    void Update()
    {
      
        if (triggered)
        {
            if (playerNo == 2)
            {
                MoveTowardsTarget(originalYValue + 17F);
            }
            else if (playerNo == 1)
            {
                MoveTowardsTarget(originalYValue + 17F);
            }
        }

    }

    //move towards a target at a set speed.
    private void MoveTowardsTarget(float yValue)
    {
        //the speed, in units per second, we want to move towards the target
        float speed = 6F;

        //move door down
        Vector3 currentPosition = this.transform.position;

        xValue = Mathf.Sin(Time.time * circleSpeed) * circleSize;
        zValue = Mathf.Cos(Time.time * circleSpeed) * circleSize;
        circleSize += circleGrowSpeed;

        if (playerNo == 1)
        {
            xValue = -xValue;
            zValue = -zValue;
        }

        Vector3 targetPosition = new Vector3(xValue+28F, yValue, zValue-25F);

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
