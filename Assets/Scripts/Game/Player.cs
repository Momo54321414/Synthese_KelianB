using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    // Initialisation des variables.

    [SerializeField] private GameObject dagger = default;
    [SerializeField] private AudioClip throwSnd = default;
    [SerializeField] private AudioClip hurtSnd = default;
    [SerializeField] private AudioClip atkSnd = default;
    [SerializeField] private AudioClip dieSnd = default;

    private bool isAttacking = false;
    private bool isThrowing = false;
    private bool canAttack = true;
    private bool isDead = false;
    private Animator anim;
    private int hp = 3;
    private UI UI;

    // Rafraîchissement fixe du jeu de 60 FPS, et initialisation de variables.

    void Start()
    {
        Application.targetFrameRate = 60;
        anim = GetComponent<Animator>();
        UI = FindObjectOfType<UI>();
    }

    // Rafraîchissement des méthodes à chaque frame pour détecter le mouvement, et le type d'attaque du joueur.

    void FixedUpdate()
    {
        Move();
        Throw();
        Attack();
    }

    // Mouvement du joueur s'il n'est pas mort, et collisions avec les coins de la carte du jeu.

    private void Move()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        if ((transform.position.x < -8.5 && x < 0) || (transform.position.x > 8.5 && x > 0))
        {
            x = 0;
        }

        if ((transform.position.y < -4.3 && y < 0) || (transform.position.y > 2.5 && y > 0))
        {
            y = 0;
        }

        if(!isAttacking && !isDead)
        {
            transform.Translate(new Vector3(x, y, 0) * Time.deltaTime * 3.5f);
        }

        if (x != 0f || y != 0f)
        {
            anim.SetBool("isMoving", true);
        }
        else
        {
            anim.SetBool("isMoving", false);
        }
    }

    // Lancer une dague, et lancement de la coroutine jointe pour le temps d'attente avant le prochain lancer.

    private void Throw()
    {
        if (!isThrowing && Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(ThrowTime());
        }
    }

    // Attaquer à l'épée, et lancement de la coroutine jointe pour le temps d'attente avant la prochaine attaque.

    private void Attack()
    {
        if (canAttack && Input.GetKeyDown(KeyCode.LeftShift))
        {
            canAttack = false;
            anim.SetBool("isAttacking", true);
            isAttacking = true;
            AudioSource.PlayClipAtPoint(atkSnd, Camera.main.transform.position, 0.2f);
            StartCoroutine(AttackTime());
        }
    }

    // Prise de dégâts du joueur lorsqu'il entre en collision avec un ennemi, et lancement de la coroutine respective.

    public void Damage()
    {
        hp -= 1;
        AudioSource.PlayClipAtPoint(hurtSnd, Camera.main.transform.position, 0.5f);

        if (hp > 0)
        {
            StartCoroutine(Hurt());
        }
        else
        {
            StartCoroutine(Die());
        }
    }

    // Augmentation des points de vie du joueur en touchant le buff, en s'assurant que sa vie ne dépasse pas le maximum de 3.

    public void Heal()
    {
        hp += 1;
        hp = hp > 3 ? 3 : hp;
    }

    // Retourner l'état du joueur, s'il est en train d'atttaquer ou non. Utile pour la détection lors de la collsion avec un ennemi.

    public bool IsAttacking()
    {
        return isAttacking ? true : false;
    }

    // Coroutine et animation de la mort du joueur, changement de son tag pour ne plus entrer en collision avec les ennemis, et attente de 2 secondes avant la scène de fin.

    IEnumerator Die()
    {
        UI.End();
        anim.SetBool("isDead", true);
        tag = "Untagged";
        isDead = true;
        AudioSource.PlayClipAtPoint(dieSnd, Camera.main.transform.position, 0.5f);
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(2);
    }

    // Coroutine et animation pour la prise de dégâts du joueur.

    IEnumerator Hurt()
    {
        anim.SetBool("isHurt", true);
        yield return new WaitForSeconds(1f);
        anim.SetBool("isHurt", false);
    }

    // Coroutine et animation de l'attaque à l'épée, et temps d'attente avant la prochaine attaque possible.

    IEnumerator AttackTime()
    {
        yield return new WaitForSeconds(0.5f);
        anim.SetBool("isAttacking", false);
        isAttacking = false;
        yield return new WaitForSeconds(0.6f);
        canAttack = true;
    }

    // Coroutine et animation du lancer de la dague du joueur, et temps d'attente avant le prochain lancer possible.

    IEnumerator ThrowTime()
    {
        isThrowing = true;
        anim.SetBool("isThrowing", true);
        yield return new WaitForSeconds(0.2f);
        Instantiate(dagger, transform.position + new Vector3(1f, 0f, 0f), Quaternion.identity);
        AudioSource.PlayClipAtPoint(throwSnd, Camera.main.transform.position, 0.2f);
        yield return new WaitForSeconds(0.3f);
        anim.SetBool("isThrowing", false);
        isThrowing = false;
    }
}