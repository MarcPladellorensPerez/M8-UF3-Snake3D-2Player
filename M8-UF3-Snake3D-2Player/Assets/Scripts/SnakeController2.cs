using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class SnakeController2 : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float bodySpeed;
    [SerializeField] private float steerSpeed;
    [SerializeField] private GameObject bodyPrefab;
    [SerializeField] private GameObject applePrefab;

    public TextMeshProUGUI scoreText; // Referencia al objeto TextMeshPro que muestra la puntuación
    public TextMeshProUGUI bestScoreText; // Referencia al objeto TextMeshPro que muestra el Best Score

    private List<GameObject> bodyParts = new List<GameObject>();
    private List<Vector3> positionHistory = new List<Vector3>();
    private int applesEaten = 0;

    private Vector2 moveDirection = Vector2.zero;

    void Start()
    {
        bodyParts.Add(gameObject);
        InvokeRepeating("UpdatePositionHistory", 0f, 0.01f);
        SpawnApple();

        // Recuperar el Best Score guardado
        int bestScore = PlayerPrefs.GetInt("BestScore", 0);
        bestScoreText.text = "Best Score: " + bestScore.ToString();
    }

    void Update()
    {
        // Utilizar las teclas de flecha izquierda y derecha para girar la serpiente
        float horizontalInput = 0f;
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            horizontalInput = -1f;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            horizontalInput = 1f;
        }
        transform.Rotate(Vector3.up * horizontalInput * steerSpeed * Time.deltaTime);

        MoveBody();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            GrowSnake();
        }
    }

    void MoveBody()
    {
        transform.position += transform.forward * moveSpeed * Time.deltaTime;

        for (int i = 1; i < bodyParts.Count; i++)
        {
            Vector3 targetPosition = bodyParts[i - 1].transform.position;
            Vector3 moveDirection = targetPosition - bodyParts[i].transform.position;
            bodyParts[i].transform.position += moveDirection.normalized * bodySpeed * Time.deltaTime;
            bodyParts[i].transform.LookAt(targetPosition);
        }
    }

    void UpdatePositionHistory()
    {
        positionHistory.Insert(0, transform.position);

        if (positionHistory.Count > 500)
        {
            positionHistory.RemoveAt(positionHistory.Count - 1);
        }
    }

    void GrowSnake()
    {
        Vector3 newPosition = bodyParts[bodyParts.Count - 1].transform.position - transform.forward * 1.0f;
        GameObject newBodyPart = Instantiate(bodyPrefab, newPosition, Quaternion.identity);
        bodyParts.Add(newBodyPart);
    }

    void SpawnApple()
    {
        Vector3 randomPos = new Vector3(Random.Range(-10f, 10f), 1f, Random.Range(-10f, 10f));
        GameObject apple = Instantiate(applePrefab, randomPos, Quaternion.Euler(-90, 0, 0));
        apple.transform.position = new Vector3(apple.transform.position.x, 1f, apple.transform.position.z);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Apple"))
        {
            Destroy(other.gameObject);
            GrowSnake();
            SpawnApple();
            UpdateScore(); // Actualizar el contador de puntuación
        }
        else if (other.CompareTag("Die"))
        {
            UpdateBestScore(); // Actualizar el Best Score
            SceneManager.LoadScene("Menu"); // Cambiar a la escena del menú
        }
    }

    public void UpdateScore()
    {
        applesEaten++;
        if (scoreText != null)
        {
            scoreText.text = "Score P2: " + applesEaten.ToString();
        }
    }

    void UpdateBestScore()
    {
        // Obtener el Best Score actual
        int bestScore = PlayerPrefs.GetInt("BestScore", 0);

        // Actualizar el Best Score si es necesario
        if (applesEaten > bestScore)
        {
            bestScore = applesEaten;
            PlayerPrefs.SetInt("BestScore", bestScore);
            bestScoreText.text = "Best Score: " + bestScore.ToString();
        }
    }
}
