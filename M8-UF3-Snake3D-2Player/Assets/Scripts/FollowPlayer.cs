using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform player; // Referencia al jugador (puedes asignarla desde el inspector)

    [SerializeField] private Vector3 offset = new Vector3(0f, 5f, -10f); // Offset de la posición de la cámara respecto al jugador

    void LateUpdate()
    {
        if (player != null)
        {
            // Ajustar la posición de la cámara para seguir al jugador con el offset
            transform.position = player.position + offset;
        }
    }
}
