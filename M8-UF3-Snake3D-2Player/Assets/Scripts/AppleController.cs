using UnityEngine;

public class AppleController : MonoBehaviour
{
    private SnakeController snakeController;

    void Start()
    {
        snakeController = FindObjectOfType<SnakeController>();
        if (snakeController == null)
        {
            Debug.LogError("No se encontró el SnakeController en la escena.");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (snakeController != null)
            {
                snakeController.UpdateScore(); // Llamar a UpdateScore directamente desde AppleController
            }
            else
            {
                Debug.LogError("SnakeController no está asignado en AppleController.");
            }
            Destroy(gameObject); // Destruir la manzana después de ser recogida
        }
    }
}
