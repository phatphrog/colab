using UnityEngine;
using System.Collections;

public class PurpleCubeCollider : MonoBehaviour {

    public bool isLarge = false;
    private bool player1Collision = false;
    private bool player2Collision = false;

    void OnCollisionEnter(Collision collision)
    {
        PurpleCollision(collision);
    }

    void OnCollisionStay(Collision collision)
    {
        PurpleCollision(collision);
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Player" && isLarge)
        {
            collision.rigidbody.velocity = Vector3.zero;

            Complete.PlayerMovement script = collision.gameObject.GetComponent<Complete.PlayerMovement>();
            if (script)
            {
                if (script.playerNumber == 2)
                {
                    player2Collision = false;
                }
                else if (script.playerNumber == 1)
                {
                    player1Collision = false;
                }

            }
        }
    }

    private void PurpleCollision(Collision collision)
    {
        Complete.PlayerSpecial specialScript = collision.gameObject.GetComponent<Complete.PlayerSpecial>();
        if (specialScript)
        {
            if (collision.gameObject.tag == "Player")
            {
                if (!isLarge)
                {
                    GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY;
                }
                else
                {
                    if (specialScript.playerNumber == 2)
                    {
                        player2Collision = true;
                    }
                    else if (specialScript.playerNumber == 1)
                    {
                        player1Collision = true;
                    }


                    if (player2Collision && player1Collision)
                    {
                        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY;
                    }
                    else
                    {
                        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
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
