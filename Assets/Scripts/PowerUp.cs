using UnityEngine;

public class PowerUp : MonoBehaviour {

    Player player;
    public PowerUpProps powerUpAttributes;
    [Header("Speed Up ability")]
    public float speedUpValue = 8f;
    public float originalSpeedValue;
    private void Start() {
        player = FindObjectOfType<Player>();
        originalSpeedValue = player.speed;
    }
    private void Update() {
        transform.Translate(Vector2.down * powerUpAttributes.powerUpFallSpeed * Time.deltaTime);
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Player") {
            enableAbilityById(powerUpAttributes.powerUpId);
            //Play audio
            AudioSource.PlayClipAtPoint(powerUpAttributes.audio, Camera.main.transform.position);
            //Destroy power up
            Destroy(gameObject);
        }
    }
    private void enableAbilityById(int id) {
        player.enableAbility(powerUpAttributes.powerUpDuration, powerUpAttributes.powerUpId);
    }

}