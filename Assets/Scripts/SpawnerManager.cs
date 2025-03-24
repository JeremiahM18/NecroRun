using UnityEngine;

public class SpawnerManager : MonoBehaviour
{
    public GameObject[] zombiePrefabs;
    public GameObject[] rubblePrefabs;
    public GameObject[] glassPrefabs;
    public GameObject[] bridgePrefabs;
    public Transform obstacleParent;

    void SpawnObstacle()
    {
        int type = Random.Range(0, 4);

        switch (type)
        {
            case 0:
                SpawnFromArray(zombiePrefabs);
                break;
            case 1:
                SpawnFromArray(rubblePrefabs);
                break;
            case 2:
                SpawnFromArray(glassPrefabs);
                break;
            case 3:
                SpawnFromArray(bridgePrefabs);
                break;
        }
    }

    void SpawnFromArray(GameObject[] prefabArray)
    {
        GameObject prefab = prefabArray[Random.Range(0, prefabArray.Length)];
        Vector3 spawnPos = new Vector3(Random.Range(-2f, 2f), 6f, 0);
        Instantiate(prefab, spawnPos, Quaternion.identity, obstacleParent);
    }

    Vector3 GetSpawnPosition()
    {
        return new Vector3(Random.Range(-2f, 2f), 6f, 0);
    }
    #region Bridge
    private void SpawnBridge()
    {
        float gapWidth = 2.5f;
        float bridgeHalfWidth = 2f;
        float ySpawn = 6f;

        // Randomize which side the gap appears on
        bool gapLeft = Random.value < 0.5;

        // Left bridge
        if (!gapLeft)
        {
            Vector3 leftPos = new Vector3(-gapWidth / 2 - bridgeHalfWidth / 2, ySpawn, 0);
            GameObject leftBridge = Instantiate(
                bridgePrefabs[Random.Range(0, bridgePrefabs.Length)],
                leftPos, Quaternion.identity, obstacleParent);

            Rigidbody2D rbL = leftBridge.GetComponent<Rigidbody2D>();
            if (rbL != null)
            {
                rbL.linearVelocity = Vector2.down * 2f;
            }
        }

        // Right bridge
        if (gapLeft)
        {
            Vector3 rightPos = new Vector3(gapWidth / 2 + bridgeHalfWidth / 2, ySpawn, 0);
            GameObject rightBridge = Instantiate(
                bridgePrefabs[Random.Range(0, bridgePrefabs.Length)],
                rightPos, Quaternion.identity, obstacleParent);

            Rigidbody2D rbR = rightBridge.GetComponent<Rigidbody2D>();
            if (rbR != null)
            {
                rbR.linearVelocity = -Vector2.down * 2f;
            }
        }
    }
    #endregion

}
