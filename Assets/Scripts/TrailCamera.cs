using UnityEngine;

public class TrailCamera : MonoBehaviour
{
    public Transform player;
    public float smoothSpeed = 5f;
    public float yoffset = 3f;

    private void LateUpdate()
    {
        if (player != null) 
        {
            Vector3 currentPos = transform.position;
            float targetY = Mathf.Lerp(currentPos.y, player.position.y + yoffset, smoothSpeed * Time.deltaTime);

            transform.position = new Vector3(currentPos.x, targetY, currentPos.z);

        }
    }
}
