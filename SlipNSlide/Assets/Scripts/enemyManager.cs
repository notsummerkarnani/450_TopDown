using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyManager : MonoBehaviour
{
    public GameObject enemy;
    GameObject[] enemies;

    private Vector3 enemyspawn1 = new Vector3(6.2f, 1.92f, 0);
    private Vector3 enemyspawn2 = new Vector3(-6.45f, -15.93f, 0);


    void Start()
    {
        Instantiate(enemy, enemyspawn1, Quaternion.identity);
        Instantiate(enemy, enemyspawn2, Quaternion.identity);

    }

    // Update is called once per frame
    void Update()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");

        if (enemies.Length == 0)
        {
            Instantiate(enemy, enemyspawn1, Quaternion.identity);
            Instantiate(enemy, enemyspawn2, Quaternion.identity);
        }
    }
}
