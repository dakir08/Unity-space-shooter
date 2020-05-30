using UnityEngine;

public class Laser : MonoBehaviour {
    Rigidbody2D rb;

    Player player;

    private void Start() {
        rb = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<Player>();
    }
    private void Update() {
        rb.velocity = new Vector2(0, player.speed);
    }
}