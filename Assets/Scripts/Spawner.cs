using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.VFX;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject[] obstaclePrefabs;
    [SerializeField] private Transform obstacleParent;
    public float obstacleSpawnTime = 2f;
    public float obstacleSpeed = 1f;

    private float timeUntilOstacleSpawn;

    private void Start()
    {
        GameManager.instance.onGameOver.AddListener(clearObstacles);
    }


    private void Update()
    {
        if (GameManager.instance.isPlaying) { 
            SpawnLoop();
        }
    }

    private void SpawnLoop()
    {
        timeUntilOstacleSpawn += Time.deltaTime;

        if(timeUntilOstacleSpawn >= obstacleSpawnTime)
        {
            Spawn();
            timeUntilOstacleSpawn = 0;
        }
    }

    private void clearObstacles()
    {
        foreach (Transform child in obstacleParent)
        {
            Destroy(child.gameObject);
        }
    }

    private void Spawn()
    {
        GameObject obstacleToSpawn = obstaclePrefabs[Random.Range(0, obstaclePrefabs.Length)];

        GameObject spawnedObstacle = Instantiate(obstacleToSpawn, transform.position, Quaternion.identity);
        spawnedObstacle.transform.parent = obstacleParent;

        Rigidbody2D obstacleRB = spawnedObstacle.GetComponent<Rigidbody2D>();
        obstacleRB.linearVelocity = Vector2.left * obstacleSpeed;
    }
}
