using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreUI;
    [SerializeField] private GameObject startMenuUI;
    [SerializeField] private GameObject gameOverUI;

    [SerializeField] private TextMeshProUGUI gameOverScoreUI;
    [SerializeField] private TextMeshProUGUI gameOverHighScoreUI;
    [SerializeField] private TextMeshProUGUI gameOverPlayerNameUI;

    GameManager gameManager;

    private void Start()
    {
        gameManager = GameManager.Instance;
        gameManager.onGameOver.AddListener(ActivateGameOverUI);
    }

    private void Update()
    {
        scoreUI.text = gameManager.PrettyScore(gameManager.currentScore);
    }

    public void PlayButtonHandler()
    {
        gameManager.StartGame();
    }

    public void ActivateGameOverUI()
    {
        gameOverUI.SetActive(true);

        // Display current score and high score
        gameOverScoreUI.text = $"Score:  {gameManager.PrettyScore(gameManager.currentScore)}";
        gameOverHighScoreUI.text = $"High Score: {gameManager.PrettyScore(gameManager.data.highScore)}";
        gameOverPlayerNameUI.text = "Player: " + gameManager.data.name;
    }
}
