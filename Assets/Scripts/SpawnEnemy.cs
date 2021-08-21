using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnEnemy : MonoBehaviour
{
    [SerializeField] int spawnIntervalStart;
    [SerializeField] int spawnIntervalEnd;
    [SerializeField] GameObject enemy;
    [SerializeField] Text objectSpawnedText;

    bool shouldStart = true;
    private void Start()
    {
        StartCoroutine(StartSpawn());
    }
    IEnumerator StartSpawn()
    {
        while (shouldStart)
        {
            ManagerScript.instance.SetText();
            Instantiate(enemy, gameObject.transform.position, Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(spawnIntervalStart, spawnIntervalEnd));
        }
    }
}
