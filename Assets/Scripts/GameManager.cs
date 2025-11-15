using System;
using System.Collections.ObjectModel;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{

    // Objects that GM connects to.
    public GameObject player_go;
    Player player;
    CharacterController player_cc;
    
    // Keys to collect
    public Key key1;
    public Key key2;
    public Key key3;
    Collection<Key> keys;
    
    // EndZone which ends the game.
    public Endzone endzone;
    
    // Animations: Should Prob Make an Animation Manager
    public Door finalDoor;
    public MazeEntrance entrance1;
    public MazeEntrance entrance2;
    public MazeEntrance entrance3;
    private MazeEntrance[] mazeEntrances;

    // Mazes
    public MazeBuilder mazeBuilder1;
    public MazeBuilder mazeBuilder2;
    public MazeBuilder mazeBuilder3;
    private MazeBuilder[] mazeList;
    private bool Maze1Done;
    private bool Maze2Done;
    private bool Maze3Done;

    // Maze Teleporters
    public Teleporter teleporter1;
    public Teleporter teleporter2;
    public Teleporter teleporter3;


    void Start()
    {
        // Get Player components.
        player = player_go.GetComponent<Player>();
        player_cc = player_go.GetComponent<CharacterController>();
        
        // This does not seem to be necessary for now.
        keys = new Collection<Key>();
        keys.Add(key1);
        keys.Add(key2);
        keys.Add(key3);

        // Initialize the list for maze entrances.
        mazeEntrances = new MazeEntrance[] { entrance1, entrance2, entrance3 };

        // Initialize the lists and bools for the MazeBuilders.
        mazeList = new MazeBuilder[3];
        mazeList[0] = mazeBuilder1;
        mazeList[1] = mazeBuilder2;
        mazeList[2] = mazeBuilder3;
        Maze1Done = false;
        Maze2Done = false;  
        Maze3Done = false;

        // Build the mazes.
        mazeBuilder1.BuildMaze();
        mazeBuilder2.BuildMaze();
        mazeBuilder3.BuildMaze();

        // Send the keys to the mazes.
        //  Get Ending Point from each maze
        //  Send key to that point.
        key1.SetStartingPoint( mazeBuilder1.GetEndOfMaze() + new Vector3(0, 2, 0));
        key1.ResetKey();
        key2.SetStartingPoint( mazeBuilder2.GetEndOfMaze() + new Vector3(0, 2, 0));
        key2.ResetKey();
        key3.SetStartingPoint( mazeBuilder3.GetEndOfMaze() + new Vector3(0, 2, 0));
        key3.ResetKey();
    }

    // Update is called once per frame
    void Update()
    {

        
        // Check if keys are collected or not and tell the endzone, so it knows the game could end.
        // Fourth term is so that this statement is not constantly true when the keys are all collected which would constantly call this function.
        //      This statement will only be true once, when the keys are finally all collected.
        if (key1.IsCollected() && key2.IsCollected() && key3.IsCollected() && !endzone.AreKeysCollected())
        {
            endzone.AllKeysCollected();
        }

        // It would be more efficient to have an Animation Manager I think.
        // Check if the final door can be opened.
        FinalDoorCheck();

        // If you reach the EndZone having collected all of the keys, you win!
        if (endzone.IsGameOver())
        {
            Debug.Log("You collected all the keys and returned to the EndZone. You won this game!");
            ResetGame();
        }

        // This will only run once, upon the collection of Key1.
        if (key1.IsCollected() && !Maze1Done)
        {
            Maze1Done = true;
            teleporter1.TeleportSwitch(false);
            player.ResetPlayer();
        }
        // Same for these two.
        if (key2.IsCollected() && !Maze2Done)
        {
            Maze2Done = true;
            teleporter2.TeleportSwitch(false);
            player.ResetPlayer();
        }
        if (key3.IsCollected() && !Maze3Done)
        {
            Maze3Done = true;
            teleporter3.TeleportSwitch(false);
            player.ResetPlayer();
        }

        /*
        // If the player's health goes below zero, the whole game is reset. You lose :(
        else if ( player.GetHealth() <= 0 )
        {
            Debug.Log("The player's health has reached zero, that means you died :(");
            Debug.Log("The game will now be reset from the start.");
            ResetGame();
        }
        */
    }

    void FinalDoorCheck()
    {
        if (finalDoor.IsUnlocked() && endzone.AreKeysCollected())
        {
            finalDoor.OpenDoor();
        }
    }

    public void TeleportPlayerToMaze(int MazeNumber)
    {
        // Access maze, get starting position
        Vector3 mazeStartPos = mazeList[MazeNumber - 1].GetStartOfMaze();

        // player.SetPosition(that maze position);
        player.SetPosition(mazeStartPos);
    }

    public void ResetGame()
    {
        // call reset function for key collection, player, mazes, and animations.
        endzone.ResetEndzone();
        player.ResetPlayer();
        finalDoor.CloseDoor();
        foreach (MazeEntrance me in mazeEntrances) me.CloseDoor();
        foreach (MazeBuilder mb in mazeList) mb.ResetMaze();
        Maze1Done = false;
        Maze2Done = false;
        Maze3Done = false;
        key1.SetStartingPoint(mazeBuilder1.GetEndOfMaze() + new Vector3(0, 2, 0));
        key2.SetStartingPoint(mazeBuilder2.GetEndOfMaze() + new Vector3(0, 2, 0));
        key3.SetStartingPoint(mazeBuilder3.GetEndOfMaze() + new Vector3(0, 2, 0));
        key1.ResetKey();
        key2.ResetKey();
        key3.ResetKey();
        teleporter1.TeleportSwitch(true);
        teleporter2.TeleportSwitch(true);
        teleporter3.TeleportSwitch(true);
    }

}
