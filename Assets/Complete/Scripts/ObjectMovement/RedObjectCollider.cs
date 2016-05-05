using UnityEngine;
using System.Collections;

public class RedObjectCollider : MonoBehaviour {

    public AudioSource sandMoving;

    void OnCollisionEnter(Collision collision)
    {
        Complete.PlayerMovement script = collision.gameObject.GetComponent<Complete.PlayerMovement>();
        if (script)
        {
            if (script.playerNumber == 1)
            {
                GetComponent<Rigidbody>().isKinematic = false;
                sandMoving.Play();
            }
            else
            {
                GetComponent<Rigidbody>().isKinematic = true;
            }
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Complete.PlayerMovement script = collision.gameObject.GetComponent<Complete.PlayerMovement>();
            if (script)
            {
                if (script.playerNumber == 1 )
                {
                    sandMoving.Stop();
                    GetComponent<Rigidbody>().isKinematic = true;
                }
            }
        }
    }
}