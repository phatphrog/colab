using UnityEngine;
using System.Collections;

public class PurpleTileTrigger : MonoBehaviour {

    public bool triggered = false;
    public GameObject door;

	void OnTriggerEnter (Collider other)
    {
        if(other.tag == "Player")
        {
            TriggerAnimation(true);
            PurpleDoorMovement doorMove = (PurpleDoorMovement)door.GetComponent(typeof(PurpleDoorMovement));

            if (!doorMove.Triggered())
            {
                TriggerOn();
            }
        }
    }

    void OnTriggerStay (Collider other)
    {
        if (other.tag == "Player")
        {
            //TriggerAnimation(true);
            PurpleDoorMovement doorMove = (PurpleDoorMovement)door.GetComponent(typeof(PurpleDoorMovement));
            //open door
            if (!doorMove.Triggered())
            {
                TriggerOn();
            }
        }
    }

    void OnTriggerExit (Collider other)
    {
        if (other.tag == "Player")
        {
            TriggerAnimation(false);
            TriggerOff();
        }
    }

    private void TriggerOn()
    {

        PurpleDoorMovement doorMove = (PurpleDoorMovement)door.GetComponent(typeof(PurpleDoorMovement));
        if(this.tag == "Trigger1")
        {
            doorMove.trigger1 = true;
        } else
        {
            doorMove.trigger2 = true;
        }
        
    }

    private void TriggerOff()
    {
        PurpleDoorMovement doorMove = (PurpleDoorMovement)door.GetComponent(typeof(PurpleDoorMovement));
        if (this.tag == "Trigger1")
        {
            doorMove.trigger1 = false;
        }
        else
        {
            doorMove.trigger2 = false;
        }

    }

    private void TriggerAnimation(bool on = false)
    {
        if(on)
        {
            triggered = true;
            //make the trigger look pressed like a tile
            transform.localScale += new Vector3(0, -0.29F, 0);
            transform.localPosition += new Vector3(0, -0.08F, 0);
            //door.transform.localPosition += new Vector3(0, -7.1536F, 0);
        } else
        {
            triggered = false;
            //make the trigger look unpressed like a tile
            transform.localScale += new Vector3(0, 0.29F, 0);
            transform.localPosition += new Vector3(0, 0.08F, 0);
            //door.transform.localPosition += new Vector3(0, 7.1536F, 0);
        }
    }
}
