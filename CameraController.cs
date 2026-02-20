using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player; 
    public float height = 10f; 
    public float distance = 8f; 

    void LateUpdate()
    {
        if (player != null)
        {
            Vector3 newPosition = player.transform.position - Vector3.forward * distance + Vector3.up * height;
            transform.position = newPosition;

            transform.LookAt(player.transform);
        }
    }
}