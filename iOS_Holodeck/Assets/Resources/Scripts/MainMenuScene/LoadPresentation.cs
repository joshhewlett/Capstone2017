using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadPresentation : MonoBehaviour {

	public void loadPresentation(InputField go){
		ApplicationModel.presentationId = go.text.ToString();
		Debug.Log(ApplicationModel.presentationId);

		GameObject canvas = GameObject.Find("Canvas");

		// Presentation Loader
		canvas.transform.GetChild(2).gameObject.SetActive(true);
		// Panel components
		canvas.transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
		canvas.transform.GetChild(0).GetChild(1).gameObject.SetActive(false);
		// Loading circle
		canvas.transform.GetChild(1).gameObject.SetActive(true);
	}
}
