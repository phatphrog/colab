using UnityEngine;
using System.Collections;

namespace Complete
{
    public class PlayerPing : MonoBehaviour
    {

        public int m_PlayerNumber = 1;  // Used to identify which avatar belongs to which player.  This is set by this avatar's manager.
        private string m_PingButton;          // The name of the PingButton 
        private Rigidbody m_Rigidbody;              // Reference used to move the player.
        private ParticleSystem m_PingParticles;

        private void Awake()
        {
            m_Rigidbody = GetComponent<Rigidbody>();
            m_PingParticles = m_Rigidbody.GetComponentInChildren<ParticleSystem>();
        }

        private void Start()
        {
            // The ping buttons on player number.
            m_PingButton = "Ping" + m_PlayerNumber;
        }

        // Update is called once per frame
        private void Update()
        {
            if(Input.GetButtonDown(m_PingButton))
            {
                if (m_PingParticles.isPlaying)
                {
                    m_PingParticles.Stop();
                    
                }
                m_PingParticles.Play();
           
            }
        }
    }
}
