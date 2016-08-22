using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {

	public GameObject[] groups;
	public static GameObject nextGroup;
	public static int i;

	void Start () {
		//spawns initial group
		spawnNext ();
		FindObjectOfType<SpawnCurrent> ().spawnCurrent ();
		spawnNext ();
	}

	public void spawnNext() {
		// Random Index
		i = Random.Range(0, groups.Length);

		// Spawn Group at current Position
		nextGroup = Instantiate(groups[i], transform.position, Quaternion.identity) as GameObject;
		nextGroup.transform.localScale -= new Vector3 (0.5f, 0.5f, 0.0f);
	}
}
