using UnityEngine;

public class GroundMovement : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float tileHeight = 72f;
    //[SerializeField] private float resetPositionY = -25f;
    //[SerializeField] private float startPositionY = 25f;

    private void Update()
    {
        transform.Translate(Vector3.down * speed * Time.deltaTime);

        if(transform.position.y <= -tileHeight)
        {
            transform.position = new Vector3(transform.position.x, tileHeight, 0);
        }
    }
}
