using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public GameObject player;
    private Vector3 cameraPosition;

    private void LateUpdate()
    {
        cameraPosition = new Vector3(player.transform.position.x, 0, player.transform.position.z);//combine these two lines of code
        transform.position = cameraPosition;
    }
}
