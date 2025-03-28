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
    
    [Header("Audio")]
    //[SerializeField] private AudioClip mainGameAudio;
    [SerializeField] private AudioClip countDownAudio;
    [SerializeField] private AudioSource audioSource; 
    private bool hasStartedCountdown = false;

    GameManager gameManager;

    #region Singleton
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    #endregion
    private void Start()
    {
        gameManager = GameManager.Instance;

        if (gameManager != null)
        {
            gameManager.onGameOver.AddListener(ActivateGameOverUI);
        }

        if(SceneManager.GetActiveScene().name == "MainGame")
        {
            StartCountdown();
        }

        if(gameOverScoreUI != null)
        {
            int score = Mathf.RoundToInt(PlayerPrefs.GetFloat("Score", 0f));
            int highScore = Mathf.RoundToInt(PlayerPrefs.GetFloat("HighScore", 0f));
            string playerName = PlayerPrefs.GetString("PlayerName", "Player");
            
            gameOverScoreUI.text = "Score: " + score;
            gameOverHighScoreUI.text = "High Score: " + highScore;
            gameOverPlayerNameUI.text = "Player: " + playerName;
        }
    }

    private void Update()
    {
        if(scoreUI != null && gameManager != null)
        {
            scoreUI.text = gameManager.PrettyScore(gameManager.currentScore);
        }
    }

    #region Countdown Audio 
    public void StartCountdown()
    {
        if (hasStartedCountdown)
        {
            return;
        }
        hasStartedCountdown = true;
        StartCoroutine(CountdownRoutine());
    }
    private IEnumerator CountdownRoutine()
    {
        countdownText.gameObject.SetActive(true);
        GameManager.Instance.isPlaying = false;

        string[] countdownWords = { "3", "2", "1", "RUN!" };
        Color[] countdownColors = { Color.red, Color.yellow, Color.green, Color.black };

        for (int i = 0; i < countdownWords.Length; i++)
        {
            if (audioSource != null && countDownAudio != null)
            {
                audioSource.PlayOneShot(countDownAudio);
            }

            countdownText.text = countdownWords[i];
            countdownText.color = countdownColors[i];

            yield return new WaitForSeconds(1f);
        }
        countdownText.gameObject.SetActive(false);
        GameManager.Instance.StartGame();

        //if (audioSource != null && mainGameAudio != null)
        //{
        //    audioSource.clip = mainGameAudio;
        //    audioSource.loop = true;
        //    audioSource.Play();
        //}
    }
    #endregion
    public void PlayButtonHandler()
    {
        string name = playerNameInput != null ? playerNameInput.text : "Player";
        PlayerPrefs.SetString("PlayerName", name);
        PlayerPrefs.Save();

       SceneManager.LoadScene("MainGame");
    }

    public void RetryButtonHandler()
    {
        hasStartedCountdown = false;
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
        PlayerPrefs.SetFloat("HighScore", Mathf.Max(gameManager.currentScore, PlayerPrefs.GetFloat("HighScore", 0f)));
        PlayerPrefs.Save();

        //gameOverScoreUI.text = $"Score:  {gameManager.PrettyScore(gameManager.currentScore)}";
        //gameOverHighScoreUI.text = $"High Score: {gameManager.PrettyScore(gameManager.data.highScore)}";
        //gameOverPlayerNameUI.text = "Player: " + gameManager.data.name;
    }

    public void QuitGame()
    {
        Debug.Log("Quit game");
        Application.Quit();
    }
}
