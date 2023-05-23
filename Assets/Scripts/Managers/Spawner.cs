using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Spawner : MonoBehaviour
{
    // Initialisation des variables.

    [SerializeField] private GameObject container = default;
    [SerializeField] private GameObject buff_HP = default;
    [SerializeField] private GameObject waveUI = default;
    [SerializeField] private GameObject enemy = default;
    [SerializeField] private Text waveTxt = default;

    private float spawnCD = 2f;
    private int wave = 1;

    // Lancement des coroutines au début du jeu pour faire apparaître les ennemis, les buffs, et modifier les vagues d'ennemis.

    void Start()
    {
        StartCoroutine(SpawnEnemy());
        StartCoroutine(SpawnBuff());
        StartCoroutine(Wave());
    }

    // Couroutine pour faire apparaître les ennemis.

    IEnumerator SpawnEnemy()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(spawnCD - 0.2f, spawnCD + 0.2f));
            Vector3 pos = new Vector3(10f, Random.Range(-4f, 1.5f), 0f);
            GameObject newEnemy = Instantiate(enemy, pos, Quaternion.identity);
            newEnemy.transform.parent = container.transform;
        }
    }

    // Coroutine pour faire apparaître les buffs.

    IEnumerator SpawnBuff()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(4f, 8f));
            Vector3 pos = new Vector3(Random.Range(-2f, 2f), 5.5f, 0f);
            Instantiate(buff_HP, pos, Quaternion.identity);
        }
    }

    // Coroutine pour modifier la difficulté du jeu par vagues d'ennemis.

    IEnumerator Wave()
    {
        yield return new WaitForSeconds(2f);
        waveUI.SetActive(false);

        while (true)
        {
            yield return new WaitForSeconds(20f);
            wave++;
            waveTxt.text = "Vague " + wave;
            waveUI.SetActive(true);
            spawnCD /= 1.5f;
            yield return new WaitForSeconds(2f);
            waveUI.SetActive(false);
        }
    }
}