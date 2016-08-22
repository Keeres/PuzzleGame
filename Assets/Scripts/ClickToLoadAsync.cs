using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ClickToLoadAsync : MonoBehaviour {
	public Slider loadingBar;
	private AsyncOperation async;
	public FadeWithLoad fade;

	public void ClickAsync(int level){
		fade.fadeIn ();
		StartCoroutine (LoadLevelWithBar (level));
	}

	IEnumerator LoadLevelWithBar(int level){
		yield return new WaitForSeconds (fade.fadeSpeed);
		async = Application.LoadLevelAsync (level);
		while (!async.isDone) {
			loadingBar.value = async.progress;
			yield return null;
		}
	}
}
