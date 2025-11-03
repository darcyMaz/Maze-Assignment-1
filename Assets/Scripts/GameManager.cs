using System;
using System.Collections.ObjectModel;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // GM connects to everyone
    // List of keys
    // List of animations
    // Player's values (health, original position)


    // Objects that GM connects to.
    public GameObject player_go;
    Player player;
    CharacterController player_cc;
    public Key key1;
    public Key key2;
    public Key key3;
    public Endzone endzone;
    Collection<Key> keys;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = player_go.GetComponent<Player>();
        player_cc = player_go.GetComponent<CharacterController>();
        keys = new Collection<Key>();
        keys.Add(key1);
        keys.Add(key2);
        keys.Add(key3);
    }

    // Update is called once per frame
    void Update()
    {
        // Check if keys are collected or not.
        // If endzone does not know that keys are collected, then we want to call this. 
        //      Prevents a call every frame of this function after keys are collected. Only want the call once.
        if (key1.IsCollected() && key2.IsCollected() && key3.IsCollected() && !endzone.AreKeysCollected())
        {
            endzone.AllKeysCollected();
        }

        // If you reach the EndZone having collected all of the keys, you win!
        if (endzone.IsGameOver())
        {
            Debug.Log("You collected all the keys and returned to the EndZone. You won this game!");
            ResetGame();
        }
        // If the player's health goes below zero, the whole game is reset. You lose :(
        else if ( player.GetHealth() <= 0 )
        {
            Debug.Log("The player's health has reached zero, that means you died :(");
            Debug.Log("The game will not be reset from the start.");
            ResetGame();
        }
    }

    public void ResetGame()
    {
        // call reset function for key collection, player, and animations.
        endzone.ResetEndzone();
        foreach (Key key in keys) key.ResetKey();
        player.ResetPlayer();
    }

}
