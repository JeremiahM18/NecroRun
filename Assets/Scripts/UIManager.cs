using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;

public class UIManager : MonoBehaviour
{
    [Header("Main Menu UI")]
    [SerializeField] private TMP_InputField playerNameInput;
    [SerializeField] private GameObject startMenuUI;

    [Header("Game UI")]
    [SerializeField] private TextMeshProUGUI scoreUI;
    [SerializeField] private TextMeshProUGUI countdownText;

    [Header("Game Over UI")]
    [SerializeField] private GameObject gameOverUI;
    [SerializeField] private TextMeshProUGUI gameOverScoreUI;
    [SerializeField] private TextMeshProUGUI gameOverHighScoreUI;
    [SerializeField] private TextMeshProUGUI gameOverPlayerNameUI;

    GameManager gameManager;

    private void Start()
    {
        gameManager = GameManager.Instance;
        if (gameManager != null)
        {
            gameManager.onGameOver.AddListener(ActivateGameOverUI);
        }

        if(gameOverScoreUI != null)
        {
            gameOverScoreUI.text = "Score: " + PlayerPrefs.GetInt("Score", 0);
            gameOverHighScoreUI.text = "High Score: " + PlayerPrefs.GetInt("HighScore", 0);
            gameOverPlayerNameUI.text = "Player: " + PlayerPrefs.GetString("PlayerName", "Player");
        }
    }

    private void Update()
    {
        if(scoreUI != null && gameManager != null)
        {
            scoreUI.text = gameManager.PrettyScore(gameManager.currentScore);
        }
    }

    public void StartCountdown()
    {
        StartCoroutine(CountdownRoutine());
    }

    private IEnumerator CountdownRoutine()
    {
        countdownText.gameObject.SetActive(true);
        GameManager.Instance.isPlaying = false;

        string[] countdownWords = { "3", "2", "1", "RUN!" };
        Color[] countdownColors = {Color.red, Color.yellow, Color.green, Color.black};

        for(int i = 0; i < countdownWords.Length; i++)
        {
            countdownText.text = countdownWords[i];
            countdownText.color = countdownColors[i];

            
            yield return new WaitForSeconds(1f);
        }

        countdownText.gameObject.SetActive(false);
        GameManager.Instance.StartGame();
     }

    public void PlayButtonHandler()
    {
        string name = playerNameInput != null ? playerNameInput.text : "Player";
        PlayerPrefs.SetString("PlayerName", name);
        PlayerPrefs.Save();

       SceneManager.LoadScene("MainGame");
    }

    public void RetryButtonHandler()
    {
        SceneManager.LoadScene("MainGame");
    }

    public void MainMenuButtonHandler()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void ActivateGameOverUI()
    {
        if (gameOverUI != null)
        {
            gameOverUI.SetActive(true);
        }

        // Save score 
        PlayerPrefs.SetFloat("Score", gameManager.currentScore);
        PlayerPrefs.SetFloat("HighScore", Mathf.Max(gameManager.currentScore, PlayerPrefs.GetFloat("HighScore", 0)));
        PlayerPrefs.Save();

        //gameOverScoreUI.text = $"Score:  {gameManager.PrettyScore(gameManager.currentScore)}";
        //gameOverHighScoreUI.text = $"High Score: {gameManager.PrettyScore(gameManager.data.highScore)}";
        //gameOverPlayerNameUI.text = "Player: " + gameManager.data.name;
    }
}
