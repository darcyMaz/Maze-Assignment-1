using System;
using System.Collections.Generic;
using UnityEngine;

// Note: I don't need to do this especially with the way the tiles are but I can make it so when a tile is recorded, then it removes the walls that were recorded so that the next tile does not double up the walls.

public class MazeBuilder : MonoBehaviour
{
    // This class is for actually building the maze.

    // Accept the wall specifications from MazeGenerator. X
    // Translate those specs from wall arrays to a dictionary where coords corresponds to an integer representing the tile type. 
    // Generate in unity the tile based on the tile type in the dictionary and place it at its coordinates.
    // And create spacing for the size of the tile. X

    public int MazeSize;
    public float TileSize;
    private MazeGenerator MazeGenerator;

    // Where the CoordsToBinary object returns a string which is a 4bit binary, translate that to decimal and use that to search this array for the GameObject tile of which their are exactly 16.
    private GameObject[] TileGameObjects;

    // All 15 prefabs to spawn in to maze.
    public GameObject FullTile;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        MazeGenerator = new MazeGenerator(MazeSize);

        // Assign the tiles objects to the array.
        // These should be public assignments probably.
        // Yes they should for reusability.
        TileGameObjects = new GameObject[16];
        TileGameObjects[0] = null;
        TileGameObjects[1] = null;
        TileGameObjects[2] = null;
        TileGameObjects[3] = null;
        TileGameObjects[4] = null;
        TileGameObjects[5] = null;
        TileGameObjects[6] = null;
        TileGameObjects[7] = null;
        TileGameObjects[8] = null;
        TileGameObjects[9] = null;
        TileGameObjects[10] = null;
        TileGameObjects[11] = null;
        TileGameObjects[12] = null;
        TileGameObjects[13] = null;
        TileGameObjects[14] = null;
        TileGameObjects[15] = null;

        BuildMaze();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void BuildMaze()
    {
        // These are the wall specifications for the generated maze.
        //      See MazeGenerator for how these specs represent the maze.
        // int[][][] MazeWallSpecs = MazeGenerator.GetMaze();

        // Next step is to translate these specs to something representing a maze tile.
        //      There are six different tiles which may have different orientations.
        //      Corner, split, forward, end, full, and empty.
        // 
        // Corner has four orientations.
        // Split has four.
        // Forward has two.
        // End has four.
        // Full has one.
        // Empty has one.
        //  Sum: Sixteen possible orientations, lucky, I can use binary.

        // This is the maze. Each coordinate in the maze has a 4 bit binary as a string representing one of 16 tile types of the maze.
        // Dictionary<int[], string> MazeAsCoords = TranslateWallsToBinary(MazeWallSpecs);
        SpawnInMaze(null); // MazeAsCoords);
    }

    // Returns a dictionary where the int[] key is the coordinates in the maze and the string is a four digit binary code representing a tile type and orientation.
    private Dictionary<int[], string> TranslateWallsToBinary(int[][][] mazeWallSpecs)
    {
        Dictionary<int[], string> CoordsToBinary = new Dictionary<int[], string>();

        // Go through each index in the maze and figure out what kind of wall it is.
        for (int row_index = 0; row_index < MazeSize; row_index++)
        {
            for (int col_index = 0; col_index < MazeSize; col_index++)
            {

                // Given a cell in the maze with all its walls.
                // So let's say the binary is in the following order:
                //   -        1
                //  | |      4 2        Clockwise!
                //   -        3
                // This would return: 1111
                // If we removed the right wall it would return: 1011
                // If we remove the top and bottom walls it would return: 0101

                string BinaryToAdd = "";

                // 1) Top wall
                BinaryToAdd += mazeWallSpecs[0][col_index][row_index];
                // 2) Right Wall
                BinaryToAdd += mazeWallSpecs[1][row_index][col_index + 1];
                // 3) Bottom Wall
                BinaryToAdd += mazeWallSpecs[0][col_index][row_index + 1];
                // 4) Left Wall
                BinaryToAdd += mazeWallSpecs[1][row_index][col_index];

                int[] NewCoords = { row_index, col_index };
                CoordsToBinary.Add(NewCoords, BinaryToAdd);
            }
        }


        return CoordsToBinary;
    }


    private void SpawnInMaze(Dictionary<int[], string> mazeAsCoords)
    {
        for (int row_index = 0; row_index < MazeSize; row_index++)
        {
            for (int col_index = 0; col_index < MazeSize; col_index++)
            {
                GameObject tile = Instantiate
                (
                    FullTile, 
                    new Vector3(  (row_index ) * TileSize,   0, (col_index) * TileSize), 
                    Quaternion.identity
                );
                tile.transform.parent = GameObject.Find("Maze").transform;
            }
        }
    }

    private int StrBinaryToDecimal(string bin)
    {
        if (bin.Length != 4)
        {
            throw new Exception("A binary string was taken into this function but is not 4bit (of length four).");
        }

        int decimalReturn = 0;

        for (int unit = 0; unit<bin.Length; unit++)
        {
            if (bin[unit] == 1) decimalReturn += (int) Mathf.Pow(unit,2);
        }

        return decimalReturn;
    }

}
