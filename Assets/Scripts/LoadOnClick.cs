using UnityEngine;
using System.Collections;

public class LoadOnClick : MonoBehaviour {

	public Fading fade;

	public void Click(int level){
		fade.fadeIn ();
		StartCoroutine (LoadLevel (level));
	}
	
	IEnumerator LoadLevel(int level){
		yield return new WaitForSeconds (0.25f);
		Application.LoadLevel (level);
	}
}
