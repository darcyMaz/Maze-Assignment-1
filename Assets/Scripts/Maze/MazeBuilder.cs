using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

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
    private float[] TileRotations;

    // All prefabs to spawn in to maze.
    public GameObject Full;
    public GameObject Corner;
    public GameObject Split;
    public GameObject Forward;
    public GameObject Empty;
    public GameObject End;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        MazeGenerator = new MazeGenerator(MazeSize);

        // Assign the tiles objects to the array.
        // Also assign the rotations.
        TileGameObjects = new GameObject[16];
        TileRotations = new float[16];

        // These values are not random.
        TileGameObjects[0] = Empty;
        TileRotations[0] = 0;
        TileGameObjects[1] = Split;
        TileRotations[1] = 270;
        TileGameObjects[2] = Split;
        TileRotations[2] = 180;
        TileGameObjects[3] = Corner;
        TileRotations[3] = 180;
        TileGameObjects[4] = Split;
        TileRotations[4] = 90;
        TileGameObjects[5] = Forward;
        TileRotations[5] = 0;
        TileGameObjects[6] = Corner;
        TileRotations[6] = 90;
        TileGameObjects[7] = End;
        TileRotations[7] = 180;
        TileGameObjects[8] = Split;
        TileRotations[8] = 0;
        TileGameObjects[9] = Corner;
        TileRotations[9] = 270;
        TileGameObjects[10] = Forward;
        TileRotations[10] = 90;
        TileGameObjects[11] = End;
        TileRotations[11] = 270;
        TileGameObjects[12] = Corner;
        TileRotations[12] = 0;
        TileGameObjects[13] = End;
        TileRotations[13] = 0;
        TileGameObjects[14] = End;
        TileRotations[14] = 90;
        TileGameObjects[15] = Full;
        TileRotations[15] = 0;

        BuildMaze();
    }

    // Update is called once per frame
    void Update()
    {

    }

    // I can see that this isn't quite working based on the result.
    // All walls should be doubled up. But they are not.
    // Ok yeah it's bevause I did not apply the rotation!
    public void BuildMaze()
    {
        // These are the wall specifications for the generated maze.
        //      See MazeGenerator for how these specs represent the maze.
        int[][][] MazeWallSpecs = MazeGenerator.GetMaze();

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
        //MazeDictionary MazeAsCoords = TranslateWallsToBinary(MazeWallSpecs);



        //Debug.Log(MazeAsCoords[new Tuple<int,int> (0,0)]);

        // Problem here: the int[] in MazeAsCoords are unique objects as opposed to being based on/
        // I can set the hash function actually wait

        //SpawnInMaze(MazeAsCoords);
    }

    // Returns a dictionary where the int[] key is the coordinates in the maze and the string is a four digit binary code representing a tile type and orientation.
    private MazeDictionary TranslateWallsToBinary(int[][][] mazeWallSpecs)
    {
        MazeDictionary CoordsToBinary = new MazeDictionary();

        // Go through each index in the maze and figure out what kind of wall it is.
        for (int row_index = 0; row_index < MazeSize; row_index++)
        {
            for (int col_index = 0; col_index < MazeSize; col_index++)
            {

                // Given a cell in the maze with all its walls.
                // So let's say the binary is in the following order:
                //   -        4
                //  | |      1 3        4321
                //   -        2
                // This would return: 1111
                // If we removed the right wall it would return: 1011
                // If we remove the top and bottom walls it would return: 0101

                string BinaryToAdd = "";

                // 1) Top wall
                BinaryToAdd += mazeWallSpecs[1][col_index][row_index];
                // 2) Right Wall
                BinaryToAdd += mazeWallSpecs[0][row_index][col_index + 1];
                // 3) Bottom Wall
                BinaryToAdd += mazeWallSpecs[1][col_index][row_index + 1];
                // 4) Left Wall
                BinaryToAdd += mazeWallSpecs[0][row_index][col_index];

                CoordsToBinary.Add(new Tuple<int,int> (row_index, col_index), BinaryToAdd);
            }
        }


        return CoordsToBinary;
    }


    private void SpawnInMaze(Dictionary<Tuple<int,int>, string> mazeAsCoords)
    {
        foreach (KeyValuePair<Tuple<int,int>, string> coord in mazeAsCoords)
        {
            //Debug.Log("All coords, before SpawnInMaze(): " + coord.Key[0] + ":" + coord.Key[1] + ". Binary: " + coord.Value);
        }

        // For each cell in the maze
        for (int row_index = 0; row_index < MazeSize; row_index++)
        {
            for (int col_index = 0; col_index < MazeSize; col_index++)
            {
                // Spawn in a tile corresponding to that cell's wall setup.
                string binary_ = mazeAsCoords[new Tuple<int,int> (row_index,col_index)];
                int binAsDecimal = StrBinaryToDecimal(binary_);
                Debug.Log(binAsDecimal);

                GameObject EmptyParent = new GameObject();
                EmptyParent.name = "Tile: (" + row_index + ", " + col_index + ")";

                GameObject CurrentTile = TileGameObjects[binAsDecimal];

                GameObject tile = Instantiate
                (
                    CurrentTile, 
                    new Vector3(  (row_index ) * TileSize,   0, (col_index) * TileSize), 
                    Quaternion.identity
                );
                
                tile.transform.parent = GameObject.Find(EmptyParent.name).transform;
                EmptyParent.transform.parent = GameObject.Find("Maze").transform;

                EmptyParent.transform.Rotate(0, TileRotations[binAsDecimal], 0);
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
            if (bin[unit] == '1') decimalReturn += (int) Mathf.Pow(2,unit);
        }

        return decimalReturn;
    }


    private class MazeDictionary : Dictionary<Tuple<int, int>, string>
    {
        public MazeDictionary() : base() { }

        public override int GetHashCode()
        {
            // Start with a prime number.
            int hash = 17;

            // Go through each Value checking if they are strings.
            foreach (var value in this.Values)
            {
                if (value is string wallBinary)
                {
                    // Add hashcode of string multiplied by the prime 31 to the hash.
                    hash = hash * 31 + wallBinary.GetHashCode();
                }
            }
            return hash;
        }

        // The comparison between two mazes relies on two things.
        // 1) The size. If they have the same Count then they are the same size maze.
        // 2) The values. The value at each Key represents the walls of the maze. So, if all the cells in the maze have the same walls, then the mazes are the same.
        public override bool Equals(object obj)
        {
            if (obj is not MazeDictionary other || this.Count != other.Count)
                return false;

            MazeDictionary.ValueCollection otherVals = other.Values;
            MazeDictionary.ValueCollection thisVals = this.Values;

            // Compares every value IN ORDER. That's critical for this.
            for (int i = 0; i < this.Count; i++)
            {
                if (otherVals.ElementAt(i) != thisVals.ElementAt(i)) return false;
            }
            return true;
        }
    }

}
