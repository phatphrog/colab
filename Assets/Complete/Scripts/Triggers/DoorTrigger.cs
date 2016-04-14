using UnityEngine;
using System.Collections;

public class DoorTrigger : MonoBehaviour
{

    public bool triggered = false;
    public GameObject door;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            TriggerAnimation(true);
            DoorMovement doorMove = (DoorMovement)door.GetComponent(typeof(DoorMovement));

            if (!doorMove.triggered)
            {
                doorMove.triggered = true;
                triggered = true;
            }
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            DoorMovement doorMove = (DoorMovement)door.GetComponent(typeof(DoorMovement));
            //open door
            if (!doorMove.triggered)
            {
                triggered = true;
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            DoorMovement doorMove = (DoorMovement)door.GetComponent(typeof(DoorMovement));
            TriggerAnimation(false);
            doorMove.triggered = false;
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
