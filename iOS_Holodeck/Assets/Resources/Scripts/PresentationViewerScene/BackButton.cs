using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class BackButton : MonoBehaviour {
    public void backButton() {
        GameObject.Find("PresentationContainer").GetComponent<StateManager>().EndPresentation();
        // SceneManager.LoadScene("PresentationIDInput");
    }
}
