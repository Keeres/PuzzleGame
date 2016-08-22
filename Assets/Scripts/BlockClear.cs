using UnityEngine;
using System.Collections;

public class BlockClear : MonoBehaviour {

	public static void ClearBlock(ref int [,]_clearBlockList, ref int count, ref bool [,] _checkedBlock){
		int tempColor;
		int index = 0;

		for (int j=0; j<Grids.h; j++) {
			for (int i=1; i<Grids.w; i++) {	
				//Checks each grid point one by one to determine is a 4 same colored blocks formed square is present
				// ex.
				// _isSquare is returned true if:
				// red  red
				// red  red
				// _isSquare is returned false if:
				// red  red
				// red  blue


				if (Grids.gridColor [i, j] > 0 && _checkedBlock [i, j] == false) {
					tempColor = Grids.gridColor [i, j];

					//bottom left corner
					if (i == 1 && j == 0) {
						_checkedBlock [i, j] = true;	
						if ((int)Grids.gridColor [i + 1, j] == tempColor && (int)Grids.gridColor [i, j + 1] == tempColor && (int)Grids.gridColor [i + 1, j + 1] == tempColor) {
							_clearBlockList[index, 0] = i;
							_clearBlockList[index, 1] = j;
							index++;
							count++;
						}
					}

					//bottom right corner
					else if (i == Grids.w-1 && j == 0) {
						_checkedBlock [i, j] = true;
						if (((int)Grids.gridColor [i - 1, j] == tempColor && (int)Grids.gridColor [i, j + 1] == tempColor) &&(int)Grids.gridColor [i - 1, j + 1] == tempColor) {
							_clearBlockList[index, 0] = i;
							_clearBlockList[index, 1] = j;
							index++;
							count++;
						}
					}

					//top left corner
					else if (i == 1 && j == Grids.h) {
						_checkedBlock [i, j] = true;
						if (((int)Grids.gridColor [i + 1, j] == tempColor && (int)Grids.gridColor [i, j - 1] == tempColor) && (int)Grids.gridColor [i + 1, j - 1] == tempColor) {
							_clearBlockList[index, 0] = i;
							_clearBlockList[index, 1] = j;
							index++;
							count++;
						}
					}

					//top right corner
					else if (i == 1 && j == Grids.h) {
						_checkedBlock [i, j] = true;
						if ((int)Grids.gridColor [i - 1, j] == tempColor && (int)Grids.gridColor [i, j - 1] == tempColor && (int)Grids.gridColor [i - 1, j - 1] == tempColor) {
							_clearBlockList[index, 0] = i;
							_clearBlockList[index, 1] = j;
							index++;
							count++;
						}
					}

					//bottom boundary
					else if (j == 0) {
						_checkedBlock [i, j] = true;	
						if (Grids.gridColor [i + 1, j] == tempColor && Grids.gridColor [i, j + 1] == tempColor && Grids.gridColor [i + 1, j + 1] == tempColor) {
							_clearBlockList[index, 0] = i;
							_clearBlockList[index, 1] = j;
							index++;
							count++;
						}
						else if (Grids.gridColor [i - 1, j] == tempColor && Grids.gridColor [i, j + 1] == tempColor && Grids.gridColor [i - 1, j + 1] == tempColor) {
							_clearBlockList[index, 0] = i;
							_clearBlockList[index, 1] = j;
							index++;
							count++;
						}
					}

					//top boundary
					else if (j == Grids.h - 1) {
						_checkedBlock [i, j] = true;	
						if (Grids.gridColor [i + 1, j] == tempColor &&  Grids.gridColor [i, j - 1] == tempColor && Grids.gridColor [i + 1, j - 1] == tempColor) {
							_clearBlockList[index, 0] = i;
							_clearBlockList[index, 1] = j;
							index++;
							count++;
						}
						else if (Grids.gridColor [i - 1, j] == tempColor &&  Grids.gridColor [i, j - 1] == tempColor && Grids.gridColor [i - 1, j - 1] == tempColor) {
							_clearBlockList[index, 0] = i;
							_clearBlockList[index, 1] = j;
							index++;
							count++;
						}
					}

					//left boundary
					else if (i == 1) {
						_checkedBlock [i, j] = true;	
						if (Grids.gridColor [i + 1, j] == tempColor && Grids.gridColor [i, j + 1] == tempColor && Grids.gridColor [i + 1, j + 1] == tempColor) {
							_clearBlockList[index, 0] = i;
							_clearBlockList[index, 1] = j;
							index++;
							count++;
						}
						else if (Grids.gridColor [i + 1, j] == tempColor && Grids.gridColor [i, j - 1] == tempColor && Grids.gridColor [i + 1, j - 1] == tempColor) {
							_clearBlockList[index, 0] = i;
							_clearBlockList[index, 1] = j;
							index++;
							count++;
						}
					}

					//Right boundary
					else if (i == Grids.w-1) {
						_checkedBlock [i, j] = true;	
						if (Grids.gridColor [i - 1, j] == tempColor && Grids.gridColor [i, j + 1] == tempColor && Grids.gridColor [i - 1, j + 1] == tempColor) {
							_clearBlockList[index, 0] = i;
							_clearBlockList[index, 1] = j;
							index++;
							count++;
						}
						else if (Grids.gridColor [i - 1, j] == tempColor && Grids.gridColor [i, j - 1] == tempColor && Grids.gridColor [i - 1, j - 1] == tempColor) {
							_clearBlockList[index, 0] = i;
							_clearBlockList[index, 1] = j;
							index++;
							count++;
						}
					}

					//Rest
					else {
						_checkedBlock [i, j] = true;	
						if (Grids.gridColor [i - 1, j] == tempColor && Grids.gridColor [i, j + 1] == tempColor && Grids.gridColor [i - 1, j + 1] == tempColor) {			
							_clearBlockList[index, 0] = i;
							_clearBlockList[index, 1] = j;
							index++;
							count++;
						}
						else if (Grids.gridColor [i + 1, j] == tempColor && Grids.gridColor [i, j + 1] == tempColor && Grids.gridColor [i + 1, j + 1] == tempColor) {			
							_clearBlockList[index, 0] = i;
							_clearBlockList[index, 1] = j;
							index++;
							count++;
						}
						else if (Grids.gridColor [i + 1, j] == tempColor && Grids.gridColor [i, j - 1] == tempColor && Grids.gridColor [i + 1, j - 1] == tempColor) {			
							_clearBlockList[index, 0] = i;
							_clearBlockList[index, 1] = j;
							index++;
							count++;
						}
						else if (Grids.gridColor [i - 1, j] == tempColor && Grids.gridColor [i, j - 1] == tempColor && Grids.gridColor [i - 1, j - 1] == tempColor) {			
							_clearBlockList[index, 0] = i;
							_clearBlockList[index, 1] = j;
							index++;
							count++;
						}
					}
				}
			}// i
		}// j
	}
}
















