## README TUTORIAL!! ⭕


### ***COMO SE VE MI TABLERO DE JUEGO ( SUPER CHULO ) 🌈***

<img width="700" height="400" alt="image" src="https://github.com/user-attachments/assets/c2ebf4ce-e6b2-483f-ab90-beb5fbf43022" />


> [!NOTE]
> ***EN EL TABLERO SE PUEDE OBSERVAR COMO HAY UN CAMPO EN EL QUE EXISTE UN JUGADOR (LA BOLA) Y UN ENEMIGO (EL CUBO), EL JUEGO CONSISTE EN TRATAR DE HUIR DEL ENEMIGO RECOLECTANDO 5 PIRÁMIDES PARA PODER GANAR LA PARTIDA EN UN TIEMPO LÍMITE DE 60 SEGUNDOS***

<img width="700" height="400" alt="image" src="https://github.com/user-attachments/assets/c2dfb7fe-56ef-43a3-8fc8-61b3434daa8d" />

### CONFIGURACIONES PRINCIPALES 🔻

#### SOBRE EL JUGADOR 🔺

>[!NOTE]
> ***ES UN OBJETO 3D EN FORMA DE ESFERA***

- ***HE AÑADIDO LA CONDICIÓN `RIGIBODY` PARA QUE DETECTE LAS FÍSICAS, GRAVEDAD, MASA...***
- ***HE AÑADIDO UN `MATERIAL` PARA DARLE COLOR AL OBJETO ( EN ESTE CASO = AZUL )***
- ***CONTIENE UN SCRIPT PARA EL MOVIMIENTO POR TECLAS AWSD O FLECHAS***

```C#
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
```

---

#### TELETRANSPORTE 🔻

>[!NOTE]
> ***HE IMPLEMENTADO ESTA NUEVA FUNCIONALIDAD, MEDIANTE UN OBJETO (CILINDRO), CONFIGURO EL COLLIDER, PARA QUE CUANDO ENTRE EN CONTACTO CON EL JUGADOR SE TELETRANSPORTE A UN ENDPOINT QUE CONFIGURO EN EL MAPA***

- ***AL CILINDRO (AGUJERO) SE LE AÑADE EL SCRIPT DE TELEPORT, DENTRO DEL INSPECTOR DEL CILINDRO CONFIGURAMOS EL SCRIPT PARA AÑADIRLE EL ENDPOINT QUE DECIDIMOS QUE TENGA***
- ***HE AÑADIDO UN MATERIAL ROSA (PA QUE ESTÉ BONITO)***

<img width="700" height="400" alt="image" src="https://github.com/user-attachments/assets/dd98a752-cde4-45e9-bb02-663b3f02f31a" />

<img width="700" height="400" alt="image" src="https://github.com/user-attachments/assets/c73d3745-3a59-4c2c-bab9-8eda5611791d" />


```C#

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

```

---

#### OBJETOS QUE SE PUEDEN PICKEAR 🔺

>[!NOTE]
> ***YA QUE NO ME GUSTABA USAR UN CUBO O CUALQUIER OBJETO DEFAULT QUE TRAE UNITY, HE IDO A LA UNITY STORE Y HE DESCARGADO UN PAQUETE QUE ME TRAE FORMAS DIFERENTES, EN ESTE CASO, LA PIRÁMIDE***

- ***SOBRE ESTE OBJETO SE LE AÑADE EL SCRIPT `PickUp`, EL CUAL TENEMOS QUE CONFIGURAR SU `ScoreValue` YA QUE SERÁ LA SUMA DE PUNTOS QUE SE NOS ACUMULARÁ AL RECOGER EL OBJETO CON EL JUGADOR***

```C#
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
```

---

#### ELEMENTO ROTATORIO 🔻

>[!NOTE]
> ***HE CONFIGURADO UN CUBO (LO HE ESTIRADO PARA QUE TUVIERA FORMA DE PARED), PARA QUE GIRE SOBRE SI MISMO, EMPUJANDO LA BOLA SI ENTRA EN CONTACTO Y ALEJANDOLA DE SU POSIBLE OBJETIVO***

- ***SOBRE ESTE OBJETO QUE LE HE LLAMADO `Helice` LE AÑADO EL SCRIPT ROTATOR***
- ***TAMBIÉN LE AÑADO MATERIAL PARA QUE TENGA COLOR***

