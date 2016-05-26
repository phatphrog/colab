using UnityEngine;
using System.Collections;

public class PlayerYinYangTrigger : MonoBehaviour {

    public int playerNo = 1;
    public float speed = 60F;
    private bool rotate = false;

    // Use this for initialization
    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "PlayerYinYangTrigger")
        {
            Complete.PlayerMovement script = other.GetComponentInParent<Complete.PlayerMovement>();

            if (script.playerNumber == playerNo)
            {
                Debug.Log("hello! " + script.playerNumber);
            }    
        }
    }



}
