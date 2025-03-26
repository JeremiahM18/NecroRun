using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    private PlayerMovement player;

    private void Start()
    {
        player = GetComponent<PlayerMovement>();
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Obstacle"))
        {
            Debug.Log("Player hit an obstacle!");
            gameObject.SetActive(false);
            GameManager.Instance.GameOver();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("TallObstacle"))
        {
            if (player.IsJumping)
            {
                return;
            }
        }

        else if (collision.gameObject.CompareTag("LowObstacle"))
        {
            if (player.IsSliding)
            {
                return;
            }
        }

        else if (collision.gameObject.CompareTag("Obstacle"))
        {
            Debug.Log("Player triggered an obstacle!");
        }
        GameManager.Instance.GameOver();
        gameObject.SetActive(false);
    }
}
