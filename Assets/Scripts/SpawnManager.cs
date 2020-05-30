#pragma warning disable 0649

using UnityEngine;
using System.Collections;

public class SpawnManager : MonoBehaviour {
    [Header("Enemy Manager")]
    [SerializeField]
    private Enemy enemy;
    [SerializeField]
    private int enemyNumber;
    [SerializeField]
    private int spawnedEnemy;

    [SerializeField]
    private float spawnIntervalMin;
    [SerializeField]
    private float spawnIntervalMax;
    [Header("Power Up Manager")]
    [SerializeField]
    private bool isPowerUpSpawn = true;
    [SerializeField]
    private PowerUpProps[] powerUpList;
    [SerializeField]
    private int[] powerUpNumber;

    GameSession gameSession;
    private void Start() {
        gameSession = FindObjectOfType<GameSession>();
        float spawnInterval = Random.Range(spawnIntervalMin, spawnIntervalMax);
        StartCoroutine(spawnEnemy(spawnInterval));
        if (isPowerUpSpawn)
            foreach (var powerUp in powerUpList) {
                StartCoroutine(spawnPowerUp(powerUp, powerUpNumber[powerUp.powerUpId]));
            }
    }

    IEnumerator spawnPowerUp(PowerUpProps powerUpData, int powerUpNumber) {
        while (powerUpNumber != 0) {
            var spawnPosition = new Vector2(Random.Range(-8.4f, 8.4f), 5.6f);
            float time = Random.Range(powerUpData.spawnIntervalMin, powerUpData.spawnIntervalMax);
            yield return new WaitForSeconds(time);
            Instantiate(powerUpData.powerUpPrefab, spawnPosition, Quaternion.identity);
            powerUpNumber--;
        }
    }

    IEnumerator spawnEnemy(float time) {
        while (spawnedEnemy < enemyNumber) {
            var spawnPosition = new Vector2(Random.Range(-8.4f, 8.4f), 5.6f);
            yield return new WaitForSeconds(time);
            Instantiate(enemy, spawnPosition, Quaternion.identity);
            spawnedEnemy++;
        }
        yield return new WaitForSeconds(7f);
        gameSession.loadNewGameScene();
    }
}

// use the dependent gameobject to make a manager