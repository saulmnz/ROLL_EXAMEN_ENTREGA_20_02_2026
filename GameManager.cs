using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Singleton para que otros scripts (como PickUp) puedan acceder fácilmente
    public static GameManager instance;

    // Variables de puntuación y estado
    public int totalScore = 0;
    public int winningScore = 5; // CAMBIA ESTE NÚMERO según tus coleccionables
    public bool isGameActive = true;

    // Referencias UI (las asignaremos en el Inspector)
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOverText;
    public Button restartButton;

    // Variables para el temporizador (opcional, si quieres perder por tiempo)
    public float timeRemaining = 60f;
    public TextMeshProUGUI timerText;
    private bool timerIsRunning = false;

    void Awake()
    {
        // Configuración del Singleton
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        // Inicializar UI
        if (gameOverText != null)
            gameOverText.gameObject.SetActive(false);
        
        if (restartButton != null)
            restartButton.gameObject.SetActive(false);
        
        UpdateScoreText();
        
        // Iniciar temporizador
        timerIsRunning = true;
    }

    void Update()
    {
        // Control del temporizador
        if (isGameActive && timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                UpdateTimerDisplay();
            }
            else
            {
                timeRemaining = 0;
                timerIsRunning = false;
                LoseGame();
            }
        }
    }

    // Método para sumar puntos (lo llama PickUp)
    public void AddScore(int scoreToAdd)
    {
        if (isGameActive)
        {
            totalScore += scoreToAdd;
            UpdateScoreText();

            if (totalScore >= winningScore)
            {
                WinGame();
            }
        }
    }

    void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = "Puntuación: " + totalScore.ToString();
        }
    }

    void UpdateTimerDisplay()
    {
        if (timerText != null)
        {
            float minutes = Mathf.FloorToInt(timeRemaining / 60);
            float seconds = Mathf.FloorToInt(timeRemaining % 60);
            timerText.text = string.Format("Tiempo: {0:00}:{1:00}", minutes, seconds);
        }
    }

    public void WinGame()
    {
        if (isGameActive)
        {
            isGameActive = false;
            ShowGameOverMessage("¡HAS GANADO!");
            FreezePlayer();
        }
    }

    public void LoseGame()
    {
        if (isGameActive)
        {
            isGameActive = false;
            ShowGameOverMessage("HAS PERDIDO");
            FreezePlayer();
        }
    }

    void ShowGameOverMessage(string message)
    {
        if (gameOverText != null)
        {
            gameOverText.text = message;
            gameOverText.gameObject.SetActive(true);
        }
        if (restartButton != null)
        {
            restartButton.gameObject.SetActive(true);
        }
    }

    void FreezePlayer()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            PlayerController controller = player.GetComponent<PlayerController>();
            if (controller != null)
                controller.enabled = false;
            
            Rigidbody rb = player.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.linearVelocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
            }
        }
    }

    // Método para reiniciar (lo llamará el botón)
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}