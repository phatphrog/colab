using UnityEngine;
using System.Collections;

public class BridgeTrigger : MonoBehaviour {

    public bool triggered = false;
    public GameObject cube;
    public float triggerHeight = 2.5F;
    public float triggerTime = 0;
    float originalYValue;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "BlueObject")
        {
            LiftMovement cubeMove = (LiftMovement)cube.GetComponent(typeof(LiftMovement));

            if (triggerTime > 0)
            {
                //wait 3 seconds then close door
                StartCoroutine(CancelTrigger(other.gameObject, triggerTime));
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

}
