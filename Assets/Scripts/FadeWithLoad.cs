using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FadeWithLoad : MonoBehaviour {
	
	public float fadeSpeed = 1.0f;
	CanvasGroup canvasTarget;
	public GameObject loadingCanvas;
	public Slider loadingBar;
	
	public void fadeIn(){
		loadingCanvas.SetActive (true);
		canvasTarget = loadingCanvas.GetComponent<CanvasGroup>();
		canvasTarget.alpha = 0.0f; 
		
		StartCoroutine (fadeInOverTime (fadeSpeed));	
	}
	
	IEnumerator fadeInOverTime(float speed){
		while (canvasTarget.alpha < 1.0f) {
			canvasTarget.alpha += fadeSpeed * Time.deltaTime; 
		//	Debug.Log(canvasTarget.alpha);
			yield return null;
		}
	}
	
	public void fadeOut(){
		loadingBar.value = 1.0f;
		canvasTarget = loadingCanvas.GetComponent<CanvasGroup>();
		canvasTarget.alpha = 1.0f; 
		
		StartCoroutine (fadeOutOverTime (fadeSpeed));	
	}
	
	IEnumerator fadeOutOverTime(float speed){
		yield return new WaitForSeconds (0.5f);
		while (canvasTarget.alpha > 0.0f) {
			canvasTarget.alpha -= fadeSpeed * Time.deltaTime; 
			yield return null;
		}
		loadingCanvas.SetActive (false);
	}
}

