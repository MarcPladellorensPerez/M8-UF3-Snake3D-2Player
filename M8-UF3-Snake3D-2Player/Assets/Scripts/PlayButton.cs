using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayButton : MonoBehaviour
{
    void Start()
    {
        // Obtener el componente Button del botón "Play"
        Button playButton = GetComponent<Button>();

        // Asignar la función LoadSnake3DScene al evento de clic del botón
        playButton.onClick.AddListener(GameManager.Instance.LoadSnake3DScene);
    }
}
