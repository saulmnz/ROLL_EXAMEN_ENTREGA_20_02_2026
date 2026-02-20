using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 10.0f; 
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        // PA PILLAR LAS TECLAS WASD
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        // VECTOR MOVIMIENTO
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        // FUERZA FÍSICA A LA BOLACH
        rb.AddForce(movement * speed);
    }
}