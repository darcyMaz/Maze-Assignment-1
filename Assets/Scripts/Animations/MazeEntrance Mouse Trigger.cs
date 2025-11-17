using UnityEngine;

public class MazeEntranceMouseTrigger : MonoBehaviour
{
    MazeEntrance me;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        me = GetComponentInParent<MazeEntrance>();
    }

    private void OnMouseDown()
    {
        me.DoorClicked();
    }

}
