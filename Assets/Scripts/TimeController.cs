using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

public class TimeController : MonoBehaviour
{
	public float testGlobalTime = 0f;
	public float myTimeScale;
	float timeVel = 0.005f;

	public Recorder recorder;
	public Rewinder rewinder;

	private FiniteStateMachine<TimeController> FSM;

	void Start ()
	{	
		myTimeScale = Time.timeScale;
		recorder = this.GetComponentInParent<Recorder> ();
		rewinder = this.GetComponentInParent<Rewinder>();

		FSM = new FiniteStateMachine<TimeController>(); //init FSM
		FSM.Configure(this, Forward.Instance); //set the FSM initial state
	}	

	void Update ()
	{
		HandleInput ();

		//Set state:
		if (myTimeScale < 0f)
		{
			FSM.SetState(Rewind.Instance);
		}
		else
		{
			FSM.SetState(Forward.Instance);
		}	

		FSM.Update();
		testGlobalTime = GlobalTime.GameTime;
	}

	void HandleInput ()
	{
		//handle user input
		if (Input.GetKey (KeyCode.C) || CrossPlatformInputManager.GetButton ("Slower")) {
			slowTime ();
		}
		if (Input.GetKey (KeyCode.V) || CrossPlatformInputManager.GetButton ("Faster")) {
			accelerateTime ();
		}
	}

	public void slowTime ()
	{
		myTimeScale -= 0.005f;
	}

	public void accelerateTime ()
	{
		myTimeScale += 0.005f;
	}
}
