using UnityEngine;
using UnityEngine.UI;

public class End : MonoBehaviour
{
    // Initialisation des variables.

    [SerializeField] private Text highScore = default;
    [SerializeField] private Text score = default;

    // Affichage du score de la partie terminée, et le meilleur score sur la machine locale.

    void Start()
    {
        score.text = PlayerPrefs.GetInt("score").ToString();
        if (PlayerPrefs.HasKey("highScore"))
        {
            if (PlayerPrefs.GetInt("score") > PlayerPrefs.GetInt("highScore"))
            {
                PlayerPrefs.SetInt("highScore", PlayerPrefs.GetInt("score"));
            }
        }
        else
        {
            PlayerPrefs.SetInt("highScore", PlayerPrefs.GetInt("score"));
        }
        PlayerPrefs.Save();
        highScore.text = PlayerPrefs.GetInt("highScore").ToString();
    }
}