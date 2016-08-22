using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour {
	public Canvas canvasObject;
	public GameObject[] backgroundArray;
	public GameObject[] buttonArray;
	public static GameObject [] backgrounds = new GameObject[2];
	public static GameObject [] buttons = new GameObject[2];
	public static GameObject tempObject;
	// Use this for initialization
	void Start () {
		Debug.Log ("Start");
		for (int i=0; i<backgrounds.Length; i++) {
			//	Debug.Log(i);
			backgrounds [i] = Instantiate (backgroundArray [i], transform.position, Quaternion.identity) as GameObject;
			backgrounds [i].transform.position = new Vector2 (-168.75f + 6.9f*i, -300.0f);
			backgrounds [i].transform.SetParent (canvasObject.transform, false);

			//backgrounds [i].transform.localScale -= new Vector3 (0.5f, 0.5f, 0.0f);
			//			backgrounds = Instantiate (groups [i], transform.position, Quaternion.identity) as GameObject;
		}
		//	tempObject = Instantiate (backgroundArray [0], transform.position, Quaternion.identity) as GameObject;
	//	tempObject.transform.position = new Vector2 (-168.75f, -300.0f);

	//		tempObject.transform.SetParent(canvasObject.transform, false);

	//	}
	//	float width = backgrounds[0].GetComponent<Renderer>().bounds.size.x;
	//	Debug.Log (width);
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
