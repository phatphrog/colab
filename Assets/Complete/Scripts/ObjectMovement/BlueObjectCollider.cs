using UnityEngine;
using System.Collections;

public class BlueObjectCollider : MonoBehaviour {

    public AudioSource sandMoving;
    private bool soundPlaying = false;
    public bool triggered = false;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            BlueCollision(collision);
        }
    }

    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            BlueCollision(collision);
        }
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

    private void BlueCollision(Collision collision)
    {
        Complete.PlayerSpecial specialScript = collision.gameObject.GetComponent<Complete.PlayerSpecial>();
        if (specialScript)
        {
            if (specialScript.playerNumber == 2 && !triggered)
            {
                GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
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
