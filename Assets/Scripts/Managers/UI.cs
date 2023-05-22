using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    // Initialisation des variables.

    [SerializeField] private GameObject pauseUI = default;
    [SerializeField] private GameObject hpBar = default;
    [SerializeField] private Text scoreUI = default;
    [SerializeField] private Text timeUI = default;

    private RectTransform hpPane;
    private bool isPaused = false;
    private int score, hp = 3;
    private float startTime;

    // Initialisation de variables et réinitialisation du temps au lancement du jeu.

    void Start()
    {
        hpPane = hpBar.GetComponent<RectTransform>();
        startTime = Time.time;
    }

    // Écoute de la touche Escape pour activer / désactiver le menu pause.

    void Update()
    {
        if (!isPaused && Input.GetKeyDown(KeyCode.Escape))
        {
            isPaused = true;
            Time.timeScale = 0;
            pauseUI.SetActive(true);
        }
        else if (isPaused && Input.GetKeyDown(KeyCode.Escape))
        {
            Resume();
        }
    }

    // Rafraichissement du compteur de temps du UI à chaque seconde.

    void FixedUpdate()
    {
        timeUI.text = (Time.time - startTime).ToString("f0");
    }

    // Modifier le score par une variable.

    public void SetScore(int n)
    {
        score += n;
        scoreUI.text = score.ToString("0000000");
    }

    // Modifier la vie du joueur par une variable et rafraîchir la barre de vie.

    public void SetHP(int n)
    {
        hp += n;
        hp = hp > 3 ? 3 : hp;
        switch (hp)
        {
            case 3: hpPane.sizeDelta = new Vector2(240, 32); break;
            case 2: hpPane.sizeDelta = new Vector2(160, 32); break;
            case 1: hpPane.sizeDelta = new Vector2(80,  32); break;
            case 0: hpPane.sizeDelta = new Vector2(0,   32); break;
        }
    }

    // Fermer le menu pause et reprendre le jeu.

    public void Resume()
    {
        pauseUI.SetActive(false);
        Time.timeScale = 1;
        isPaused = false;
    }

    // Modifier le pointage du joueur à la fin de la partie.

    public void End()
    {
        PlayerPrefs.SetInt("score", score);
    }
}