using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    private bool interactable;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && interactable)
        {
            // Obtén el componente del jugador y llama al método para aumentar la vida
            PlayerController player = FindObjectOfType<PlayerController>();
            if (player != null)
            {
                player.IncreaseHealth(1); // Aumenta la vida en 1
            }
            Destroy(gameObject); // Destruye el corazón
            SoundManager.instance.PlaySFX(SoundManager.instance._audioSource, SoundManager.instance._healthAudio);
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            interactable = true;
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            interactable = false;
        }
    }
}