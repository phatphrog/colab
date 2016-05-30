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
        public bool disabled = false;
        public bool triggered = false;
        public float originalYValue;

        private string verticalAxisName;          // The name of the input axis for moving forward and back.
        private string horizontalAxisName;              // The name of the input axis for turning.
        private Rigidbody rigidBody;              // Reference used to move the player.
        private float verticalInputValue;         // The current value of the movement input.
        private float horizontalInputValue;             // The current value of the turn input.
        private float circleSpeed = 0.2F;
        private float circleSize = 1F;
        private float circleGrowSpeed = 0.3F;
        private float xValue = 0;
        private float zValue = 0;

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
            originalYValue = (transform.position.y);
        }


        private void Update ()
        {
            // Store the value of both input axes.
            if (!disabled)
            {
                verticalInputValue = Input.GetAxis(verticalAxisName);
                horizontalInputValue = Input.GetAxis(horizontalAxisName);
            }
            else
            {
                verticalInputValue = 0;
                horizontalInputValue = 0;
            }

            if (triggered)
            {
                GetComponent<Rigidbody>().useGravity = false;
                GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                if (playerNumber == 2)
                {
                    MoveTowardsTarget(originalYValue + 18F);
                } else if(playerNumber == 1)
                {
                    MoveTowardsTarget(originalYValue + 18F);
                }
                
            }
            

            if(explode)
            {
                Vector3 center = (currentPos + endPos) * 0.5F;
                center -= new Vector3(0, 1, 0);
                Vector3 riseRelCenter = currentPos - center;
                Vector3 setRelCenter = endPos - center;
                float fracComplete = (Time.time - startTime) / translationTime;
                transform.position = Vector3.Slerp(riseRelCenter, setRelCenter, fracComplete);
                transform.position += center;
                if(transform.position.x == endPos.x && transform.position.z == endPos.z)
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

        //move towards a target at a set speed.
        private void MoveTowardsTarget(float yValue)
        {
            //the speed, in units per second, we want to move towards the target
            float speed = 6F;

            //move door down
            Vector3 currentPosition = this.transform.position;

            xValue = Mathf.Sin(Time.time * circleSpeed) * circleSize;
            zValue = Mathf.Cos(Time.time * circleSpeed) * circleSize;
            circleSize += circleGrowSpeed;

            if (playerNumber == 1)
            {
                xValue = -xValue;
                zValue = -zValue;
            }
            Vector3 targetPosition = new Vector3(xValue+28F, yValue, zValue-25F);

            //first, check to see if we're close enough to the target
            if (Vector3.Distance(currentPosition, targetPosition) > .1f)
            {
                Vector3 directionOfTravel = targetPosition - currentPosition;
                //now normalize the direction, since we only want the direction information
                directionOfTravel.Normalize();
                //scale the movement on each axis by the directionOfTravel vector components

                this.transform.Translate(
                    (directionOfTravel.x * speed * Time.deltaTime),
                    (directionOfTravel.y * speed * Time.deltaTime),
                    (directionOfTravel.z * speed * Time.deltaTime),
                    Space.World);
            }
        }

    }
}