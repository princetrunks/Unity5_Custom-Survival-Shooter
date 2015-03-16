using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	public float speed = 6f;
	
	Vector3 movement;            //movement in (x,y,z)
	Animator anim;               //reference to Player Animation
	Rigidbody playerRigidbody;  //private reference to the RigidBody of the player
	int floorMask;               //reference to the Floor layer
	float camRayLength = 100f;   //RayCast length
	
	
	//Note: Awake() is better for references than Start() as it is called wheither script is enabled or not
	void Awake()  
	{
		floorMask = LayerMask.GetMask ("Floor");
		anim = GetComponent <Animator> ();
		playerRigidbody = GetComponent <Rigidbody> ();
		
	}
	
	//Note: FixedUpdate() is to be used for Physics updates instead of Update()
	//Update() syncs with Rendering
	void FixedUpdate()
	{
		float h = Input.GetAxisRaw ("Horizontal"); //GetAxisRaw is only -1, 0, 1... not varying values between that
		float v = Input.GetAxisRaw ("Vertical");   //Axis is input Mapped to certain keys in the InputManager
		
		Move (h, v);
		Turning ();
		Animating (h, v);
		
	}
	
	void Move(float h, float v)
	{
		movement.Set (h, 0f, v);
		movement = movement.normalized * speed * Time.deltaTime;  //normalized so that moving diagonal doesn't add extra distance
		
		
		playerRigidbody.MovePosition (transform.position + movement);
	}
	
	void Turning()
	{
		//uses RayCasting from the camera and mouse position (or touch) for where the character turns to
		Ray camRay = Camera.main.ScreenPointToRay (Input.mousePosition);
		
		RaycastHit floorHit;
		
		if (Physics.Raycast (camRay, out floorHit, camRayLength, floorMask)) 
		{
			Vector3 playerToMouse = floorHit.point - transform.position;
			playerToMouse.y = 0f;                                             //keeps the player from leaning forward
			
			Quaternion newRotation = Quaternion.LookRotation (playerToMouse); //Forward vector; z-axis
			playerRigidbody.MoveRotation (newRotation);
		}
		
	}
	
	void Animating (float h, float v)
	{
		bool walking = h != 0f || v != 0f;        //checks if moving in any axis
		anim.SetBool ("IsWalking", walking);
	}
	
}