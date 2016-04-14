using UnityEngine;
using System.Collections;

public class PlayerCollider : MonoBehaviour {

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "FibonnacciBarrier")
        {
            Complete.PlayerMovement script = GetComponent<Complete.PlayerMovement>();
            if(script.scaled == true && script.m_PlayerNumber == 1)
            {
                collision.collider.isTrigger = true;
            } 
        }
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "FibonnacciBarrier")
        {
            Complete.PlayerMovement script = GetComponent<Complete.PlayerMovement>();
            if (!script.scaled)
            {
                collision.GetComponent<Collider>().isTrigger = false;
            }
        }
    }
    

    void OnCollisionExit(Collision collision)
    {
        GetComponent<Rigidbody>().velocity = Vector3.zero;
    }
}
