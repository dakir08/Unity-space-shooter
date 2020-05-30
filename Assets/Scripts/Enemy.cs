using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour {
    [Header("Enemy Setting")]
    [SerializeField]
    private float speed = 1;
    [SerializeField]
    private int health = 300;
    public int enemyScore;

    Animator animator;
    Collider2D enemy_collider2D;
    AudioSource audioSource;
    private void Start() {
        animator = GetComponent<Animator>();
        enemy_collider2D = GetComponent<Collider2D>();
        audioSource = GetComponent<AudioSource>();
        enemyScore = health;
    }

    private void Update() {
        transform.Translate(Vector2.down * speed * Time.deltaTime);

    }

    public void destroyEnemyObject() {
        enemy_collider2D.enabled = false;
        explode();
        playerDestroyAudio(0.14f);
        Destroy(gameObject, 2f);
    }

    private void playerDestroyAudio(float time) {
        audioSource.PlayDelayed(time);
    }

    public void explode() {
        animator.SetBool("isExplode", true);
    }

    public int getHealth() {
        return health;
    }
    public void setHealth(int value) {
        health = value;
    }
}