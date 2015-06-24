using UnityEngine;

public sealed class Rewind : FSMState<TimeController> {
	
	static readonly Rewind instance = new Rewind();
	public static Rewind Instance 
	{
		get {
			return instance;
		}
	}
	
	static Rewind () {}
	private Rewind () {}
	
	public override void Enter (TimeController t)
	{
		Debug.Log("enter rewind state");
		t.rewinder.StartRewind();
	}
	
	public override void Execute (TimeController t)
	{
		//Debug.Log("executing rewind state");

		Time.fixedDeltaTime = 0f;
		GlobalTime.GameTime += Time.unscaledDeltaTime * t.myTimeScale; //rewinding gameTime
	}
	
	public override void Exit (TimeController t)
	{
		Debug.Log("exit rewind state");
		t.rewinder.StopRewind();
	}
}