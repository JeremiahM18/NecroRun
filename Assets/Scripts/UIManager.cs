using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreUI;
    [SerializeField] private GameObject startMenuUI;
    [SerializeField] private GameObject gameOverUI;

    GameManager gameManager;

    private void Start()
    {
        gameManager = GameManager.instance;
        gameManager.onGameOver.AddListener(ActivateGameOverUI);
    }

    public void PlayButtonHandler()
    {
        gameManager.StartGame();
        
    }

    public void ActivateGameOverUI()
    {
        gameOverUI.SetActive(true);
    }
    private void OnGUI()
    {
        scoreUI.text = gameManager.PrettyScore();
    }
}
