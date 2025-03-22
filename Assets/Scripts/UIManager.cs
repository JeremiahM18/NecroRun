using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreUI;

    GameManager gameManager;

    private void Start()
    {
        gameManager = GameManager.instance;
    }

    private void OnGUI()
    {
        scoreUI.text = gameManager.PrettyScore();
    }
}
