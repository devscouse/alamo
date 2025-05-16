using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;
    public Vector3 offset;


    // Update is called once per frame
    void LateUpdate()
    {
        // Get camera to look in the same direction as the player
        transform.Rotate(new Vector3(0, player.transform.rotation.eulerAngles.y - transform.rotation.eulerAngles.y, 0));
    }
}
