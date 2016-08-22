using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScaleFont : MonoBehaviour {
	
	public Vector2 offset;
	public Text next;
	public float ratio = 10;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnGUI(){
		
		float finalSize = (float)Screen.width/ratio;
		next.fontSize = (int)finalSize;
		next.transform.position = new Vector2( offset.x * Screen.width, offset.y * Screen.height);
	}
	
	
}