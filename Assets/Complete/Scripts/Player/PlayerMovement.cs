using UnityEngine;

namespace Complete
{
    public class PlayerMovement : MonoBehaviour
    {
        public int m_PlayerNumber = 1;              // Used to identify which avatar belongs to which player.  This is set by this avatar's manager.
        public float m_Speed = 3f;                 // How fast the player moves forward and back.
        public float m_TurnSpeed = 180f;            // How fast the player turns in degrees per second.
        public bool scaled = false;
        public bool explode = false;
        public Vector3 currentPos;
        private Vector3 endPos = new Vector3(-7, 0, -17);
        public float translationTime = 2F;
        public float startTime = 0;


        private string m_VerticalAxisName;          // The name of the input axis for moving forward and back.
        private string m_HorizontalAxisName;              // The name of the input axis for turning.
        private Rigidbody m_Rigidbody;              // Reference used to move the player.
        private float m_VerticalInputValue;         // The current value of the movement input.
        private float m_HorizontalInputValue;             // The current value of the turn input.
        private float m_OriginalPitch;              // The pitch of the audio source at the start of the scene.


        private void Awake ()
        {
            m_Rigidbody = GetComponent<Rigidbody> ();
        }


        private void OnEnable ()
        {
            // When the player is turned on, make sure it's not kinematic.
            m_Rigidbody.isKinematic = false;

            // Also reset the input values.
            m_VerticalInputValue = 0f;
            m_HorizontalInputValue = 0f;
        }


        private void OnDisable ()
        {
            // When the player is turned off, set it to kinematic so it stops moving.
            m_Rigidbody.isKinematic = true;
        }


        private void Start ()
        {
            // The axes names are based on player number.
            m_VerticalAxisName = "Vertical" + m_PlayerNumber;
            m_HorizontalAxisName = "Horizontal" + m_PlayerNumber;
        }


        private void Update ()
        {
            // Store the value of both input axes.
            m_VerticalInputValue = Input.GetAxis (m_VerticalAxisName);
            m_HorizontalInputValue = Input.GetAxis (m_HorizontalAxisName);

            if(m_PlayerNumber == 1 && explode)
            {
                m_VerticalInputValue = 0;
                m_HorizontalInputValue = 0;
                Vector3 center = (currentPos + endPos) * 0.5F;
                center -= new Vector3(0, 1, 0);
                Vector3 riseRelCenter = currentPos - center;
                Vector3 setRelCenter = endPos - center;
                float fracComplete = (Time.time - startTime) / translationTime;
                transform.position = Vector3.Slerp(riseRelCenter, setRelCenter, fracComplete);
                transform.position += center;
                if(transform.position == endPos)
                {
                    explode = false;
                }
            }
        }

        private void FixedUpdate ()
        {
            // Adjust the rigidbodies position and orientation in FixedUpdate.
            MoveVertical ();
            MoveHorizontal ();
        }


        private void MoveVertical ()
        {
            // Create a vector in the direction the player is facing with a magnitude based on the input, speed and the time between frames.
            Vector3 movement = transform.forward * m_VerticalInputValue * m_Speed * Time.deltaTime;

            // Apply this movement to the rigidbody's position.
            m_Rigidbody.MovePosition(m_Rigidbody.position + movement);
        }

        private void MoveHorizontal ()
        {
            // Create a vector in the direction the player is facing with a magnitude based on the input, speed and the time between frames.
            Vector3 movement = transform.right * m_HorizontalInputValue * m_Speed * Time.deltaTime;

            // Apply this movement to the rigidbody's position.
            m_Rigidbody.MovePosition(m_Rigidbody.position + movement);
        }

    }
}