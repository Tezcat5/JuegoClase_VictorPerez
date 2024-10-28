using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallDetector : MonoBehaviour
{
    [SerializeField] private int fallDamage = 5; // Daño que aplica el collider de caída

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Verifica si el objeto que colisiona es el jugador
        if (other.CompareTag("Player"))
        {
            // Intenta obtener el componente PlayerController del jugador
            PlayerController playerController = other.GetComponent<PlayerController>();

            // Si el jugador tiene el componente PlayerController, aplica daño
            if (playerController != null)
            {
                for (int i = 0; i < fallDamage; i++)
                {
                    playerController.TakeDamage();
                }
            }
        }
    }
}