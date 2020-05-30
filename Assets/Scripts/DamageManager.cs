#pragma warning disable 0649

using UnityEngine;
using System.Collections;

public class DamageManager : MonoBehaviour {
    [SerializeField]
    private int damage;

    Enemy enemy;
    Player player;

    GameSession gameSession;

    private void Start() {

        gameSession = FindObjectOfType<GameSession>();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        enemy = other.GetComponent<Enemy>();
        player = other.GetComponent<Player>();
        if (other.tag == "Enemy") {
            enemyHealthReduce(other);

            var enemyHealth = enemy.getHealth();
            //checking die ship object
            if (enemyHealth <= 0) {
                enemy.destroyEnemyObject();
                gameSession.setCurrentScore(gameSession.getCurrentScore() + enemy.enemyScore);
            }
            if (gameObject.tag == "Laser")
                Destroy(gameObject);
        }
        if (other.tag == "Player") {
            if (player.isShield)
                player.isShield = false;
            else
                playerHealthReduce(other);

            //checking if die
            player.deadSystem();
        }

    }

    private void enemyHealthReduce(Collider2D other) {
        var enemyCurrentHealth = enemy.getHealth() - damage;
        enemy.setHealth(enemyCurrentHealth);
    }
    private void playerHealthReduce(Collider2D other) {
        player.health -= damage;
    }
}