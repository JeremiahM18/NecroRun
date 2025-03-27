using UnityEngine;
using UnityEngine.Events;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    #region Singleton

    public static GameManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

    }
    #endregion

    public float currentScore = 0f;
    public SaveData data;
    public bool isPlaying = false;

    public UnityEvent onPlay = new UnityEvent();
    public UnityEvent onGameOver = new UnityEvent();

    //[SerializeField] private TMP_InputField nameInputField;
    //[SerializeField] private GameObject nameInputPanel;


    private void Start()
    {
        string loadedData = SaveSystem.Load("save");

        if (loadedData != null)
        {
            data = JsonUtility.FromJson<SaveData>(loadedData);
        }
        else
        {
            data = new SaveData();
        }

        string playerName = PlayerPrefs.GetString("PlayerName", "Player");
        data.name = playerName;

        if(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "MainGame")
        {
            //StartGame();
            FindFirstObjectByType<UIManager>().StartCountdown();
        }
    }

    private void Update()
    {
        if (isPlaying)
        {
            currentScore += Time.deltaTime;
        }       
    }

    public void StartGame()
    {
        onPlay.Invoke();
        isPlaying = true;
        currentScore = 0f;
    }

    public void GameOver()
    {
        isPlaying = false;

        if(currentScore > data.highScore)
        {
            //nameInputPanel.SetActive(true);
            data.highScore = currentScore;
           // data.name = "";
        }

        string saveString = JsonUtility.ToJson(data);
        SaveSystem.Save("save", saveString);

        PlayerPrefs.SetInt("Score", Mathf.RoundToInt(currentScore));
        PlayerPrefs.SetInt("HighScore", Mathf.RoundToInt(data.highScore));
        PlayerPrefs.SetString("PlayerName", data.name);
        PlayerPrefs.Save();

        onGameOver.Invoke();

        UnityEngine.SceneManagement.SceneManager.LoadScene("GameOver");
    }

    //public void submitName()
    //{
    //    string initials = nameInputField.text.ToUpper().Trim();
    //    if (initials.Length > 0)
    //    {
    //        data.name = initials;
    //    }
    //    nameInputPanel.SetActive(false);
    //}

    public string PrettyScore(float score)
    {
        return Mathf.RoundToInt(score).ToString();
    }
}


