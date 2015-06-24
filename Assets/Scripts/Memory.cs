using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Memory : MonoBehaviour
{

	//key - the recordable object, value - a list of the records asociated to that recordable.
	Dictionary<IRecordable, List<Record>> dictionary = new Dictionary<IRecordable, List<Record>> ();

	void Start ()
	{
	
	}
	
	public void AddRecordable (IRecordable recordable)
	{
		dictionary.Add (recordable, new List<Record> ());
	}

	public void RemoveRecordable (IRecordable recordable)
	{
		if (dictionary.ContainsKey (recordable)) 
		{
			dictionary.Remove(recordable);
		} else 
		{
			Debug.LogError ("Trying to remove a recordable but not found");
		}
	}

	public List<IRecordable> getRecordables ()
	{
		return dictionary.Keys.ToList();
	}

	public void AddRecord (IRecordable recordable, Record record)
	{
		if (dictionary.ContainsKey (recordable)) 
		{
			dictionary [recordable].Add (record);
		} else 
		{
			Debug.LogError ("Trying to save a record but not recordable found");
		}
	}

	public void RemoveLastRecord (IRecordable recordable)
	{
		if (dictionary.ContainsKey (recordable)) 
		{
			dictionary [recordable].RemoveAt (dictionary [recordable].Count - 1);
		} else 
		{
			Debug.LogError ("Trying to remove a record but not recordable found");
		}
	}

	public List<Record> getRecords (IRecordable recordable)
	{
		if (dictionary.ContainsKey (recordable)) 
		{
			return dictionary [recordable];
		} else 
		{
			Debug.LogError ("Trying to get all records from recordable but not recordable found");
			return null;
		}
	}

	public int getSize ()
	{
		return dictionary.Count;
	}

	void OnDrawGizmos ()
	{
		Gizmos.color = Color.blue;

		foreach (IRecordable recordable in getRecordables())
		{
			//iterate through the records to get this recordable updated
			List<Record> records = getRecords(recordable);
			if(records.Count > 0)
			{
				foreach (Record record in records)
				{
					Gizmos.DrawWireSphere(record.position, 0.5f);
				}
			}
			else
			{
				Debug.Log("No more records for recordable: " + recordable);
			}
		}

	}

}
