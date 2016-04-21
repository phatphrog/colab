using UnityEngine;
using System.Collections;

namespace Complete
{
    public class PlayerPing : MonoBehaviour
    {

        public int playerNumber = 1;  // Used to identify which avatar belongs to which player.  This is set by this avatar's manager.
        private string pingButton;          // The name of the PingButton 
        private Rigidbody rigidBody;              // Reference used to move the player.
        private ParticleSystem pingParticles;

        private void Awake()
        {
            rigidBody = GetComponent<Rigidbody>();
            pingParticles = rigidBody.GetComponentInChildren<ParticleSystem>();
        }

        private void Start()
        {
            // The ping buttons on player number.
            pingButton = "Ping" + playerNumber;
        }

        // Update is called once per frame
        private void Update()
        {
            if(Input.GetButtonDown(pingButton))
            {
                if (pingParticles.isPlaying)
                {
                    pingParticles.Stop();
                    pingParticles.Clear();
                }
                pingParticles.Play();
            }
        }
    }
}
