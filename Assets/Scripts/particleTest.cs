using UnityEngine;
using System.Collections;

public class particleTest : MonoBehaviour {
//	public ParticleSystem test;
//	Sprite testSprite = gameObject.GetComponent<SpriteRenderer>().sprite;
	private Color blockColor; 

	void Start () {
	//	test.enableEmission = false;
		gameObject.GetComponent<ParticleSystem>().enableEmission = false;
		blockColor = gameObject.GetComponent<SpriteRenderer>().color;
	}

	void Update(){
		if (Input.GetKeyDown ("space")) {
		//	gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
			StartCoroutine(ScaleUpOverTime(0.7f));
				StartCoroutine(FadeOutOverTime(0.7f));
			print ("space key was pressed");
			//test.enableEmission = true;
			gameObject.GetComponent<ParticleSystem>().enableEmission = true;
			gameObject.GetComponent<ParticleSystem>().Play();
			//test.Play ();	
		}

		if (Input.GetKeyDown (KeyCode.R)) {
			transform.localScale = new Vector3 (1.0f, 1.0f, 1.0f);
			gameObject.GetComponent<SpriteRenderer>().color = new Color(blockColor.r, blockColor.b, blockColor.g, 1.0f);
		}
	}

	IEnumerator ScaleUpOverTime(float time)
	{
		float size = Random.Range (1.2f, 2.5f);
		Debug.Log (size);
		Vector3 originalScale = transform.localScale;
		Vector3 destinationScale = new Vector3(size, size, size);
		
		float currentTime = 0.0f;
		
		do
		{
			transform.localScale = Vector3.Lerp(originalScale, destinationScale, currentTime / time);
			currentTime += Time.deltaTime;
			yield return null;
		} while (currentTime <= time);
		
	}



	IEnumerator FadeOutOverTime(float time){
		float startTime = Time.time;
		for (double i=0; i<5; i+=0.05) {
			float t = (Time.time - startTime) / time;
			gameObject.GetComponent<SpriteRenderer>().color = new Color(blockColor.r, blockColor.b, blockColor.g, Mathf.SmoothStep(1.0f, 0.0f, t));
			yield return null;
		}
	}
}
