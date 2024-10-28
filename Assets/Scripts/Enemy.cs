using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int healthPoints = 5;
    
    private AudioSource _audioSource;

    void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        // Si quieres que suene al iniciar, puedes dejarlo
        // SoundManager.instance.PlaySFX(_audioSource, SoundManager.instance._mimikAudio);
    }

    void Update()
    {
        // Aquí puedes agregar lógica adicional si es necesario
    }

    public void TakeDamage()
    {
        healthPoints -= 1;

        if (healthPoints <= 0)
        {
            // Reproducir el sonido de muerte antes de destruir el objeto
            SoundManager.instance.PlaySFX(_audioSource, SoundManager.instance._mimikAudio);
            StartCoroutine(Die()); // Llama a la coroutine para manejar la muerte
        } 
    }

    private IEnumerator Die()
    {
        // Opcional: Agrega un pequeño retraso antes de destruir al enemigo
        yield return new WaitForSeconds(0.5f); // Tiempo para que el sonido se escuche antes de ser destruido
        Destroy(gameObject);
    }
}