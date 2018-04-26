using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BestHTTP.SocketIO;

public class SocketController {

	readonly string TRANSFORM_UPDATE = "TRANSFORM_UPDATE";
	readonly string SLIDE_CHANGED = "SLIDE_CHANGED";
	readonly string PRESENTATION_END = "PRESENTATION_END";
    readonly string SOCKET_URL = ApplicationModel.API_URL + "/socket.io/";


	SocketManager socketManager;
	List<Action<object[]>> transformUpdateActions = new List<Action<object[]>>();
	List<Action<object[]>> slideChangedActions = new List<Action<object[]>>();
	List<Action<object[]>> presentationEndActions = new List<Action<object[]>>();
	
	public SocketManager getInstance() {
        Debug.Log("Getting instance");
		if (socketManager == null){
			socketManager = new SocketManager(new Uri(SOCKET_URL));
			setupSocketLisenters();
		}
		return socketManager;
	}

	public void disconnect(){
		if (socketManager != null){
			socketManager.Close();
		}
	}

	public void connect(){
		if (socketManager != null){
			socketManager.Open();
		}
	}

	public void addTransformUpdateListener(Action<object[]> action){
        Debug.Log("Added Transform update listener");
		transformUpdateActions.Add(action);
	}

    public void addSlideChangedListener(Action<object[]> action){
        Debug.Log("Added Slide changed listener");
		slideChangedActions.Add(action);
	}

    public void addPresentationEndListener(Action<object[]> action) {
        Debug.Log("Added presentation end listener");
		presentationEndActions.Add(action);
	}

	private void setupSocketLisenters(){
		socketManager.Socket.On(TRANSFORM_UPDATE, onTransformUpdate);
		socketManager.Socket.On(SLIDE_CHANGED, onSlideChanged);
		socketManager.Socket.On(PRESENTATION_END, onPresentationEnd);

		// predefined events "connect", "connecting", "event", "disconnect", "reconnect", "reconnecting", "reconnect_attempt", "reconnect_failed", "error"
		socketManager.Socket.On(SocketIOEventTypes.Error, OnError);
	}

	private void onTransformUpdate(Socket socket, Packet packet, params object[] args) {
		// Parse event details here

        foreach (Action<object[]> action in transformUpdateActions){
			// TODO pass params
			action(args);
		}
	}

	private void onSlideChanged(Socket socket, Packet packet, params object[] args) {
		// Parse event details here

        foreach (Action<object[]> action in slideChangedActions){
			// TODO pass params
			action(args);
		}
	}

	private void onPresentationEnd(Socket socket, Packet packet, params object[] args) {
		// Parse event details here

        foreach (Action<object[]> action in presentationEndActions){
			// TODO pass params
			action(args);
		}
	}

	// Provided with BestHTTP documentation
	private void OnError(Socket socket, Packet packet, params object[] args)
	{
		Error error = args[0] as Error;
		switch (error.Code)
		{
			case SocketIOErrors.User:
				Debug.Log("Exception in an event handler!");
				break;
			case SocketIOErrors.Internal:
				Debug.Log("Internal error!");
				break;
			default:
				Debug.Log("Server error!");
				break;
		}

		Debug.Log(error.ToString());
	}

}
