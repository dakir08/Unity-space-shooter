using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour {

    public Sprite[] lives;
    public Image displayLivesImage;
    public TextMeshProUGUI displayScoreText;
    public TextMeshProUGUI displayHealthText;
    public Image displayTripleShotImage;
    public Image displaySpeedUpImage;
    public Image displayShieldImage;

    GameSession gameSession;
    Player player;
    private void Start() {
        // Debug.Log(1);
        gameSession = FindObjectOfType<GameSession>();
        player = FindObjectOfType<Player>();
        displayLivesImage.sprite = lives[lives.Length - 1];
        Debug.Log("current scene" + gameSession.getCurrentScene());
        if (gameSession.getCurrentScene() == 1)
            gameSession.setCurrentScore(0);
    }
    private void Update() {
        displayScoreText.text = gameSession.getCurrentScore().ToString();
        if (displayHealthText.IsActive())
            healthUI();

    }

    private void healthUI() {
        displayHealthText.text = $"HP: {player.health}";
        if (player.health > 500)
            displayHealthText.color = Color.green;
        else if (player.health > 100)
            displayHealthText.color = Color.yellow;
        else
            displayHealthText.color = Color.red;
        if (player.health < 0)
            displayHealthText.text = "HP: 0";
    }

    public void updateLives(int currentLive) {
        displayLivesImage.sprite = lives[currentLive];
    }
    public void updateTripleShot(bool value) {
        if (value)
            displayTripleShotImage.enabled = true;
        else
            displayTripleShotImage.enabled = false;
    }
    public void updateSpeedUp(bool value) {
        if (value)
            displaySpeedUpImage.enabled = true;
        else
            displaySpeedUpImage.enabled = false;
    }
    public void updateShield(bool value) {
        if (value)
            displayShieldImage.enabled = true;
        else
            displayShieldImage.enabled = false;
    }



    public void clicked() {
        gameSession.loadNextScene();
    }


}


// Textmeshpro is used for meshes in 3d world space
// Textmeshpro is used for canvas element.