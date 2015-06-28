using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Recorder : MonoBehaviour 
{
	public float recordInterval = 0.05f;
	List<IRecordable> recordables;
	Memory memory;

	bool enabled;

	public void Subscribe (IRecordable recordable)
	{
		recordables.Add (recordable);
		memory.AddRecordable(recordable);
	}

	public void UnSubscribe (IRecordable recordable)
	{
		recordables.Remove (recordable);
		memory.RemoveRecordable(recordable);
	}

	void Awake () 
	{
		recordables = new List<IRecordable> ();
		memory = gameObject.GetComponent<Memory> ();
	}	

	void Update () 
	{
		//Debug.Log ("recording " + recordables.Count + " objects");
		//DO THIS WITH A CURVE:
		//recordInterval = 0.005f / ((gameObject.GetComponent<TimeController> ().myTimeScale));

	}

	public void StartRecord ()
	{
		enabled = true;
		StartCoroutine (record());
	}

	public void StopRecord ()
	{
		enabled = false;
		StopCoroutine (record());
	}

	IEnumerator record ()
	{
		while (enabled) 
		{
			//Debug.Log ("recording");
			foreach (IRecordable recordable in recordables)
			{
				memory.AddRecord(recordable, recordable.getRecord()); //save the record of each recordable in memory.
			}

			yield return StartCoroutine(CoroutineUtil.WaitForRealSeconds(recordInterval)); //timescale independent
		}
	}

}
