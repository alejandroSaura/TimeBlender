using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Rewinder : MonoBehaviour {

	public float rewindInterval = 0.05f;
	Memory memory;
	bool enabled;


	void Awake () 
	{
		memory = gameObject.GetComponent<Memory> ();
	}
	
	public void StartRewind ()
	{
		enabled = true;
		StartCoroutine (rewind());
	}
	
	public void StopRewind ()
	{
		enabled = false;
		StopCoroutine (rewind());
	}
	
	IEnumerator rewind ()
	{		 

		while (enabled) 
		{
			//Debug.Log ("rewinding");
			foreach (IRecordable recordable in memory.getRecordables())
			{
				//iterate through the records to get this recordable updated
				List<Record> records = memory.getRecords(recordable);
				if(records.Count > 0)
				{
					Record nextRecord = records[records.Count - 1];

					while (nextRecord.time > GlobalTime.GameTime)
					{
						Debug.Log("Read record with time: " + nextRecord.time);
						recordable.setPosition(nextRecord.position);


							records.RemoveAt(records.Count -1);
							memory.RemoveLastRecord (recordable);


						if((records.Count -1)>0)
						{
							nextRecord = records[records.Count - 1];
						}
					}
				}
				else
				{
					Debug.Log("No more records for recordable: " + recordable);
				}
			}
			
			yield return StartCoroutine(CoroutineUtil.WaitForRealSeconds(rewindInterval)); //timescale independent
		}

	}
}
