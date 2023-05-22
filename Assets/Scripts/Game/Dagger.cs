using UnityEngine;

public class Dagger : MonoBehaviour
{
    // Simuler une portée maximale en supprimant la dague après un cours temps donné.

    void Start()
    {
        Destroy(gameObject, 0.7f);
    }

    // Vers bouger à vitesse constante la dague vers la droite.
        
    void FixedUpdate()
    {
        transform.Translate(Vector3.right * Time.deltaTime * 12f);
    }
}