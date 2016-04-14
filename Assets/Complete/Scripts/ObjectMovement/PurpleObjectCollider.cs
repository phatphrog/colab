using UnityEngine;
using System.Collections;

public class PurpleObjectCollider : MonoBehaviour {

    public bool player1Collision = false;
    public bool player2Collision = false;
    public bool scaled = false;

    void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.tag == "PurpleScaleDecrease" && !scaled)
        {
            transform.localScale += new Vector3(-2F, -2F, -2F);
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            //Destroy(collision.gameObject);
            scaled = true;
            GetComponent<Rigidbody>().isKinematic = false;
        }
        else if  (collision.gameObject.tag == "PurpleScaleIncrease" && scaled)
        {
            transform.localScale += new Vector3(2F, 2F, 2F);
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionY;
            //Destroy(collision.gameObject);
            scaled = false;
            GetComponent<Rigidbody>().isKinematic = true;
        }


        if (collision.gameObject.tag == "Player" && !scaled)
        { 
            Complete.PlayerMovement script = collision.gameObject.GetComponent<Complete.PlayerMovement>();
            if (script)
            {
                if (script.m_PlayerNumber == 2)
                {
                    player2Collision = true;
                }
                else if(script.m_PlayerNumber == 1)
                {
                    player1Collision = true;
                }

            }

            if (player2Collision && player1Collision)
            {
                GetComponent<Rigidbody>().isKinematic = false;
            } else 
            {
                GetComponent<Rigidbody>().isKinematic = true;
            }
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Player" && !scaled)
        {
            collision.rigidbody.velocity = Vector3.zero;

            Complete.PlayerMovement script = collision.gameObject.GetComponent<Complete.PlayerMovement>();
            if (script)
            {
                if (script.m_PlayerNumber == 2)
                {
                    player2Collision = false;
                }
                else if (script.m_PlayerNumber == 1)
                {
                    player1Collision = false;
                }

            }
        }
    }
}
