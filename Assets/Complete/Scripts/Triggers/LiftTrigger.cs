using UnityEngine;
using System.Collections;

public class LiftTrigger : MonoBehaviour {

    public bool triggered = false;
    public GameObject cube;
    public float triggerHeight = 2.5F;
    float originalYValue;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            TriggerAnimation(true);
            LiftMovement cubeMove = (LiftMovement)cube.GetComponent(typeof(LiftMovement));

            if (!cubeMove.triggered)
            {
                cubeMove.triggered = true;
                triggered = true;
            }
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            LiftMovement cubeMove = (LiftMovement)cube.GetComponent(typeof(LiftMovement));
            //open door
            if (!cubeMove.triggered)
            {
                triggered = true;
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            LiftMovement cubeMove = (LiftMovement)cube.GetComponent(typeof(LiftMovement));
            TriggerAnimation(false);
            cubeMove.triggered = false;
            triggered = false;
        }
    }

    private void TriggerAnimation(bool on = false)
    {
        if (on)
        {
            triggered = true;
            //make the trigger look pressed like a tile
            transform.localScale += new Vector3(0, -0.29F, 0);
            transform.localPosition += new Vector3(0, -0.08F, 0);
            //door.transform.localPosition += new Vector3(0, -7.1536F, 0);
        }
        else
        {
            triggered = false;
            //make the trigger look unpressed like a tile
            transform.localScale += new Vector3(0, 0.29F, 0);
            transform.localPosition += new Vector3(0, 0.08F, 0);
            //door.transform.localPosition += new Vector3(0, 7.1536F, 0);
        }
    }

}
