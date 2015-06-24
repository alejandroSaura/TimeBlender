using UnityEngine;

public sealed class Forward : FSMState<TimeController>
{

	static readonly Forward instance = new Forward ();

	public static Forward Instance {
		get {
			return instance;
		}
	}

	static Forward ()
	{
	}

	private Forward ()
	{
	}

	public override void Enter (TimeController t)
	{
		//Debug.Log ("enter forward state");
		t.recorder.StartRecord ();
	}

	public override void Execute (TimeController t)
	{
		//Debug.Log ("executing forward state");

		//The fixed delta time frequency will be 1/0.02 frames per real-time second, so the slow-motion can be smooth.
		Time.fixedDeltaTime = 0.02F * Time.timeScale;
		
		Time.timeScale = t.myTimeScale;	
		GlobalTime.GameTime += Time.deltaTime;
	}

	public override void Exit (TimeController t)
	{
		Debug.Log ("exit forward state");
		t.recorder.StopRecord();
		Time.timeScale = 0f; //stop realtime engine calculations


	}
}
