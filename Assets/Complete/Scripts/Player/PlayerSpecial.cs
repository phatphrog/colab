using UnityEngine;
using UnityEngine.UI;

namespace Complete
{
    public class PlayerSpecial : MonoBehaviour
    {
        public float startingSpecial = 100f;               // The amount of special each player starts with.
        public Slider sliderSpecial;                             // The slider to represent how much special the player currently has.
        public Image fillImage;                           // The image component of the slider.
        public Color specialColour;       // The color the special bar will be when on full special. This is set by player manager to be the same colour as the player
        public int playerNumber = 1;  // Used to identify which avatar belongs to which player.  This is set by this avatar's manager.
        private string specialButton;          // The name of the SpecialButton 
        private float currentSpecial;                      // How much special the player currently has.
        private Rigidbody rigidBody;              // Reference used to move the player.
        public bool playerCollision = false;
        public bool specialOn = false;


        private void Start()
        {
            // The ping buttons on player number.
            specialButton = "Special" + playerNumber;

            //set the colour of the special slider based on player colour
            if (playerNumber == 1)
            {
                specialColour = new Color(1F, 0F, 0.316F, 0.194F);               
            } 
            else
            {
                specialColour = new Color(0F,0.903F,1F,0.284F);
            }

            fillImage.color = specialColour;
        }

        private void OnEnable()
        {
            // When the player is enabled, reset the player's health and whether or not it's dead.
            currentSpecial = startingSpecial;

            // Update the health slider's value and color.
            SetSpecialUI();
        }

        // Update is called once per frame
        private void Update()
        {
            //use special while button is held down
            if (Input.GetButton(specialButton))
            {
                UseSpecial(0.85F);
            }

            //stop special when button is let go, or if there is no special meter left
            if (Input.GetButtonUp(specialButton) || currentSpecial <= 0)
            {
                StopSpecial();
            }

            //refill special slider when players are colliding - playerCollision is altered by PlayerCollider class
            if (playerCollision)
            {
                RefillSpecial(1F);
            }
        }

        private void UseSpecial (float amount)
        {
            // Reduce current health by the amount of damage done.
            if(currentSpecial > 0f)
            {
                currentSpecial -= amount;

                // Change the UI elements appropriately.
                SetSpecialUI();

                if (!specialOn)
                {
                    if (playerNumber == 1)
                    {
                        //increase speed
                        Complete.PlayerMovement script = GetComponent<Complete.PlayerMovement>();
                        script.speed = 12F;
                    }
                    else
                    {
                        //grow
                        this.gameObject.transform.localScale += new Vector3(+0.8F, +0.4F, +0.8F);
                    }
                    specialOn = true;
                }
               
            }   
        }

        private void RefillSpecial(float amount)
        {
            // Reduce current health by the amount of damage done.
            if (currentSpecial < 100f)
            {
                currentSpecial += amount;

                // Change the UI elements appropriately.
                SetSpecialUI();
            }
        }

        private void StopSpecial()
        {
            if (specialOn)
            {
                if (playerNumber == 1)
                {
                    //decrease speed
                    Complete.PlayerMovement script = GetComponent<Complete.PlayerMovement>();
                    script.speed = 6F;
                }
                else
                {
                    //shrink
                    this.gameObject.transform.localScale += new Vector3(-0.8F, -0.4F, -0.8F);
                }
                specialOn = false;
            }
            
        }


        private void SetSpecialUI ()
        {
            // Set the slider's value appropriately.
            sliderSpecial.value = currentSpecial;
        }
    }
}