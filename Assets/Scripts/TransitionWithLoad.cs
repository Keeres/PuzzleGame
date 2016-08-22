using UnityEngine;
using System.Collections;

public class TransitionWithLoad : MonoBehaviour {

	public FadeWithLoad fade;
	public GameObject loadingCanvas;

	// Use this for initialization

	void Start () {
		loadingCanvas.SetActive (true);
		fade.fadeOut ();
	}
}
