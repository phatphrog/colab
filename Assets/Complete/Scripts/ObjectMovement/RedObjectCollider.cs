using UnityEngine;
using System.Collections;

public class RedObjectCollider : MonoBehaviour {

    void OnCollisionEnter(Collision collision)
    {
        Complete.PlayerMovement script = collision.gameObject.GetComponent<Complete.PlayerMovement>();
        if (script)
        {
            if (script.playerNumber == 1)
            {
                GetComponent<Rigidbody>().isKinematic = false;
            }
            else
            {
                GetComponent<Rigidbody>().isKinematic = true;
            }
        }
    }
}