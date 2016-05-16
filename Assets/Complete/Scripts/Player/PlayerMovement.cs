using UnityEngine;

namespace Complete
{
    public class PlayerMovement : MonoBehaviour
    {
        public int playerNumber = 1;              // Used to identify which avatar belongs to which player.  This is set by this avatar's manager.
        public float speed = 6f;                 // How fast the player moves forward and back.
        public bool scaled = false;
        public bool explode = false;
        public Vector3 currentPos;
        public Vector3 endPos = new Vector3(-7, 0, -17);
        public float translationTime = 2F;
        public float startTime = 0;

        private string verticalAxisName;          // The name of the input axis for moving forward and back.
        private string horizontalAxisName;              // The name of the input axis for turning.
        private Rigidbody rigidBody;              // Reference used to move the player.
        private float verticalInputValue;         // The current value of the movement input.
        private float horizontalInputValue;             // The current value of the turn input.

        private void Awake ()
        {
            rigidBody = GetComponent<Rigidbody> ();
        }


        private void OnEnable ()
        {
            // When the player is turned on, make sure it's not kinematic.
            rigidBody.isKinematic = false;

            // Also reset the input values.
            verticalInputValue = 0f;
            horizontalInputValue = 0f;
        }


        private void OnDisable ()
        {
            // When the player is turned off, set it to kinematic so it stops moving.
            rigidBody.isKinematic = true;
        }


        private void Start ()
        {
            // The axes names are based on player number.
            verticalAxisName = "Vertical" + playerNumber;
            horizontalAxisName = "Horizontal" + playerNumber;
        }


        private void Update ()
        {
            // Store the value of both input axes.
            verticalInputValue = Input.GetAxis (verticalAxisName);
            horizontalInputValue = Input.GetAxis (horizontalAxisName);

            if(explode)
            {
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
            Vector3 movement = transform.forward * verticalInputValue * speed * Time.deltaTime;

            // Apply this movement to the rigidbody's position.
            rigidBody.MovePosition(rigidBody.position + movement);
        }

        private void MoveHorizontal ()
        {
            // Create a vector in the direction the player is facing with a magnitude based on the input, speed and the time between frames.
            Vector3 movement = transform.right * horizontalInputValue * speed * Time.deltaTime;

            // Apply this movement to the rigidbody's position.
            rigidBody.MovePosition(rigidBody.position + movement);
        }

    }
}