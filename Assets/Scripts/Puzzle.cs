using UnityEngine;
using System.Collections;

public class Puzzle : MonoBehaviour {

	public string color;
	public static int x;
	public static int y;

	// Use this for initialization
	void Start () {

		x = Mathf.RoundToInt(transform.position.x);
		y = Mathf.FloorToInt(transform.position.y);
		//Debug.Log (x);
		//Debug.Log (y);

		Grids.grid [x, y] = gameObject.transform;
		Grids.movable [x, y] = false;

		if (color == "white") {
			Grids.gridColor [x, y] = (int)Constants.Colors.white;	
		} else if (color == "orange") {
			Grids.gridColor [x, y] = (int)Constants.Colors.orange;	
		} else if (color == "blue") {
			Grids.gridColor [x, y] = (int)Constants.Colors.blue;	
		}
	}
}
