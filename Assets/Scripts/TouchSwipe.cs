using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TouchSwipe : MonoBehaviour {
	
	public float comfortZoneHorizontalSwipe = 50; // the horizontal swipe will have to be inside a 50 pixels vertical boundary
	public float minSwipeDistance = 14; // the swipe distance will have to be longer than this for it to be considered a swipe
	
	//the following 4 variables are used in some cases that I don’t want my character to be allowed to move on the board (it’s a board game)
	public float startTime;
	public Vector2 startPos;
	public float maxSwipeTime;

	//Canvas Components
	public Component[] canvasComponent;

	void Start(){
		canvasComponent = GetComponentsInChildren<Canvas> ();
		Debug.Log (canvasComponent.Length);
	}

	void Update () {
		// Handle native touch events
		foreach (Touch touch in Input.touches) {
			HandleTouch (touch.fingerId, Camera.main.ScreenToWorldPoint (touch.position), touch.phase);
		}
		
		// Simulate touch events from mouse events
		if (Input.touchCount == 0) {
			if (Input.GetMouseButtonDown (0)) {
				HandleTouch (10, Camera.main.ScreenToWorldPoint (Input.mousePosition), TouchPhase.Began);
			}
			if (Input.GetMouseButton (0)) {
				HandleTouch (10, Camera.main.ScreenToWorldPoint (Input.mousePosition), TouchPhase.Moved);
			}
			if (Input.GetMouseButtonUp (0)) {
				HandleTouch (10, Camera.main.ScreenToWorldPoint (Input.mousePosition), TouchPhase.Ended);
			}
		}
	}
	
	private void HandleTouch(int touchFingerId, Vector2 touchPosition, TouchPhase touchPhase) {
		switch (touchPhase) {
			
		case TouchPhase.Began:
			startTime = Time.time;
			startPos = touchPosition;
				Debug.Log ("touch begin");
			
			break;
			
		case TouchPhase.Moved:
			Vector2 touchDeltaPosition = startPos-touchPosition;
			startPos = touchPosition;
		//	Debug.Log("Move");
		//	GameObject backgrounds = GameObject.Find("Backgrounds");
			
			Vector3 xShift =new Vector3 (touchDeltaPosition.x, 0.0f, 0.0f);
		//	backgrounds.transform.position += xShift;
			canvasComponent[0].transform.position -= xShift;

			break;
			
		case TouchPhase.Ended:
			//	Debug.Log ("touch end");
			
			float swipeTime = Time.time - startTime;
			float swipeDist = (touchPosition - startPos).magnitude;
			//	Debug.Log (swipeTime);
				Debug.Log (swipeDist);
			
			if ((Mathf.Abs (touchPosition.y - startPos.y)) < comfortZoneHorizontalSwipe && (swipeTime < maxSwipeTime) && (swipeDist > minSwipeDistance) && Mathf.Sign (touchPosition.x - startPos.x) < 0.0f) {
				Debug.Log ("left");
			}
			
			if ((Mathf.Abs (touchPosition.y - startPos.y)) < comfortZoneHorizontalSwipe && (swipeTime < maxSwipeTime) && (swipeDist > minSwipeDistance) && Mathf.Sign (touchPosition.x - startPos.x) > 0.0f) {
				Debug.Log ("right");
				
			}
			break; 
			
		}
	}
}