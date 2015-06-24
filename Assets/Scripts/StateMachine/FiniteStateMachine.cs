using UnityEngine;
using System.Collections;

public class FiniteStateMachine <T>
{
	private T Owner;
	private FSMState<T> CurrentState;
	private FSMState<T> PreviousState;
	private FSMState<T> GlobalState;

	public void Awake ()
	{
		CurrentState = null;
		PreviousState = null;
		GlobalState = null;
	}

	public void Configure (T owner, FSMState<T> InitialState)
	{
		Owner = owner;
		SetState (InitialState);
	}

	public void  Update ()
	{
		if (GlobalState != null)
			GlobalState.Execute (Owner);
		if (CurrentState != null)
			CurrentState.Execute (Owner);
	}
	
	public void  SetState (FSMState<T> NewState)
	{
		if (NewState != CurrentState) {
			PreviousState = CurrentState;
			if (CurrentState != null)
				CurrentState.Exit (Owner);
			CurrentState = NewState;
			if (CurrentState != null)
				CurrentState.Enter (Owner);
		}
	}
	
	public void  RevertToPreviousState ()
	{
		if (PreviousState != null)
			SetState (PreviousState);
	}
}