using UnityEngine;

public class Buff : MonoBehaviour
{
    // Initialisation des variables.

    [SerializeField] private AudioClip healSnd = default;

    private Player player;
    private UI UI;

    // Initialisation de variables, et s'assurer que le buff disparait après un temps donné.

    void Start()
    {
        player = FindObjectOfType<Player>();
        UI = FindObjectOfType<UI>();
        Destroy(gameObject, 3.5f);
    }

    // Faire bouger le buff à vitesse constante vers le bas.

    void FixedUpdate()
    {
        transform.Translate(Vector3.down * Time.deltaTime * 3.2f);
    }

    // Redonner 1 point de vie au jeu lorsqu'il entre en collision avec le buff.

    void OnTriggerEnter2D(Collider2D c)
    {
        if (c.tag == "Player")
        {
            UI.SetHP(1);
            player.Heal();
            AudioSource.PlayClipAtPoint(healSnd, Camera.main.transform.position, 1f);
            Destroy(gameObject);
        }
    }
}