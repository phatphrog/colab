using UnityEngine;
using System.Collections;

public class BridgeTrigger : MonoBehaviour {

    public bool triggered = false;
    public GameObject cube;
    public float triggerHeight = 2.5F;
    public float triggerTime = 0;
    float originalYValue;
    public AudioSource doorMoveAudio;
    public AudioClip doorMoveClip;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "BlueObject")
        {
            LiftMovement cubeMove = (LiftMovement)cube.GetComponent(typeof(LiftMovement));

            if (triggerTime > 0)
            {
                //wait 3 seconds then close door
                StartCoroutine(CancelTrigger(other.gameObject, triggerTime));
                StartCoroutine(PlaySound(triggerTime));
            }

            if (!cubeMove.triggered)
            {
                cubeMove.triggered = true;
                triggered = true;
            }
        }
    }

  
    IEnumerator CancelTrigger(GameObject gobj, float delay)
    {
        float timer = 0.0F;
        while (timer < delay + 1)
        {
            if (timer > delay)
            {
                LiftMovement cubeMove = (LiftMovement)cube.GetComponent(typeof(LiftMovement));
                cubeMove.triggered = false;
                triggered = false;
            }
            yield return new WaitForEndOfFrame();
            timer += Time.deltaTime;
        }
    }

    IEnumerator PlaySound( float delay)
    {
        float timer = 0.0F;
        bool soundPlayed = false;
        while (timer < delay*1.4F + 1)
        {
            if (timer > delay*1.4F)
            {
                if (!soundPlayed)
                {
                    //play the scraper sound when door opens/closes
                    doorMoveAudio.clip = doorMoveClip;
                    doorMoveAudio.Play();
                    soundPlayed = true;
                }
            }
            yield return new WaitForEndOfFrame();
            timer += Time.deltaTime;
        }
    }

}
