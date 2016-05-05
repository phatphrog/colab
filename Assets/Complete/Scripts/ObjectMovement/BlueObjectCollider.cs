using UnityEngine;

public class BlueObjectCollider : MonoBehaviour
{

    public bool isLarge = false;

    void OnCollisionEnter(Collision collision)
    {
        BlueCollision(collision);
    }

    void OnCollisionStay(Collision collision)
    {
        BlueCollision(collision);
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
                    GetComponent<Rigidbody>().constraints =  RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
                }
                else
                {
                    
                    if (specialScript.specialOn)
                    {
                        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezePositionY;
                    }
                    else
                    {
                        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;   
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