using UnityEngine;
using System.Collections;

public class Transition : MonoBehaviour {
	public Fading fade;
	public GameObject loadingCavas;

	// Use this for initialization
	void Start () {
		loadingCavas.SetActive (true);
		fade.fadeOut ();		

	}
	void Update () {

	}
}
