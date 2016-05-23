using UnityEngine;
using System.Collections;

public class RedObjectCollider : MonoBehaviour
{

    public AudioSource sandMoving;
    private bool soundPlaying = false;
    public bool triggered = false;

    void OnCollisionEnter(Collision collision)
    {
        RedCollision(collision);
    }

    void OnCollisionStay(Collision collision)
    {
        RedCollision(collision);
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Complete.PlayerMovement script = collision.gameObject.GetComponent<Complete.PlayerMovement>();
            if (script)
            {
                if (script.playerNumber == 2)
                {
                    sandMoving.Stop();
                    soundPlaying = false;
                }
            }
        }
    }

    private void RedCollision(Collision collision)
    {
        Complete.PlayerSpecial specialScript = collision.gameObject.GetComponent<Complete.PlayerSpecial>();
        if (specialScript)
        {
            if (specialScript.playerNumber == 1 && !triggered)
            {
                GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionY;
                if (!soundPlaying)
                {
                    sandMoving.Play();
                    soundPlaying = true;
                }
            }
            else
            {
                GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezePositionY;
            }
        }
    }
}
