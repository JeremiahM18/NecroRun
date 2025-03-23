using UnityEngine;

public class RevivePlayer : MonoBehaviour
{
    private void Start()
    {
        GameManager.instance.onPlay.AddListener(revivePlayer);

    }

    private void revivePlayer()
    {
        gameObject.SetActive(true);
    }
}


