using UnityEngine;

public class TempScript : MonoBehaviour
{
    public GameObject CurrentTile;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    //void Start()
    //{
        // Debug.Log(GetComponent<MeshRenderer>().bounds.size);    
    //}

    // Update is called once per frame
    void Start()
    {
        // transform.Rotate(new Vector3(0,3f,0), Space.Self);

        //GameObject emptyGO = new GameObject("Empty Group");
        //emptyGO.transform.position = Vector3.zero;

        // GameObject the_tile = CurrentTile;
        
        
        GameObject tile = Instantiate
        (
            CurrentTile,
            new Vector3(0,0,0),
            Quaternion.identity
        );

        tile.transform.Rotate(0, 90, 0);


        //tile.transform.parent = GameObject.Find(emptyGO.name).transform;

    }
}
