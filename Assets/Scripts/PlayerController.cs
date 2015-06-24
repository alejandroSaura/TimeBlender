using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerController : MonoBehaviour, IRecordable
{

	public float joystickSensibilityX = 10f;
	public float joystickSensibilityY = 10f;
	public float runSpeed = 0.2f;
	public float jumpForce = 20f;
	public float dodgeForce = 15f;
	public float dodgeBreakFactor = 0.04f;

	public float jumpFactor = 20f;
	public float dodgeDuration = 500f;

	Vector3 moveDir;
	Vector3 moveAmount;
	Vector3 smoothMoveVelocity;

	CNJoystick joystick;
	Recorder recorder;

	TimeController timeController;

	public Record getRecord () //This function is called by the recorder in order to store data for rewind.
	{
		Record record = new Record ();
		record.position = transform.position;
		record.time = GlobalTime.GameTime;
		return record;
	}

	public void setPosition(Vector3 pos)
	{
		transform.position = pos;
	}

	void Start ()
	{
		Physics.gravity = new Vector3(0, -jumpFactor, 0);	
		joystick = GameObject.Find ("CNJoystick").GetComponent<CNJoystick> ();

		timeController = GameObject.Find("TimeController").GetComponent<TimeController>();

		//subscribe to the recorder for recording movement
		recorder = GameObject.Find ("TimeController").GetComponent<Recorder> ();
		recorder.Subscribe (this);
	}
	

	void Update ()
	{
		//MOVE
		moveDir = new Vector3 (joystick.GetAxis("Horizontal"), 0f, joystick.GetAxis("Vertical"));
		if (Application.platform == RuntimePlatform.WindowsEditor && (Input.GetAxisRaw ("Horizontal") != 0 || Input.GetAxisRaw ("Vertical") != 0)) 
		{
			moveDir = new Vector3 (Input.GetAxisRaw ("Horizontal"), 0f, Input.GetAxisRaw ("Vertical")).normalized;
		}

		Vector3 targetMoveAmount = moveDir * runSpeed;
		//We can use this for smoothing the player movement
		//moveAmount = Vector3.SmoothDamp (moveAmount, targetMoveAmount, ref smoothMoveVelocity, .15f);
		moveAmount = targetMoveAmount;

		//JUMP
		if (Input.GetButtonDown("Jump") || CrossPlatformInputManager.GetButtonDown ("Jump")) 
		{
			Jump ();
		}

		//DODGE
		if ((CrossPlatformInputManager.GetButtonDown ("Dodge") || Input.GetKeyDown(KeyCode.LeftShift)) && moveDir != Vector3.zero) 
		{
			Dodge();
		}

		//LAND
//		if (CrossPlatformInputManager.GetButton ("Dodge") && moveDir == Vector3.zero)
//		{
//			Land ();
//		}
	
	}

	public void Jump ()
	{
		GetComponent<Rigidbody>().AddForce(transform.up * jumpForce,ForceMode.Impulse); 
		GetComponent<Rigidbody>().velocity = new Vector3(GetComponent<Rigidbody>().velocity.x, 0f, GetComponent<Rigidbody>().velocity.z);
		//Debug.Log ("Jump");
	}

	public void Dodge ()
	{
		//GetComponent<Rigidbody>().velocity = new Vector3(GetComponent<Rigidbody>().velocity.x, 0f, GetComponent<Rigidbody>().velocity.z);
		GetComponent<Rigidbody>().velocity = new Vector3(0f, 0f, 0f);//stop the rigidbody
		GetComponent<Rigidbody>().useGravity = false; //no gravity applied

		GetComponent<Rigidbody>().AddForce(moveDir * dodgeForce,ForceMode.Impulse); //else, we apply an impulse in the moveDir direction.
		StopCoroutine("EndDodge");
		//In certain msec we want the object to continue using gravity
		StartCoroutine(EndDodge());
	}

	IEnumerator EndDodge() {
		var t = 0f;
		while (t < dodgeDuration) 
		{
			t += Time.deltaTime;
			yield return null;
		}
		//Debug.Log ("End dodge");

		GetComponent<Rigidbody>().velocity = new Vector3(0f, 0f, 0f);//stop the rigidbody
		GetComponent<Rigidbody>().useGravity = true; //restore gravity

	}

	public void Land ()
	{
		GetComponent<Rigidbody>().AddForce(new Vector3(0f, -1f, 0f) * dodgeForce/4,ForceMode.Impulse); //Impulse towards the ground
	}

	void FixedUpdate()
	{
		GetComponent<Rigidbody>().MovePosition (GetComponent<Rigidbody>().position + moveAmount * Time.deltaTime*50f);
	}
}
