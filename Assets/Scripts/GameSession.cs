using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSession : MonoBehaviour {
    [SerializeField]
    private int currentScore = 0;
    private int currentScene;

    private void Awake() {
        singleTon();
    }

    private void Start() {
        currentScene = SceneManager.GetActiveScene().buildIndex;
        // AudioSource.PlayClipAtPoint(backgroundMusic, Camera.main.transform.position);
    }

    private void singleTon() {
        GameSession[] gameSession = FindObjectsOfType<GameSession>();
        if (gameSession.Length > 1) {
            Destroy(gameObject);
        } else
            DontDestroyOnLoad(gameObject);
    }

    public int getCurrentScore() {
        return currentScore;
    }
    public void setCurrentScore(int value) {
        currentScore = value;
    }
    public int getCurrentScene() {
        return currentScene;
    }
    public void loadNextScene() {
        currentScene++;
        SceneManager.LoadScene(currentScene);
    }
    public void loadGameOverScene() {
        currentScene = 0;
        SceneManager.LoadScene(currentScene);
    }
    public void loadNewGameScene() {
        currentScene = 0;
        SceneManager.LoadScene(currentScene);
    }
}

// Only use gamesession in the first scene of the game
// Research how to fix multiple gamesession added in many scenes