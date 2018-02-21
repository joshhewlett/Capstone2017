using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coroutinetest : MonoBehaviour {

    bool inFirst = false;

    public void Start() {

        print("in StartPage()");
        StartCoroutine(FinishFirst(5.0f));
        StartCoroutine(DoLast());
        print("done");
    }

    IEnumerator FinishFirst(float waitTime) {
        inFirst = true;
        print("in FinishFirst");
        yield return new WaitForSeconds(waitTime);
        print("leave FinishFirst");
        inFirst = false;
    }

    IEnumerator DoLast() {

        while (inFirst)
            yield return new WaitForSeconds(0.1f);
        print("Do stuff.");
    }
    
	
	// Update is called once per frame
	void Update () {
		
	}
}
