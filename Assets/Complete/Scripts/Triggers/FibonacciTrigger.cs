using UnityEngine;
using System.Collections;

public class FibonacciTrigger : MonoBehaviour {

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Complete.PlayerMovement script = other.GetComponent<Complete.PlayerMovement>();
            if (script.scaled == false && script.m_PlayerNumber == 1)
            {
                //player stuck
                script.explode = true;

            }
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            Complete.PlayerMovement script = other.GetComponent<Complete.PlayerMovement>();
            if (script.scaled == false && script.m_PlayerNumber == 1)
            {
                //player stuck
                script.explode = true;
            }
        }
    }
}
