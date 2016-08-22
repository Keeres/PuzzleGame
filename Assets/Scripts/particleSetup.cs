using UnityEngine;
using System.Collections;

public class particleSetup : MonoBehaviour {

	// Use this for initialization
	void Start () {
		gameObject.GetComponent<ParticleSystem>().enableEmission = false;
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
