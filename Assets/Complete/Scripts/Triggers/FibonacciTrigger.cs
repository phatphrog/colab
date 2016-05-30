using UnityEngine;
using System.Collections;

public class FibonacciTrigger : MonoBehaviour {

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Complete.PlayerMovement script = other.GetComponent<Complete.PlayerMovement>();
            if (script.scaled == false && script.playerNumber == 1 && script.explode == false)
            {
                script.startTime = Time.time;
                script.currentPos = other.transform.position;
                script.endPos = new Vector3(-7, 0, -17);
                script.explode = true;

            }
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            Complete.PlayerMovement script = other.GetComponent<Complete.PlayerMovement>();
            if (script.scaled == false && script.playerNumber == 1 && script.explode == false)
            {
                script.startTime = Time.time;
                script.currentPos = other.transform.position;
                script.endPos = new Vector3(-7, 0, -17);
                //player stuck
                script.explode = true;
            }
        }
    }
}
