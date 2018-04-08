using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnToHomeScene : MonoBehaviour {

	public void ReturnToHome() {
		Debug.Log("Returning to home page");
		SceneManager.LoadScene("PresentationIdInput");
	}
}
