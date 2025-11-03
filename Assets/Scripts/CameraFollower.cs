using UnityEngine;

public class CameraFollower : MonoBehaviour
{

    // Value assigned in inspector.
    public Transform characterTransform;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // I could use the projection from the camera's point of view, of where the player is on the screen to get these offsets to be exactly where the player is to center the cam.
        // Could also be possible that this idea has to be implemented in the CapsuleController script.
        transform.position = new Vector3(characterTransform.position.x, 4, characterTransform.position.z - 20);
        
    }
}
