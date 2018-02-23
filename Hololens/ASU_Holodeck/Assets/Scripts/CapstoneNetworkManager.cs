using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


public class CapstoneNetworkManager : MonoBehaviour {

    public bool inProgress;

    public enum RequestTypes {
        Load_Project,
        Save_Project,
        Retrieve_Poly,
        Login
    };
    //TODO: define server target with James
    //public const string serverTarget = "http://127.0.0.1";
    public const string serverTarget = "http://capstone-679aea6d.207c8177.svc.dockerapp.io:3001";
    //cookie used for authentication data. Received from file
    private string cookie;

    public UnityWebRequest client;

	// Use this for initialization
	void Start () {
        //Request(RequestTypes.Load_Project, "1");
	}

    /**
     * [in] type      Request type for sending to storage server
     * [in] data      Changes depending on the call being used
     * [in] target    Destination to write to (mainly used to differentiate between server and Poly API)
     */
    public string Request(RequestTypes type, string data, string target=serverTarget) {
        string retval = ""; 
        switch (type) {
            case RequestTypes.Load_Project:
                // data is the respective presentation id to retrieve 
                //http://capstone-679aea6d.207c8177.svc.dockerapp.io:3001
                string downloadArea = serverTarget + "/presentation/" + data + "/all";
                //Debug.Log("URL:\t" + downloadArea);
                client = UnityWebRequest.Get(downloadArea);
                StartCoroutine(GetDataResults());
                
                while (retval == "") {
                    retval = client.downloadHandler.text;
                }
                Debug.Log("Network Retval Value: \t" + retval);
                break;
            case RequestTypes.Save_Project:
                //TODO: Put the proper endpoint here to save the presentation
                string saveArea = serverTarget + "/presentation/" + data + "/all";

                client = UnityWebRequest.Put(saveArea);
                StartCoroutine(GetDataResults());
                break;
            case RequestTypes.Retrieve_Poly:
                break;
            default:
                retval = "error";
                break;
        }

        return retval;
    }


    public IEnumerator GetDataResults()
    {
        inProgress = true;
        //Debug.Log("Before async call");
        // Wait for asychronous response 
        yield return client.Send();
        //Debug.Log("After async call");
        if (client.isNetworkError) {
            Debug.Log(client.error);
            inProgress = false;
        }
        else {
            // Show results as text
            Debug.Log(client.downloadHandler.text);
            inProgress = false;
        }
    }

    

    //fill in information to process file and send cookie
    public void authenticate() {

    }
}
