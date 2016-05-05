using UnityEngine;
using System.Collections;

public class ExplodeTrigger : MonoBehaviour {

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Complete.PlayerMovement script = other.GetComponent<Complete.PlayerMovement>();
            //player stuck
            script.startTime = Time.time;
            script.currentPos = other.transform.position;
            script.endPos = new Vector3(39, 3, 26);
            script.explode = true;
        } 
    }

    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            Complete.PlayerMovement script = other.GetComponent<Complete.PlayerMovement>();
            //player stuck
            script.endPos = new Vector3(39, 3, 26);
            script.explode = true;
        }
    }
}
