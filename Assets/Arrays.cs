using UnityEngine;
using System.Collections;

public class Arrays : MonoBehaviour {
	public GameObject[] groups;
	public static GameObject [] backgrounds = new GameObject[2];

	// Use this for initialization
	void Start () {
		Debug.Log ("Start");
		for (int i=0; i<groups.Length; i++) {
			Debug.Log(i);
			backgrounds [i] = Instantiate (groups [i], transform.position, Quaternion.identity) as GameObject;
			backgrounds [i].transform.position = new Vector2 (0.0f + 6.062f * i, 0.0f);
			//backgrounds [i].transform.localScale -= new Vector3 (0.5f, 0.5f, 0.0f);
//			backgrounds = Instantiate (groups [i], transform.position, Quaternion.identity) as GameObject;

		}
		float width = backgrounds[0].GetComponent<Renderer>().bounds.size.x;
		Debug.Log (width);

	   }

	// Update is called once per frame
	void Update () {
	
	}
}
