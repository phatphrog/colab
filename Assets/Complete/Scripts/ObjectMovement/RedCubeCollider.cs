using UnityEngine;
using System.Collections;

public class RedCubeCollider : MonoBehaviour {

    public AudioSource sandMoving;
    private bool soundPlaying = false;

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
                if (script.playerNumber == 1)
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
            if (specialScript.playerNumber == 1)
            {
                GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationX;
                if (!soundPlaying)
                    {
                        sandMoving.Play();
                        soundPlaying = true;
                    }
            }
            else
            {
                GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
            }
        }
    }
}