using UnityEngine;
public class Teleporter : MonoBehaviour
{
    public Transform exitPoint; 

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Rigidbody rb = other.GetComponent<Rigidbody>();
            rb.linearVelocity = Vector3.zero; 
            other.transform.position = exitPoint.position;
        }
    }
}