```C#
using UnityEngine;
public class Rotator : MonoBehaviour
{
    public Vector3 rotationSpeed = new Vector3(0, 100, 0);
    void Update()
    {
        transform.Rotate(rotationSpeed * Time.deltaTime);
    }
}
```

> ***HE PUESTO DOS CAPTURAS PARA QUE SE VEA QUE CAMBIA SU POSICIÓN (ACTÚA COMO HÉLICE)***

<img width="700" height="400" alt="image" src="https://github.com/user-attachments/assets/bf9bd766-51ad-4b7a-b1f1-ab13b6a5bf3c" />

<img width="700" height="400" alt="image" src="https://github.com/user-attachments/assets/6c6f1536-8f6c-46bf-90c9-233e99e1279a" />

---


#### CÁMARA 🔻

> [!NOTE]
> ***SOBRE LA MAIN CÁMARA HE AÑADIDO UN SCRIPT PARA AJUSTARLA Y QUE SE PUEDA VER BIEN DESDE ARRIBA EL JUGADOR ( ADEMÁS DE SEGUIR SU MOVIMIENTO)***

```C#
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
```

---


### SOBRE EL ENEMIGO 🔺

> [!NOTE]
> ***ES UN OBJETO 3D EN FORMA DE CUADRADO, EL ENEMIGO VA A TENER UN RANGO DE DETECCION (EL CUAL ES ELEVADO PARA QUE LE SIGA POR PRÁCTICAMENTE TODO EL MAPA), UTILIZA DOS SCRIPTS, UNO PARA DETECTAR LA COLISIÓN Y OTRO PARA SEGUIR AL JUGADOR (LA BOLA)***

- ***SE LE HA AÑADIDO LA MATERIAL PARA QUE TENGA COLOR***
- ***SOBRE EL SCRIPT `EnemyFollower` EXISTEN VARIABLES PARA AJUSTAR SU RANGO DE DETECCIÓN, AJUSTARLE AL OBJETO QUE QUIERE SEGUIR (EL JUGADOR) Y LA VELOCIDAD DEL ENEMIGO***

<img width="700" height="400" alt="image" src="https://github.com/user-attachments/assets/3c4e4fe7-1904-48ed-bfc5-72fb4ebf2609" />


---

#### GAMEMANAGER Y CANVAS 🔻

>[!NOTE]
> ***CUANDO SE INICIA EL JUEGO TENEMOS EL OBJETIVO DE CONSEGUIR LA PUNTUACIÓN EN EL TIEMPO LÍMITE, PARA QUE ESTO LO SEPA EL USUARIO HE CONFIGURADO UN SCRIPT QUE LE DE LÓGICA AL JUEGO Y UNOS TEXTOS Y BOTONES (REINCIIAR) CANVA PARA MOSTRAR CUANDO SE PIERDE O GANA EL JUEGO.***


- ***SOBRE EL TEXTO SCORE: LE MODIFICO LA POSICIÓN (TOP-LEFT), EL CONTENIDO DEL TEXTO, EL ESTILO DE LETRA, TAMAÑO...**

<img width="700" height="700" alt="image" src="https://github.com/user-attachments/assets/00381661-ad9b-4e3a-b938-2fea0f4f3c54" />

> ***SOBRE EL RESTO DE TEXTOS HE CONFIGURADO LO MISMO SOLO QUE EN DISTINTAS POSICIONES**

- ***ACLARAR QUE EL BUTTON HAY QUE OCULTARLO PARA QUE NO SALGA EN PANTALLA TODO EL RATO, SOLO CUANDO SE GANA O CUANDO SE PIERDE***
  
<img width="700" height="400" alt="image" src="https://github.com/user-attachments/assets/552f4785-0a3f-42dd-8966-dcd5a26536f6" />

>[!CAUTION]
>***TODO ESTE TEXTO ES FUNCIONAL Y TIENE LÓGICA DETRAS, ESTÁ DEFINIDA EN EL SCRIPT `GameManager`***

```C#
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int totalScore = 0;
    public int winningScore = 5;
    public bool isGameActive = true;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOverText;
    public Button restartButton;

    public float timeRemaining = 60f;
    public TextMeshProUGUI timerText;
    private bool timerIsRunning = false;

    void Awake()
    {
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
        if (gameOverText != null)
            gameOverText.gameObject.SetActive(false);
        
        if (restartButton != null)
            restartButton.gameObject.SetActive(false);
        
        UpdateScoreText();
        
        timerIsRunning = true;
    }

    void Update()
    {
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

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
```




  
