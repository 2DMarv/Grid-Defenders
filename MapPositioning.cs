using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// checks the tilemap for units that are NOT on a tilemap
// i.e. moveable players and units. the tilemap has to set a
// size beforehand and is put in manually
public class MapPositioning : MonoBehaviour {

    // enter manual map size. this map is 8 rows and 3 columns;
    public static int[,] mapPositions;
    int rows = 8;
    int columns = 3;

    // define zero points of map. zero points are the world to cell values of the left most, bottom most tile
    public static int zeroPointx = -9;
    public static int zeroPointy = -3;

    // Use this for initialization
    void Start () {
        
        mapPositions = new int[rows, columns];

        for(int i = 0; i < rows; i++) {
            for(int j = 0; j < columns; j++) {
                mapPositions[i, j] = 0;
            }
        }
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown("m")) {
            for (int i = 0; i < rows; i++) {
                for (int j = 0; j < columns; j++) {
                    print("Map Position [" + i + ", " + j + "] is: " + mapPositions[i, j]);
                }
            }
        }
    }

    // UPDATE AND CHECK POSITION FUNCTIONS
    // -- Explanation of Code --
    // we take the vector2 position of the unit and add
    // 0.5f  -- since due to tilemapping, each unit will end up
    // in a x.5 position. this ensures we always round 0.5f up
    // and prevents funky interactions with the 0 number line.
    // then we subtract from the zero point to align the matrix 
    // with the grid and take into account the [0,0] array value
    // and subtract by 1
    public static void UpdatePositionENTER(Vector2 position) {
        print((int)position.y);
        int x = (int)(position.x + 0.5f) - zeroPointx - 1;
        int y = (int)(position.y + 0.5f) - zeroPointy - 1;
        print("entering postion: [" + y + ", " + x + "]");
        mapPositions[y, x] = 1;
    }

    public static void UpdatePositionLEAVE(Vector2 position) {
        int x = (int)(position.x + 0.5f) - zeroPointx - 1;
        int y = (int)(position.y + 0.5f) - zeroPointy - 1;
        mapPositions[y, x] = 0;
    }

    public static int CheckPosition(Vector2 position) {
        int x = (int)(position.x + 0.5f) - zeroPointx - 1;
        int y = (int)(position.y + 0.5f) - zeroPointy - 1;
        print("checking postion: [" + y + ", " + x + "]");
        return mapPositions[y, x];
    }

}
