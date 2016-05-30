using UnityEngine;
using System.Collections;

public class PlayerCollider : MonoBehaviour {

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "FibonnacciBarrier")
        {
            Complete.PlayerMovement script = GetComponent<Complete.PlayerMovement>();
            if(script.scaled == true && script.playerNumber == 1)
            {
                collision.collider.isTrigger = true;
            } 
        }
    }

    void OnCollisionStay(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Complete.PlayerSpecial specialScript = GetComponent<Complete.PlayerSpecial>();
            specialScript.playerCollision = true;
        }
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "FibonnacciBarrier")
        {
            Complete.PlayerMovement script = GetComponent<Complete.PlayerMovement>();
            if (!script.scaled && script.playerNumber == 1)
            {
                collision.GetComponent<Collider>().isTrigger = false;
            }
        }
    }
    

    void OnCollisionExit(Collision collision)
    {
        GetComponent<Rigidbody>().velocity = Vector3.zero;

        if(collision.gameObject.tag == "Player")
        {
            Complete.PlayerSpecial specialScript = GetComponent<Complete.PlayerSpecial>();
            specialScript.playerCollision = false;
        }
    }
}
