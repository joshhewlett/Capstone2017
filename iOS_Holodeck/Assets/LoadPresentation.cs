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
		SceneManager.LoadScene("iOS_Holo");
	}
}
