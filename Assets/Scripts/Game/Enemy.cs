using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Initialisation des variables.

    [SerializeField] private AudioClip dieSnd = default;

    private SpriteRenderer sprite;
    private bool isHit = false;
    private BoxCollider2D box;
    private Animator anim;
    private Player player;
    private float speed;
    private bool isDead;
    private UI UI;

    // Initialisation de variables et donner une vitesse aléatoire à l'ennemi.

    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        player = FindObjectOfType<Player>();
        box = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
        speed = Random.Range(2.5f, 4f);
        UI = FindObjectOfType<UI>();
    }

    // Faire bouger l'ennemi à vitesse constance vers la gauche, tant qu'il n'est pas mort.

    void FixedUpdate()
    {
        transform.Translate(Vector3.left * Time.deltaTime * (!isDead ? speed : 0));
    }

    // Attribution des points donnés au joueur dépendemment de la façon dont il tue l'ennemi. Sinon, réduire les points s'il entre en collision directement avec le joueur.

    void OnTriggerEnter2D(Collider2D c)
    {
        if (c.tag == "Player")
        {
            if(player.IsAttacking())
            {
                UI.SetScore(100);
            }
            else
            {
                UI.SetScore(-50);
                player.Damage();
                UI.SetHP(-1);
            }
            anim.SetBool("isDead", true);
            isDead = true;
            Destroy(box);
            AudioSource.PlayClipAtPoint(dieSnd, Camera.main.transform.position, 0.3f);
            Destroy(gameObject, 0.5f);
        }
        else if (c.tag == "Dagger")
        {
            if (isHit)
            {
                UI.SetScore(50);
                anim.SetBool("isDead", true);
                isDead = true;
                Destroy(box);
                AudioSource.PlayClipAtPoint(dieSnd, Camera.main.transform.position, 0.3f);
                Destroy(gameObject, 0.5f);
            }
            else
            {
                StartCoroutine(Hurt());
                isHit = true;
            }
            Destroy(c.gameObject);
        }
    }

    // Afficher l'ennemi en rouge pendant un certain temps lorsqu'il se fait tapper par une dague.

    IEnumerator Hurt()
    {
        sprite.color = new Color(1, 0, 0, 1);
        yield return new WaitForSeconds(0.3f);
        sprite.color = new Color(1, 1, 1, 1);
    }
}