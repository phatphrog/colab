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

    void OnTriggerEnter (Collider other)
    {
        if (other.tag == "BlueObject")
        {
            //TriggerAnimation(true);
            BlueCubeCollider cubeCollider = (BlueCubeCollider)cube.GetComponent(typeof(BlueCubeCollider));
            BlueCubeTriggerMovement cubeTriggerMove = (BlueCubeTriggerMovement)cube.GetComponent(typeof(BlueCubeTriggerMovement));
            DoorMovement doorMove = (DoorMovement)door.GetComponent(typeof(DoorMovement));
            doorMove.triggered = true;

            //Debug.Log(cube.GetComponent<Renderer>().material.color);
            cube.GetComponent<Renderer>().material.color = new Color(0F, 0.5F, 1F);
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

            if (cubeTriggerMove)
            {
                if (!cubeTriggerMove.triggered)
                {
                    cubeTriggerMove.triggered = true;
                }
            }

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
                }
            }
            yield return new WaitForEndOfFrame();
            timer += Time.deltaTime;
        }
    }
}

