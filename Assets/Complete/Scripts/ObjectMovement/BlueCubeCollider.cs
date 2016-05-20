using UnityEngine;
using System.Collections;

public class BlueCubeCollider : MonoBehaviour
{

    public bool isLarge = false;
    public AudioSource sandMoving;
    private bool soundPlaying = false;

    void OnCollisionEnter(Collision collision)
    {
        BlueCollision(collision);
    }

    void OnCollisionStay(Collision collision)
    {
        BlueCollision(collision);
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
            if (specialScript.playerNumber == 2)
            {
                if (!isLarge)
                {
                    GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
                    if(!soundPlaying)
                    {
                        sandMoving.Play();
                        soundPlaying = true;
                    }
                    
                }
                else
                {

                    if (specialScript.specialOn)
                    {
                        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezePositionZ;
                        if (!soundPlaying)
                        {
                            sandMoving.Play();
                            soundPlaying = true;
                        }
                    }
                    else
                    {
                        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
                        sandMoving.Stop();
                        soundPlaying = false;
                    }
                }

            }
            else
            {
                GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
            }
        }
    }
}