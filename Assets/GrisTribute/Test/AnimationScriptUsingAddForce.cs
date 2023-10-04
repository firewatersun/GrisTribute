using UnityEngine;
using System.Collections;

public class AnimationScriptUsingAddForce : MonoBehaviour
{
	public float maxSpeed;
	public float jumpPower;
	public float accelerationSpeed;//how fast the Rigidbody will accelerate to maxSpeed
	public float inAirAccelerationSpeed;
	public float inAirMaxSpeed;
	Rigidbody2D myrigidbody; //sets a variable called myrigidbody of type Rigidbody2D. not written as public so will be stored privately
	Animator anim; //sets a variable called anim of type Animator
	float currentSpeed; //this will be used to check for the horizontal velocity of the Rigidbody2D
	float upSpeed;//this will be used to check for the vertical velocity of the Rigidbody2D
	float startFriction; // checks the physicsmaterial2D start friction value
	public Transform animatorTransform;
	public bool amIGrounded;


	void Start()
	{
		startFriction = GetComponent<Collider2D>().sharedMaterial.friction;
		anim = GetComponentInChildren<Animator>(); //getcomponent Animator and assigns it to anim
		myrigidbody = GetComponent<Rigidbody2D>(); //getcomponent Rigidbody2D and assigns it to myrigidbody
	}



	void FixedUpdate() //change this to fixedupdate, better than update. Fixedupdate happens every physics calculation
	{




		//move code begins

		currentSpeed = myrigidbody.velocity.x; //sets currentSpeed variable to the current horizontal velocity of the rigidbody

		float move = Input.GetAxis("Horizontal"); //checks input axis on the horizontal, so A, D or arrow left right buttons. will also work with a controller

		//this following if/else statement checks to see if the user pressed the right/left buttons, and tells the Animator if the right button has been pressed or the left
		if (move > 0) {
			animatorTransform.localScale = new Vector3(-1, animatorTransform.localScale.y, animatorTransform.localScale.z); //flips the gameobject to -1 scale. Depending on which direction your gameobject started off, the -1 may need to be 1 instead this allows the animation to play in the opposite direction
		}
		else if (move < 0) {
			animatorTransform.localScale = new Vector3(1, animatorTransform.localScale.y, animatorTransform.localScale.z);

		}




		if (Mathf.Abs(currentSpeed) < maxSpeed && amIGrounded == true)
		{ //will only add more force if maxSpeed is not yet reached, AND player is on the ground. Mathf.Abs is used to make sure currentSpeed is always a positive value even if it is going left, which normally results in a negative value
			GetComponent<Collider2D>().sharedMaterial.friction = startFriction;
			myrigidbody.AddForce(new Vector2(move * accelerationSpeed, 0)); //this adds a force on the rigidbody of (move*accelerationSpeed).)
		}

		if (Mathf.Abs(currentSpeed) < inAirMaxSpeed && amIGrounded == false) // if player is in the air, reduce the amount of force added, since player is in air
		{
			GetComponent<Collider2D>().sharedMaterial.friction = 0; // sets friction to 0 to allow collider to slide off walls IF it is not grounded
			myrigidbody.AddForce(new Vector2(move * inAirAccelerationSpeed, 0)); //this adds a force on the rigidbody of (move*inAirAccelerationSpeed).)
		}

		anim.SetFloat("speed", (Mathf.Abs(currentSpeed + move))); //checks the currentspeed and outputs the value as speed of the animator. move is added so if trying to move against a wall, it will still play animation, as it will detect the keypress. Mathf.Abs returns the absolute value (so if movement is negative, will still send a positive value)

		//move code ends



		//jump code starts

		float moveup = Input.GetAxis("Vertical"); //checks input axis on the vertical so W, S, up and down arrow keys

		upSpeed = myrigidbody.velocity.y; //checks what the current vertical speed of the player is, and sets the float upSpeed to that value

		if (Input.GetKey("space")||moveup>0 && amIGrounded == true) //checks if up button is being pressed, and if the player is standing on something
		{
			myrigidbody.velocity = new Vector2(myrigidbody.velocity.x, 0);
			myrigidbody.AddForce (new Vector2 (0, jumpPower)); //if all the above conditions are correct, then jumps
		}


		anim.SetFloat("verticalSpeed", (upSpeed + moveup)); //tells the animator what the current vertical speed of the player is, in the form of a float called verticalSpeed. if positive, is moving up, negative, is falling

	//jump code ends

   }

	//checks if player is on the ground, and sends a bool to the Animator called grounded that is true if the player is on the ground, false if not. Uses a trigger underneath the player that checks if it is inside a collider or not
	private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag != "Player")
        {
            amIGrounded = true;
            anim.SetBool("grounded", true);
        }

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag != "Player")
        {
            amIGrounded = false;
            anim.SetBool("grounded", false);
        }

    }
    //end of groundcheck

}