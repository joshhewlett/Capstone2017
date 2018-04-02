using System;
using System.Collections.Generic;
using UnityEngine;

namespace UnityEngine.XR.iOS
{
	public class ARKitUserTapPlace : MonoBehaviour
	{
		public Transform m_HitTransform;
		public bool userHasTappedScreenInBoundingBox = false;
		public GameObject PolyImporter;
		public GameObject userCursor;
		public GameObject thumbnails;
		public GameObject modelMenu;
		public bool importing = false;

		//TODO: Disable the UnityARGeneratePlane script once user does a HitTest for plane detection.

		void Start() {
			userCursor.SetActive (true);
			// Assign transform property of plane generation to self.
			m_HitTransform = gameObject.transform;
			PolyImporter = transform.GetChild (1).gameObject;
		}

		bool HitTestWithResultType (ARPoint point, ARHitTestResultType resultTypes)
		{
			List<ARHitTestResult> hitResults = UnityARSessionNativeInterface.GetARSessionNativeInterface ().HitTest (point, resultTypes);
			if (hitResults.Count > 0) {
				foreach (var hitResult in hitResults) {
					Debug.Log ("Got hit!");
					m_HitTransform.position = UnityARMatrixOps.GetPosition (hitResult.worldTransform);
					m_HitTransform.rotation = UnityARMatrixOps.GetRotation (hitResult.worldTransform);
					Debug.Log (string.Format ("x:{0:0.######} y:{1:0.######} z:{2:0.######}", m_HitTransform.position.x, m_HitTransform.position.y, m_HitTransform.position.z));
					return true;
				}
			}
			return false;
		}

		// Update is called once per frame
		void Update () {
			var headPosition = Camera.main.transform.position;
			var gazeDirection = Camera.main.transform.forward;

			RaycastHit hitInfo;

			if (Physics.Raycast (headPosition, gazeDirection, out hitInfo)) {
			
//				if (((hitInfo.collider.gameObject.name == "plane") || !(userHasTappedScreenInBoundingBox)) && (Input.touchCount > 0 && m_HitTransform != null)) {
				if ((userHasTappedScreenInBoundingBox == false) && (Input.touchCount > 0 && m_HitTransform != null)) {
					if (hitInfo.collider.gameObject.name != "Plane") {
						return;
					}
					var touch = Input.GetTouch (0);
//					if (touch.phase == TouchPhase.Began || touch.phase == TouchPhase.Moved) {
					if (touch.phase == TouchPhase.Began) {
						var screenPosition = Camera.main.ScreenToViewportPoint (touch.position);
						ARPoint point = new ARPoint {
							x = screenPosition.x,
							y = screenPosition.y
						};

						// prioritize reults types
						// Creates a plane with all feature points for placing object on horizontal plane.
						ARHitTestResultType[] resultTypes = {
							ARHitTestResultType.ARHitTestResultTypeExistingPlaneUsingExtent, 
							// if you want to use infinite planes use this:
							//ARHitTestResultType.ARHitTestResultTypeExistingPlane,
							ARHitTestResultType.ARHitTestResultTypeHorizontalPlane, 
							ARHitTestResultType.ARHitTestResultTypeFeaturePoint
						}; 

						foreach (ARHitTestResultType resultType in resultTypes) {
							if (HitTestWithResultType (point, resultType)) {
								userHasTappedScreenInBoundingBox = true;	
								// Instantiate a menu as a child game object of the hit object,
								// and load it from the prefabs folder.
								modelMenu = Instantiate (
									                       Resources.Load ("Prefabs/Menu/PolyMenu", typeof(GameObject)),
									                       m_HitTransform) as GameObject;
								modelMenu.transform.localPosition = new Vector3 (0.94f, 3.69f, 0f);
								modelMenu.transform.localRotation = Quaternion.Euler (0f, 28.437f, 0f);
								modelMenu.transform.localScale = new Vector3 (5.5f, 5.5f, 5.5f);
								// Grab reference to thumbnails encapsulated parent.
								thumbnails = modelMenu.transform.GetChild (0).gameObject;
								// Load thumbnail images into each thumbnail holder.
								PolyImporter.GetComponent<PolyTinker> ().LoadThumbnails (thumbnails);
								return;
							} else {
//								userHasTappedScreenInBoundingBox = false;
							}
						}
					}
				} else if (hitInfo.collider.gameObject.CompareTag ("Thumbnail") && (Input.touchCount > 0) && !importing) {
					var touch = Input.GetTouch (0);
					if (touch.phase == TouchPhase.Began) {
//						PolyImporter.GetComponent<PolyTinker> ().GrabAsset ();	// Import run time asset.
						if (hitInfo.collider.gameObject.name == "Thumbnail") {
							PolyImporter.GetComponent<PolyTinker> ().GrabAsset (
								PolyImporter.GetComponent<PolyTinker> ().assetTest [0]
							);	
							importing = true;
						} else if (hitInfo.collider.gameObject.name.Contains ("Thumbnail1")) {
							PolyImporter.GetComponent<PolyTinker> ().GrabAsset (
								PolyImporter.GetComponent<PolyTinker> ().assetTest [1]
							);	
							importing = true;
						} else if (hitInfo.collider.gameObject.name.Contains ("Thumbnail2")) {
							PolyImporter.GetComponent<PolyTinker> ().GrabAsset (
								PolyImporter.GetComponent<PolyTinker> ().assetTest [2]
							);	
							importing = true;
						} else if (hitInfo.collider.gameObject.name.Contains ("Thumbnail3")) {
							PolyImporter.GetComponent<PolyTinker> ().GrabAsset (
								PolyImporter.GetComponent<PolyTinker> ().assetTest [3]
							);	
							importing = true;
						}
						// Grab asset of featured image thumbnail.
					}
				}
			}		
		}	// end update method
			
	}	// end class
}	// end namespace

