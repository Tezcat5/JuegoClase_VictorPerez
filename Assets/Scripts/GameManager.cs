using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private int coins = 0;
    [SerializeField] Text _coinText;

    private bool isPaused;
    private bool pauseAnimation;
    [SerializeField] GameObject _pauseCanvas;
    
    private int starsCollected = 0; // Contador de estrellas recolectadas
    private int totalStars; // Total de estrellas en el nivel
    [SerializeField] GameObject[] estrellasActivadas; // Referencias a las estrellas activadas

    private Animator _pausePanelAnimator;
    [SerializeField] private Slider _healthBar;

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }

        _pausePanelAnimator = _pauseCanvas.GetComponentInChildren<Animator>();
    }

    void Start()
    {
        // Cuenta el total de estrellas en la escena al iniciar
        totalStars = GameObject.FindGameObjectsWithTag("Star").Length;
        Debug.Log("Total de estrellas en el nivel: " + totalStars);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            StartCoroutine(LoadAsync("Main Menu"));
        }
    }

    public void Pause()
    {
        if (!isPaused && !pauseAnimation)
        {
            isPaused = true;
            Time.timeScale = 0f;
            _pauseCanvas.SetActive(true);
        }
        else if (isPaused && !pauseAnimation)
        {
            pauseAnimation = true;
            StartCoroutine(ClosePauseAnimation());
        }
    }

    IEnumerator ClosePauseAnimation()
    {
        _pausePanelAnimator.SetBool("Close", true);
        yield return new WaitForSecondsRealtime(0.50f);
        Time.timeScale = 1;
        isPaused = false;
        _pauseCanvas.SetActive(false);
        pauseAnimation = false;
    }

    public void AddCoin()
    {
        coins++;
        _coinText.text = coins.ToString();
    }

    public void AddStar()
    {
        starsCollected++;
        Debug.Log("Estrellas recolectadas: " + starsCollected + "/" + totalStars);

        if (starsCollected - 1 < estrellasActivadas.Length)
        {
            estrellasActivadas[starsCollected - 1].SetActive(true);
        }

        // Verifica si se han recolectado todas las estrellas
        if (starsCollected >= totalStars)
        {
            LoadWinScreen();
        }
    }

    private void LoadWinScreen()
    {
        Debug.Log("¡Has recolectado todas las estrellas! Cargando pantalla de victoria...");
        SceneManager.LoadScene("Win Screen");
    }

    public void SetHealthBar(int maxHealth)
    {
        _healthBar.maxValue = maxHealth;
        _healthBar.value = maxHealth;
    }

    public void UpdateHealthBar(int health)
    {
        _healthBar.value = health;
    }

    public void SceneLoader(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    IEnumerator LoadAsync(string sceneName)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}