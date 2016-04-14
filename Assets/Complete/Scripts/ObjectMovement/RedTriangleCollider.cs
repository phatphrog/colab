using UnityEngine;
using System.Collections;

public class RedTriangleCollider : MonoBehaviour
{
    public bool scaled = false;
    public float scaledTime = 0;


    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player" && !scaled)
        {
            
            Complete.PlayerMovement script = collision.gameObject.GetComponent<Complete.PlayerMovement>();
            if (script)
            {
                if (script.m_PlayerNumber == 1)
                {
                    collision.gameObject.transform.localScale += new Vector3(-2F, 0, -2F);
                    scaled = true;
                    script.scaled = true;

                    //wait 10 seconds then unscale
                    if(scaledTime > 0) { 
                        StartCoroutine(EnlargeObject(collision.gameObject, scaledTime));
                    }

                }
            }
        }

    }

    IEnumerator EnlargeObject(GameObject gobj, float delay)
    {
        float timer = 0.0F;
        while (timer < delay+1)
        {
            if (timer>delay && scaled)
            {
                gobj.transform.localScale += new Vector3(2F, 0, 2F);
                Complete.PlayerMovement script = gobj.GetComponent<Complete.PlayerMovement>();
                scaled = false;
                script.scaled = false;
                script.startTime = Time.time;
                script.currentPos = gobj.transform.position;

            }
            yield return new WaitForEndOfFrame();
            timer += Time.deltaTime;
        }
    }
}
