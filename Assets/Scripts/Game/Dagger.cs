using UnityEngine;

public class Dagger : MonoBehaviour
{
    // Simuler une port�e maximale en supprimant la dague apr�s un cours temps donn�.

    void Start()
    {
        Destroy(gameObject, 0.7f);
    }

    // Vers bouger � vitesse constante la dague vers la droite.
        
    void FixedUpdate()
    {
        transform.Translate(Vector3.right * Time.deltaTime * 12f);
    }
}