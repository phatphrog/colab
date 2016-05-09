using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class PurpleObjectCollider : MonoBehaviour {

    public bool player1Collision = false;
    public bool player2Collision = false;
    public bool scaled = false;
    public AudioClip pop1Clip;
    public AudioClip pop2Clip;


    void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.tag == "PurpleScaleDecrease" && !scaled)
        {
            transform.localScale += new Vector3(-2F, -2F, -2F);

            AudioSource.PlayClipAtPoint(pop1Clip, transform.position);
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            scaled = true;
            GetComponent<Rigidbody>().isKinematic = false;
        }
        else if  (collision.gameObject.tag == "PurpleScaleIncrease" && scaled)
        {
            transform.localScale += new Vector3(2F, 2F, 2F);
            AudioSource.PlayClipAtPoint(pop2Clip, transform.position);
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionY;
            scaled = false;
            GetComponent<Rigidbody>().isKinematic = true;
        }


        if (collision.gameObject.tag == "Player" && !scaled)
        { 
            Complete.PlayerMovement script = collision.gameObject.GetComponent<Complete.PlayerMovement>();
            if (script)
            {
                if (script.playerNumber == 2)
                {
                    player2Collision = true;
                }
                else if(script.playerNumber == 1)
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
}
