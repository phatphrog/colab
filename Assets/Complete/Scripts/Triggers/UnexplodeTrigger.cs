using UnityEngine;
using System.Collections;

public class UnexplodeTrigger : MonoBehaviour {

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Complete.PlayerMovement script = other.GetComponent<Complete.PlayerMovement>();
            script.explode = false;
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            Complete.PlayerMovement script = other.GetComponent<Complete.PlayerMovement>();
            script.explode = false;
        }
    }
}
