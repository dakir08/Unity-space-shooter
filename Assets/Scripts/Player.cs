#pragma warning disable 0649
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;
using System;

public class Player : MonoBehaviour {
    // Start is called before the first frame update
    [Header("Ship Setting")]
    public int lives;
    public float health;
    public float speed;
    public bool isPlayerDead = false;
    public AudioClip laserAudio;
    public AudioClip explosionAudio;
    private AudioSource audioSource;
    public GameObject thruster;
    [Header("Laser object")]
    public Laser laserPreFab;
    public float laserSpeed;
    public float fireRate;
    private float fireCooldown = 0f;
    [Header("Rapid Fire Ability")]
    public bool isRapidFire = false;
    [Header("Triple Shot Ability")]
    public bool isTripleShot = false;
    public GameObject tripleShotPreFab;
    [Header("Speed up Ability")]
    public bool isSpeedUp = false;
    public float speedUpValue = 8;
    public float originalSpeed;
    [Header("Shield Ability")]
    public bool isShield = false;

    Rigidbody2D rb;
    PowerUp powerUp;
    Animator playerAnimator;
    Collider2D playerCollider;
    UIManager uiManager;
    GameSession gameSession;

    Transform playerShield;
    void Start() {
        rb = GetComponent<Rigidbody2D>();
        powerUp = FindObjectOfType<PowerUp>();
        playerAnimator = GetComponent<Animator>();
        playerCollider = GetComponent<Collider2D>();
        uiManager = FindObjectOfType<UIManager>();
        gameSession = FindObjectOfType<GameSession>();
        audioSource = GetComponent<AudioSource>();
        //Get the first element of this game object
        playerShield = gameObject.GetComponentInChildren<Transform>().GetChild(0);
        originalSpeed = speed;
    }
    private void bound() {
        if (transform.position.x > 9) {
            transform.position = new Vector2(-8.99f, transform.position.y);
        }
        if (transform.position.x < -9) {
            transform.position = new Vector2(8.99f, transform.position.y);
        }
        if (transform.position.y > 5.15) {
            transform.position = new Vector2(transform.position.x, -5.12f);
        }
        if (transform.position.y < -5.15) {
            transform.position = new Vector2(transform.position.x, 5.12f);
        }
    }

    // Update is called once per frame
    void Update() {

        // ability session
        speedUpAbility();
        if (isShield)
            playerShield.gameObject.SetActive(true);
        else
            StartCoroutine(disableShield());

        bound();

        uiManager.updateShield(isShield);
        uiManager.updateSpeedUp(isSpeedUp);
        uiManager.updateTripleShot(isTripleShot);
    }

    private void FixedUpdate() {
        if (!isPlayerDead) {
            playerMovement();
            fireSystem();
        }
    }

    private IEnumerator disableShield() {
        yield return new WaitForSeconds(0.2f);
        playerShield.gameObject.SetActive(false);
    }

    private void speedUpAbility() {
        if (isSpeedUp == true) {
            speed = speedUpValue;
            scaleThruster(0.6f);
        } else {
            speed = originalSpeed;
            scaleThruster();
        }

    }

    private void scaleThruster(float value = 0.4f) {
        thruster.transform.localScale = new Vector2(value, value);
    }

    private void fireSystem() {
        if (Input.GetButtonDown("Fire1") && !isRapidFire) {
            if (isTripleShot) {
                tripleFireWithRate();
            } else
                fireWithRate();
        }
        if (Input.GetButton("Fire1") && isRapidFire) {
            if (isTripleShot)
                tripleFireWithRate();
            else
                fireWithRate();

        }
    }

    private void playAudio(AudioClip audioClip, float time, float volume = 0.2f) {
        audioSource.clip = audioClip;
        audioSource.volume = volume;
        audioSource.PlayDelayed(time);
    }

    public void deadSystem() {
        if (health <= 0) {
            playerDead();
            if (lives <= 0) {
                playerCollider.enabled = false;
                Destroy(gameObject, 2f);
                gameSession.loadGameOverScene();
            } else {
                StartCoroutine(respawnPlayer());
            }
        }
    }

    private void playerDead() {
        playerAnimator.SetBool("isExplode", true);
        playAudio(explosionAudio, 0.2f, 0.4f);
        isPlayerDead = true;
        thruster.SetActive(false);
    }

    //No game over scene so first scene is loaded


    IEnumerator respawnPlayer() {
        yield return new WaitForSeconds(2f);
        isPlayerDead = false;
        lives = lives - 1;
        //Update lives
        health = 1000;
        playerAnimator.SetBool("isExplode", false);
        uiManager.updateLives(lives);
        thruster.SetActive(true);
    }


    private void playerMovement() {
        float horizontalVelocity = Input.GetAxisRaw("Horizontal");
        float verticalVelocity = Input.GetAxisRaw("Vertical");

        //set value for left-right value
        playerAnimator.SetFloat("horizontalValue", horizontalVelocity);
        //translate for ignoring physics
        transform.Translate(new Vector2(horizontalVelocity * speed * Time.deltaTime, verticalVelocity * speed * Time.deltaTime));
    }

    private void fireWithRate() {
        if (Time.time > fireCooldown) {

            Instantiate(laserPreFab, transform.position, Quaternion.identity);
            playAudio(laserAudio, 0f);
            fireCooldown = Time.time + fireRate;
        }
    }
    private void tripleFireWithRate() {
        if (Time.time > fireCooldown) {
            Instantiate(tripleShotPreFab, transform.position + new Vector3(0, 0.4f, 0), Quaternion.identity);
            playAudio(laserAudio, 0f);
            fireCooldown = Time.time + fireRate;
        }
    }
    public void changeShipSpeed(float value) {
        speed = value;
    }
    public void enableAbility(float time, int id) {
        getAbilityById(id, true);
        StartCoroutine(disableAbility(time, id));
    }
    private IEnumerator disableAbility(float time, int id) {
        yield return new WaitForSeconds(time);
        getAbilityById(id, false);

    }
    private void getAbilityById(int id, bool value) {
        switch (id) {
            case 1:
                isSpeedUp = value;
                break;
            case 2:
                isShield = value;
                break;

            default:
                isTripleShot = value;
                break;
        }
    }
}


//Things I learned
//use velocity for the physics 
// Can be use for dynamic
// rb.AddForce(new Vector2(horizontalVelocity * speed, verticalVelocity * speed));
//Time.deltatime is a real time second if the computer is strong enough to provide full frame, it variable
//Time.time is the time that count from the application start.
//Hireachy information stored in the Transform component