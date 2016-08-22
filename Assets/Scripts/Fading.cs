using UnityEngine;
using System.Collections;

public class Fading : MonoBehaviour {

	public float fadeSpeed	;
	CanvasGroup canvasTarget;
	public GameObject loadingCanvas;

	public void fadeIn(){
		loadingCanvas.SetActive (true);
		canvasTarget = loadingCanvas.GetComponent<CanvasGroup>();
		canvasTarget.alpha = 0.0f; 
		StartCoroutine (fadeInOverTime (fadeSpeed));	
	}
	
	IEnumerator fadeInOverTime(float speed){
		while (canvasTarget.alpha < 1.0f) {
			canvasTarget.alpha += fadeSpeed * Time.deltaTime; 

			yield return null;
		}
	}
	
	public void fadeOut(){
		canvasTarget = loadingCanvas.GetComponent<CanvasGroup>();
		canvasTarget.alpha = 1.0f; 
		
		StartCoroutine (fadeOutOverTime (fadeSpeed));	
	}
	
	IEnumerator fadeOutOverTime(float speed){
		while (canvasTarget.alpha > 0.0f) {
			canvasTarget.alpha -= fadeSpeed*2 * Time.deltaTime; 
		//	Debug.Log(canvasTarget.alpha);
			yield return null;
		}
		loadingCanvas.SetActive (false);
	}
}
