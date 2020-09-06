using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    [SerializeField] float spawnInterval;
    [SerializeField] GameObject enemy;
    bool shouldStart = true;
    private void Start()
    {
        StartCoroutine(StartSpawn());
    }

    IEnumerator StartSpawn()
    {
        while (shouldStart)
        {
            Instantiate(enemy, gameObject.transform.position, Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(3, 5));
        }
    }
 
}
