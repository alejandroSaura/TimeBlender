using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class TimeLabel : MonoBehaviour {

	Text _textScript;
	TimeController _timeController;
	float _timeScale;

	// Use this for initialization
	void Start () {
		_textScript = gameObject.GetComponent<Text> ();
		_timeController = GameObject.Find ("TimeController").GetComponent<TimeController> ();
	}
	
	// Update is called once per frame
	void Update () {

		_timeScale = _timeController.myTimeScale;
		_textScript.text = "Time Speed: x" + _timeScale.ToString();
			
	}
}
