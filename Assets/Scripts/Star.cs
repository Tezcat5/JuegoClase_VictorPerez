using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : MonoBehaviour
{
    private bool interactable;

    void Update()
    {
        // Verifica si el jugador está interactuando (presionando "F") y está cerca de la estrella
        if (Input.GetKeyDown(KeyCode.F) && interactable)
        {
            GameManager.instance.AddStar(); // Notifica al GameManager que se recolectó una estrella
            SoundManager.instance.PlaySFX(SoundManager.instance._audioSource, SoundManager.instance._starAudio);
            Destroy(gameObject); // Destruye la estrella para simular la recolección
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            interactable = true; // Activa la posibilidad de interactuar cuando el jugador está cerca
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            interactable = false; // Desactiva la interacción cuando el jugador se aleja
        }
    }
}