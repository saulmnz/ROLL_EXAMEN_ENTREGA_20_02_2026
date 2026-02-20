using UnityEngine;

public class PickUp : MonoBehaviour
{
    public int scoreValue = 1;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (GameManager.instance != null)
            {
                GameManager.instance.AddScore(scoreValue);
            }
            gameObject.SetActive(false);
        }
    }
}