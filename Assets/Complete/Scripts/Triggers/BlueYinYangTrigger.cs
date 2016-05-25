using UnityEngine;
using System.Collections;

public class BlueYinYangTrigger : MonoBehaviour {

    public GameObject redSphere;
    public GameObject blueSphere;
    public GameObject redYinYang;
    public AudioSource doorMoveAudio;
    public AudioClip doorMoveClip;
    public bool triggered = false;
    float originalYValue;

    // Use this for initialization
    void Start()
    {
        originalYValue = (this.transform.position.y);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "RedYinYang")
        {
            TriggerAnimation(true);
            RedObjectMovement sphereMovement = (RedObjectMovement)redSphere.GetComponent(typeof(RedObjectMovement));
            BlueObjectMovement blueSphereMovement = (BlueObjectMovement)blueSphere.GetComponent(typeof(BlueObjectMovement));
            RedYinYangTrigger redYinYangTrigger = (RedYinYangTrigger)redYinYang.GetComponent(typeof(RedYinYangTrigger));
            RedObjectCollider redYinYangCollider = (RedObjectCollider)other.GetComponent(typeof(RedObjectCollider));

            //play the scraper sound when door opens/closes
            doorMoveAudio.clip = doorMoveClip;
            doorMoveAudio.Play();

            if (!sphereMovement.triggered)
            {
                sphereMovement.triggered = true;
            }

            if (!blueSphereMovement.triggered)
            {
                blueSphereMovement.triggered = true;
            }

            if (!redYinYangTrigger.triggered)
            {
                redYinYangTrigger.triggered = true;
                redYinYangCollider.triggered = true;
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (triggered)
        {
            MoveTowardsTarget(originalYValue - 1.82F);
        }
        else if (!triggered)
        {
            MoveTowardsTarget(originalYValue);
        }
	}

    private void TriggerAnimation(bool on = false)
    {
        BlueObjectCollider yinYangCollider = (BlueObjectCollider)this.GetComponent(typeof(BlueObjectCollider));
        if (on)
        {
            triggered = true;
            yinYangCollider.triggered = true;
            //move tile down gradually
        }
        else
        {
            triggered = false;
            yinYangCollider.triggered = false;
        }
    }

    //move towards a target at a set speed.
    private void MoveTowardsTarget(float yValue)
    {
        //the speed, in units per second, we want to move towards the target
        float speed = 3;

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
