using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MainMenu : MonoBehaviour
{
    public TextMeshProUGUI bestScoreText;

    void Start()
    {
        // Recuperar el Best Score guardado
        int bestScore = PlayerPrefs.GetInt("BestScore", 0);

        // Mostrar el Best Score en el TextMeshProUGUI
        bestScoreText.text = "Best Score: " + bestScore.ToString();
    }
}
