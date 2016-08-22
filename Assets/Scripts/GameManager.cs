using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {
	
	public static float lastFall = 0.0f;
	float delay = 0.0f;
	private Vector3[] childPosition = new Vector3[4];

	// Variables for determining if the blocks are in a valid position
	private int blockIndex = 0;
	private int blockLandCount = 0;
	private bool[] blockLanded = new bool[4];

	// Variables for checking adjoining color blocks
	int blockCount = 0;
	bool[,] checkedBlock = new bool[Grids.w, Grids.h];
	int[,] clearBlockList = new int[Grids.w * Grids.h, 2];
	
	// Use this for initialization
	void Start (){
		for (int i=0; i<4; i++) {
			blockLanded [i] = false;
		}

		for (int i=0; i<Grids.h; i++) {
			for (int j=0; j<Grids.w; j++) {	
				checkedBlock [j, i] = false;
			}
		}	

		for (int i=0; i<Grids.w*Grids.h; i++) {
			clearBlockList [i, 0] = -10;
			clearBlockList [i, 1] = -10;				
		}
	}
	
	// Update is called once per frame
	void Update () {
		// Move Left
		if(SpawnCurrent.currentGroup){
			if (Input.GetKeyDown (KeyCode.LeftArrow)) {
				// Modify position
				transform.position += new Vector3 (-1, 0, 0);

				// See if valid
				if (!isValidGridPos ())
				// Its not valid. revert.
					transform.position += new Vector3 (1, 0, 0);
			}
		// Move Right
		else if (Input.GetKeyDown (KeyCode.RightArrow)) {
				// Modify position
				transform.position += new Vector3 (1, 0, 0);
			
				// See if valid
				if (!isValidGridPos ())
				// It's not valid. revert.
					transform.position += new Vector3 (-1, 0, 0);
			}
		// Rotate
		else if (Input.GetKeyDown (KeyCode.UpArrow)) {
				transform.Rotate (0, 0, -90);
			
				// See if valid
				if (!isValidGridPos ())
				// It's not valid. revert.
					transform.Rotate (0, 0, 90);
			} 
		//Move Down
		else if (Time.time - lastFall >= 1) {

				moveDown ();

				if (blockLandCount == 4) {
				
					updateScore (40);
					StartCoroutine (processBlocksFunction (0.4f));
				
					// Resets variables
					blockLandCount = 0;
					for (int i=0; i<4; i++) {
						blockLanded [i] = false;
					}
				
					//disable script
					enabled = false;
				}
				lastFall = Time.time;
			} else if (Input.GetKeyDown (KeyCode.DownArrow)) {
				// Modify position
				do {
					moveDown ();
				} while(blockLandCount != 4);
				
				// If all 4 blocks setted search for connecting same color blocks
				// and destroy them if 4 of more are connected
				if (blockLandCount == 4) {

					updateScore (40);
					searchForSameColorBlocks ();

					// Resets variables
					blockLandCount = 0;
					for (int i=0; i<4; i++) {
						blockLanded [i] = false;
					}

					//disable script
					enabled = false;
				}
			}
		}
	}

	// Is postions valid
	// Is it within boundaries and not overlapping another block
	bool isValidGridPos() {     
		foreach (Transform child in transform) {
			Vector2 v = child.position;
			Vector2 w = Grids.roundVec2 (transform.position);

			// Not inside Border?
			if (!Grids.insideBorder (w)){
				return false;
			}
			else if (!Grids.insideBottomBoundary (w))
				return false;

			// Block in grid cell
			else if (Grids.grid [Mathf.RoundToInt(v.x), Mathf.FloorToInt(v.y)] != null){
				return false;
			}
		}
		return true;
	}

	// Is postions valid when pressing down key only
	// Is it within boundaries and not overlapping another block
	bool isValidBlockPos(Vector3 childPosition){
		Vector2 v = childPosition;
		Vector2 w = Grids.roundVec2 (transform.position);

		// Not inside Border?
		if (!Grids.insideBottomBoundary (w))
			return false;
		
		// Block in grid cell
  			else if (Grids.grid [Mathf.RoundToInt(v.x), Mathf.FloorToInt(v.y)] != null)
				return false;
			
		return true;
	}

	//	Update the grid with the postions of each blocks by adding children to grid
	//	Update the color grids with the color of the blocks using the tags
	void updateGridWithBlock(Vector3 childPosition, int childNumber){
		Vector2 v = childPosition;

		Grids.grid[Mathf.RoundToInt(v.x), Mathf.FloorToInt(v.y)] = transform.GetChild(childNumber);
	//	Debug.Log ("asdf x : " + Mathf.RoundToInt (v.x) + "y : " + (int) v.y);

		if (transform.GetChild (childNumber).tag == "Orange") {
			Grids.gridColor[Mathf.RoundToInt(v.x), Mathf.FloorToInt(v.y)] = (int)Constants.Colors.orange;
		} else if (transform.GetChild (childNumber).tag == "White") {
			Grids.gridColor[Mathf.RoundToInt(v.x), Mathf.FloorToInt(v.y)] = (int)Constants.Colors.white;
		} else if (transform.GetChild (childNumber).tag == "Blue") {
			Grids.gridColor[Mathf.RoundToInt(v.x), Mathf.FloorToInt(v.y)] = (int)Constants.Colors.blue;
		} else if (transform.GetChild (childNumber).tag == "Red") {
			Grids.gridColor[Mathf.RoundToInt(v.x), Mathf.FloorToInt(v.y)] = (int)Constants.Colors.red;
		} else if (transform.GetChild (childNumber).tag == "Purple") {
			Grids.gridColor[Mathf.RoundToInt(v.x), Mathf.FloorToInt(v.y)] = (int)Constants.Colors.purple;
		} 
	}

	// Search for connecting color blocks and save their positions to a list
	// Repeats to find all connecting color blocks sets
	public void searchForSameColorBlocks(){
		blockCount = 0;

		BlockClear.ClearBlock (ref clearBlockList, ref blockCount, ref checkedBlock);
		Debug.Log ("count: " + blockCount);

		if (blockCount >= 4) {
			//More than 4 blocks are connected, add current set of array to a list to be destroyed later
			destroyBlocks(blockCount);

		}else if (blockCount > 0 && blockCount < 4) {
			//sameColorBlocks array not saved, reuse same index
			Debug.Log("BLOCK COUNT ERROR");

		} else if (blockCount == 0) {
			// Spawn next group
			StartCoroutine (delaySpawnFunction (0.0f));
			StopCoroutine (delaySpawnFunction (0.0f));

			lastFall = Time.time;
		//	Debug.Log("#########################finish");
		}

	}// close function

	//Goes through the list and destroys the blocks
	void destroyBlocks(int count){
		for (int k=0; k<count; k++) {
			if (clearBlockList [k, 0] >= 0 && clearBlockList [k, 1] >= 0) {
				ParticleSystem tempParticle = Grids.grid [clearBlockList [k, 0], clearBlockList [k, 1]].gameObject.GetComponent<ParticleSystem>();
				tempParticle.enableEmission = true;
				tempParticle.Play();

				StartCoroutine(ScaleUpOverTime(k, 1.0f));
				StartCoroutine(FadeOutOverTime(k, 0.8f));

				Destroy (Grids.grid [clearBlockList [k, 0], clearBlockList [k, 1]].gameObject, 1.5f);
				Grids.grid [clearBlockList [k, 0], clearBlockList [k, 1]] = null;
				Grids.gridColor[clearBlockList [k, 0], clearBlockList [k, 1]] = -10;
				updateScore (40);
				//if the location of a destroyed block is unvmovable,
				//(i.e special blocks that players are required to destroy),
				//set the block loaction to true after it is destroyed
				if (Grids.movable [clearBlockList [k, 0], clearBlockList [k, 1]] == false) {
					Grids.movable [clearBlockList [k, 0], clearBlockList [k, 1]] = true;
				}
			}
		}
		StartCoroutine(processBlocksFunction(0.4f));
	}	
		
	

	IEnumerator processBlocksFunction(float delayTime){
		yield return new WaitForSeconds(delayTime);
		moveBlocksDown ();
		resetVariables ();
		searchForSameColorBlocks ();
	}

	IEnumerator delaySpawnFunction(float delayTime){
		yield return new WaitForSeconds(delayTime);
		FindObjectOfType<SpawnCurrent> ().spawnCurrent ();
		FindObjectOfType<Spawner> ().spawnNext ();
	}

	IEnumerator FadeOutOverTime(int k, float time){
		Color blockColor; 
		SpriteRenderer tempRenderer = Grids.grid [clearBlockList [k, 0], clearBlockList [k, 1]].gameObject.GetComponent<SpriteRenderer>();
		blockColor = tempRenderer.color;

		float startTime = Time.time;
		for (double i=0; i<5.0; i+=0.1) {
			float t = (Time.time - startTime) / time;
			tempRenderer.color = new Color(blockColor.r, blockColor.b, blockColor.g, Mathf.SmoothStep(1.0f, 0.0f, t));
			yield return null;
		}
	}

	IEnumerator ScaleUpOverTime(int k, float time)
	{
		Transform tempTransform = Grids.grid [clearBlockList [k, 0], clearBlockList [k, 1]].gameObject.GetComponent<Transform>();
		float size = Random.Range (1.3f, 3.0f);
		Vector3 originalScale = transform.localScale;
		Vector3 destinationScale = new Vector3(size, size, size);
		
		float currentTime = 0.0f;
		
		do
		{
			tempTransform.localScale = Vector3.Lerp(originalScale, destinationScale, currentTime / time);
			currentTime += Time.deltaTime;
			yield return null;
		} while (currentTime <= time);
	
	}

	void moveBlocksDown(){
		for(int i=0 ; i<Grids.w; i++){
			for(int j=1 ; j<Grids.h; j++){
				if(Grids.grid[i, j] != null && Grids.movable[i, j] == true){
					int temp = j;
						while(Grids.grid[i,temp-1] == null && temp > 0){
				
							Grids.grid[i, temp-1] = Grids.grid[i, temp];
							Grids.gridColor[i, temp-1] = Grids.gridColor[i, temp];
							Grids.gridColor[i, temp] = -10;
							Grids.grid[i, temp] = null;

							// Update Block position
							Grids.grid[i, temp-1].position += new Vector3(0, -1, 0);
							temp = temp - 1;
							if(temp == 0)
								break;	
					};
				}
			}
		}
	}

	//Rests variables
	void resetVariables(){
		blockCount = 0;

		for (int i=0; i<Grids.h*Grids.w; i++) {
			clearBlockList[i, 0] = -10;
			clearBlockList[i, 1] = -10;
		}

		for (int i=0; i<Grids.w; i++){
			for (int j=0; j<Grids.h; j++) {
				checkedBlock [i, j] = false;
			}
		}
	}

	void updateScore(int score){
		ScoreTicker.TargetScore += score;
	}

	void moveDown(){

		transform.position += new Vector3 (0, -1, 0);

		foreach (Transform child in transform) {
			childPosition[blockIndex] = child.position;
			blockIndex++;
		}	
		blockIndex = 0;
		// See if valid
		for (int i=0; i<4; i++) {
			for (int j=0; j<4; j++) {
				
				//goes through each x and y coordinates of the 4 blocks in a group
				//Checks for vadility for the 2 blocks in the same column
				
				if (Mathf.Round (childPosition [i].x) == Mathf.Round (childPosition [j].x) && childPosition [i].y < childPosition [j].y) {
					//	Debug.Log(i + " " + Mathf.Round(childPosition[i].x) + " " +Mathf.Round(childPosition[j].x));
					//	Debug.Log(i + " " + (childPosition[i].x) + " " + (childPosition[j].x));
					//	Debug.Log(i + " " + childPosition[i].y + " " +childPosition[j].y);
					
					if (!isValidBlockPos (childPosition [i])) {
						
						// If it's not a valid positon when pressing down it means the group has reached 
						// either the bottom boundary or another block. Revert back to previous position and 
						// sets the current two blocks to that position.
						transform.GetChild (i).position += new Vector3 (0, 1, 0);
						transform.GetChild (j).position += new Vector3 (0, 1, 0);
						
						if (blockLanded [i] == false) {
							updateGridWithBlock (transform.GetChild (j).position, j);
							blockLanded [j] = true;
							blockLandCount ++;
							
							updateGridWithBlock (transform.GetChild (i).position, i);
							blockLanded [i] = true; 
							blockLandCount ++;
						}
					}
				}
			}
		}
	}
}

	