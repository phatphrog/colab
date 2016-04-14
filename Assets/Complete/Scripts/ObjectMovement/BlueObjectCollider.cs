using UnityEngine;

public class BlueObjectCollider : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        Complete.PlayerMovement script = collision.gameObject.GetComponent<Complete.PlayerMovement>();
        if (script)
        {
            if (script.m_PlayerNumber == 2)
            {
                GetComponent<Rigidbody>().isKinematic = false;
            }
            else
            {
                GetComponent<Rigidbody>().isKinematic = true;
            }
        }
    }

    /*void OnCollisionExit(Collision collision)
    {
        Complete.PlayerMovement script = collision.gameObject.GetComponent<Complete.PlayerMovement>();
        if (script)
        {
            if (script.m_PlayerNumber == 2 || script.m_PlayerNumber == 1)
            {
                collision.rigidbody.velocity = Vector3.zero;
            }
        }
    }*/
}