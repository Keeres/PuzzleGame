using UnityEngine;
using System.Collections;

public class Grids : MonoBehaviour {

	// The Grid itself
	public static int w = 11;
	public static int h = 16;
	public static Transform[,] grid = new Transform[w, h];
	public static int[,] gridColor = new int[w, h];
	public static bool[,] movable = new bool[w, h];

	void Awake(){
		for (int i=0; i<w; i++) {
			for (int j=0; j<h; j++) {
				gridColor[i, j] = -10;
				movable[i, j] = true;
			}
		}
	}

	public static Vector2 roundVec2(Vector2 v) {
		return new Vector2(Mathf.Floor(v.x),
		                   Mathf.Round(v.y));
	}

	public static Vector2 roundVec2Child(Vector2 v) {

			return new Vector2 (Mathf.RoundToInt (v.x),
		                    Mathf.FloorToInt(v.y));

	}
	/*public static bool insideBorder(Vector2 pos) {
		return ((int)pos.x >= 1 &&
		        (int)pos.x < w);
	}
*/
	public static bool insideBorder(Vector2 pos) {
		return ((int)pos.x >= 1 &&
		        (int)pos.x < w-1);
	}

	public static bool insideBottomBoundary(Vector2 pos){
		return ((int)pos.y >= 1);
	}




}
