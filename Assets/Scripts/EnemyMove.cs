using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    [SerializeField] float speed;

    void Update()
    {
        transform.Translate(Vector3.left * speed * Time.deltaTime);
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("HIT");
        GameObject player = other.gameObject;
        GameObject destroyer = other.gameObject;

        if (player.tag == "Player" || destroyer.tag == "Destroyer")
        {
            Destroy(gameObject);
        }
    }
}
