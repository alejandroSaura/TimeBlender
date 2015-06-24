using UnityEngine;

public sealed class Stop : FSMState<TimeController> {
	
	static readonly Stop instance = new Stop();
	public static Stop Instance 
	{
		get {
			return instance;
		}
	}
	
	static Stop () {}
	private Stop () {}
	
	public override void Enter (TimeController t)
	{
		
	}
	
	public override void Execute (TimeController t)
	{
		
	}
	
	public override void Exit (TimeController t)
	{
		
	}
}
