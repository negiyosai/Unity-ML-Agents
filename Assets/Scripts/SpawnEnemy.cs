using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    [SerializeField] float spawnInterval;
    [SerializeField] GameObject enemy;
    private void Start()
    {
        InvokeRepeating("Spawn", 0, 3);
    }
    void Spawn()
    {
        Instantiate(enemy, gameObject.transform.position, Quaternion.identity);
    }
}
