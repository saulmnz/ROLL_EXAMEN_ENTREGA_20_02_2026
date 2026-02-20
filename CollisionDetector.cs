
using UnityEngine;

public class CollisionDetector : MonoBehaviour
{
    // LLAMAMOS A LA COLISIÓN
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("¡COLISIIIIOÓNN! EL JUGADOR/BOLA TOCÓ AL ENEMIGO/CUBO.");
        }
    }

    // SE LLAMA A LA COLISIÓN MIENTRAS DURA EL FRAME
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("ESTÁAAN COLISIONANDO... (LA BOLA SIGUE TOCANDO EL CUBOOO)");
        }
    }

    // SE LLAMA CUANDO LA COLISIÓN TERMINA
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("SE ACABÓ LA COLISIÓOONN, LA BOLA SE SEPARÓ DEL CUBO.");
        }
    }
}
