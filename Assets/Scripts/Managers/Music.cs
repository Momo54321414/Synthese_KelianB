using UnityEngine;

public class Music : MonoBehaviour
{
    // Initialisation des variables.

    private AudioSource music;

    // Initialisation de variables et vérification de ne pas jouer la musique de fond en double.

    void Start()
    {
        music = FindObjectOfType<Music>().GetComponent<AudioSource>();
        int nbMusic = FindObjectsOfType<Music>().Length;

        if (nbMusic > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(music);
        }
        
        if (PlayerPrefs.GetInt("Muted", 0) == 1)
        {
            music.Stop();
        }
    }

    // Activer / désactiver le thème sonore de fond.

    public void ToggleMusic()
    {
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
}