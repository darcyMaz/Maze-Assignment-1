using System;
using UnityEngine;

public class Key : MonoBehaviour
{
    // Set starting position in inpsector.
    Vector3 starting_pos;

    void Start()
    {

        // Set the current position to the starting position.
        // transform.position = starting_pos;
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        // This makes the gameObject dissapear.
        gameObject.SetActive(false);
    }

    public Boolean IsCollected()
    {
        return !gameObject.activeSelf;
    }

    public void SetStartingPoint(Vector3 start)
    {
        starting_pos = start;
    }

    public void ResetKey()
    {
        transform.position = starting_pos;
        gameObject.SetActive(true);
    }



}
