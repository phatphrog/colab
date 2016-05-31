using UnityEngine;
using System.Collections;

public class BlueTileTrigger : MonoBehaviour {

    public bool triggered = false;
    public GameObject cube;
    public GameObject door;
    public float triggerTime = 0;
    public float triggerHeight = 2.5F;
    float originalYValue;
    public AudioSource doorMoveAudio;
    public AudioClip doorMoveClip;
    public bool isLarge = false;

    void OnTriggerEnter (Collider other)
    {
        if (other.tag == "BlueObject")
        {
            //TriggerAnimation(true);
            BlueCubeCollider cubeCollider = (BlueCubeCollider)cube.GetComponent(typeof(BlueCubeCollider));
            DoorMovement doorMove = (DoorMovement)door.GetComponent(typeof(DoorMovement));
            doorMove.triggered = true;

            //if it's large the cube is doing a different thing
            if (cubeCollider.isLarge)
            {
                BlueCubeTriggerMovement cubeMovement = (BlueCubeTriggerMovement)cube.GetComponent(typeof(BlueCubeTriggerMovement));
                isLarge = true;
                cube.GetComponent<Renderer>().material.color = new Color(0F, 0.5F, 1F);
                GetComponent<Renderer>().material.color = new Color(0F, 0.5F, 1F);
                if (cubeMovement)
                {
                    if (!cubeMovement.triggered)
                    {
                        cubeMovement.triggered = true;
                    }
                }
            }
            else //else use the normal movement script
            {
                BlueObjectMovement cubeMovement = (BlueObjectMovement)cube.GetComponent(typeof(BlueObjectMovement));
                TriggerAnimation(true);
                if (cubeMovement)
                {
                    if (!cubeMovement.triggered)
                    {
                        cubeMovement.triggered = true;
                    }
                }
            }
            
            //play the scraper sound when door opens/closes
            doorMoveAudio.clip = doorMoveClip;
            doorMoveAudio.Play();

            if (triggerTime > 0)
            {
                //wait 3 seconds then close door
                StartCoroutine(CancelTrigger(other.gameObject, triggerTime));
            }

            if (cubeCollider)
            {
                if (!cubeCollider.triggered)
                {
                    cubeCollider.triggered = true;
                }
            }

            

        }
    }

    // Update is called once per frame
    void Update()
    {
        if (triggered && !isLarge)
        {
            MoveTowardsTarget(originalYValue + 2F);
        }
        else if (!triggered && !isLarge)
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
            //move tile up gradually
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

    IEnumerator CancelTrigger(GameObject gobj, float delay)
    {
        float timer = 0.0F;
        bool doorTriggered = true;
        while (timer < delay + 1)
        {
            if (timer > delay)
            {
                DoorMovement doorMove = (DoorMovement)door.GetComponent(typeof(DoorMovement));
                doorMove.triggered = false;
                triggered = false;
                if(doorTriggered != triggered)
                {
                    //play the scraper sound when door opens/closes
                    doorMoveAudio.clip = doorMoveClip;
                    doorMoveAudio.Play();
                    doorTriggered = false;
                    GetComponent<Renderer>().material.color = new Color(0F, 1F, 0.996F);
                }
            }
            yield return new WaitForEndOfFrame();
            timer += Time.deltaTime;
        }
    }
}

