using UnityEngine;
using System.Collections;

public class SpawnCurrent : MonoBehaviour {
	public static GameObject currentGroup;
	public GameObject[] groups;

	// Use this for initialization
	void Start () {

	}	
	
	public void spawnCurrent() {
		Destroy (Spawner.nextGroup);
		currentGroup = Instantiate(groups[Spawner.i], transform.position, Quaternion.identity) as GameObject;
		currentGroup.transform.position = new Vector3 (5.5f, 15.0f, 0.0f);
		currentGroup.AddComponent <GameManager>();

		checkForOverlap ();
	}

	public void checkForOverlap(){
		foreach (Transform child in currentGroup.transform) {	
			Vector2 v = child.position;
			if (Grids.grid [Mathf.RoundToInt(v.x), Mathf.FloorToInt(v.y)] != null) 
			{		
				print ("game over");
				currentGroup.GetComponent<GameManager>().enabled = false;
				break;
			}
		}
	}
}
