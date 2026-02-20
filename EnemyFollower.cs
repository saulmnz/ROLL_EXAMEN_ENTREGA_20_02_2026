
using UnityEngine;

public class EnemyFollower : MonoBehaviour
{
    // REFERENCIAMOS AL JUGADOR / LA BOLA
    public Transform player;
    
    // DISTANCIA / RANGO A LA QUE EMPIEZA A PERSEGUIR
    public float detectionRange = 5f;
    
    // VELOCIDAD DE PERSECUCIÓN
    public float speed = 3f;

    // VARIABLE PARA GUARDAR EL ESTADO ACTUAL / SI ESTÁ LEJOS O CERCA DEL JUGADOR
    private string currentState = "LEJOS"; 

    void Update()
    {
        // SI NO SE ASIGNA EL JUGADOR NO SE HACE NADA
        if (player == null)
        {
            Debug.LogWarning("ASIGNA JUGADOR");
            return;
        }

        // CALCULAR DISTANCIA ENTRE CUBO Y BOLA
        float distance = Vector3.Distance(transform.position, player.position);

        // LÓGICA DE LOS ESTADOS LEJOS Y CERCA
        if (distance > detectionRange)
        {
            // LEJOS
            currentState = "LEJOS";
            // EL CUBO NO SE MUEVE
            Debug.Log("ESTADO: LEJOS - EL CUBO ESTÁ QUIETO NO SE MUEVEEEE");
        }
        else
        {
            // CERCA
            currentState = "CERCA";
            
            // DIRECCIÓNM HACIA EL JUGADOR
            Vector3 direction = (player.position - transform.position).normalized;
            
            // MOVIMIENTO DEL CUBO HACIA EL JUGADOR
            transform.position += direction * speed * Time.deltaTime;
            
            Debug.Log("ESTADO: CERCA - EL CUBO ESTA PERSIGUIENDO A LA BOLA");
        }
    }
}


