using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BackButton : MonoBehaviour {
    public void backButton() {
        SceneManager.LoadScene("PresentationIDInput");
    }
}
