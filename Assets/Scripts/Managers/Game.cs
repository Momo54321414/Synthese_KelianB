using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    // Initialisation des variables.

    [SerializeField] private GameObject helpUI = default;
    [SerializeField] private AudioClip playSnd = default;

    // Jouer le son du début de la partie, à sa scene respective.

    void Start()
    {
        if(SceneManager.GetActiveScene().buildIndex == 1)
        AudioSource.PlayClipAtPoint(playSnd, Camera.main.transform.position, 0.5f);
    }

    // Début de la partie: Initialisation de la scène et remise à niveau du passage du temps.

    public void Begin()
    {
        SceneManager.LoadScene(1);
        Time.timeScale = 1;
    }

    // Activer / désactiver le thème sonore de fond.

    public void ToggleMusic()
    {
        AudioSource music = FindObjectOfType<Music>().GetComponent<AudioSource>();

        if (PlayerPrefs.GetInt("Muted", 0) == 1)
        {
            music.Play();
            PlayerPrefs.SetInt("Muted", 0);
            PlayerPrefs.Save();
        }
        else
        {
            music.Pause();
            PlayerPrefs.SetInt("Muted", 1);
            PlayerPrefs.Save();
        }
    }

    // Afficher le menu d'instructions.

    public void ShowHelp()
    {
        helpUI.SetActive(true);
    }

    // Fermer le menu d'instructions.

    public void HideHelp()
    {
        helpUI.SetActive(false);
    }

    // Revenir au menu principal.

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

    // Rejouer en chargeant directement la scene de jeu (1).

    public void Retry()
    {
        SceneManager.LoadScene(1);
    }

    // Quitter l'application.

    public void Quit()
    {
        Application.Quit();
    }
}