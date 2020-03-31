using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    float waittime = 2f;
    int randomPower;
    [SerializeField]
    GameObject Enemy, EnemyContainer, PowerContainer;
    [SerializeField]
    GameObject[] PowerUps;

    GameManager gameManager;
    void Start()
    {
        gameManager = GameObject.Find("Game_Manager").GetComponent<GameManager>();
    }

    IEnumerator SpawnPowerUp()
    {
        yield return new WaitForSeconds(7f);
        while (!gameManager.isGameOver)
        {
            yield return new WaitForSeconds(Random.Range(2, 5));
            Vector3 spawn = new Vector3(Random.Range(-9f, 9f), 6.5f, 0);
            randomPower = Random.Range(0, 3);
            GameObject newTripleShot = Instantiate(PowerUps[randomPower], spawn, Quaternion.identity);
            newTripleShot.transform.parent = PowerContainer.transform;
            yield return new WaitForSeconds(Random.Range(5,15));
        }
    }
    IEnumerator SpawnEnemy()
    {
        yield return new WaitForSeconds(3f);
        while(!gameManager.isGameOver)
        {
            Vector3 spawn = new Vector3(Random.Range(-9f, 9f), 6.5f, 0);
            GameObject newEnemy = Instantiate(Enemy, spawn, Quaternion.identity);
            newEnemy.transform.parent = EnemyContainer.transform;
            yield return new WaitForSeconds(waittime);
        }
    }

    public void StartWave()
    {
        StartCoroutine(SpawnEnemy());
        StartCoroutine(SpawnPowerUp());
    }
}
