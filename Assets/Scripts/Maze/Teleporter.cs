using UnityEngine;

public class Teleporter : MonoBehaviour
{
    public MazeBuilder MazeBuilder;
    bool canTeleport;

    private void Start()
    {
        canTeleport = true;
    }

    public void TeleportSwitch(bool switch_)
    {
        canTeleport = switch_;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player" && canTeleport)
        {
            //  Get the Player component                 Set the player's position to the maze's starting point.
            other.gameObject.GetComponent<Player>()     .SetPosition(MazeBuilder.GetStartOfMaze());
        }
    }
}
