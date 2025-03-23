using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.VFX;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject[] obstaclePrefabs;
    [SerializeField] private Transform obstacleParent;
    public float obstacleSpawnTime = 2f;
    [Range(0, 1)] public float obstacleSpawnTimeFacor = 0.1f;
    public float obstacleSpeed = 1f;
    [Range(0, 1)] public float obstacleSpeedFactor = 0.2f;

    private float _obstacleSpawnTime;
    private float _obstacleSpeed;
    private float timeAlive;
    private float timeUntilOstacleSpawn;

    private void Start()
    {
        GameManager.instance.onGameOver.AddListener(clearObstacles);
       GameManager.instance.onPlay.AddListener(resetFactors);
    }


    private void Update()
    {
        if (GameManager.instance.isPlaying) { 
            timeAlive += Time.deltaTime;

            calculateFactors();

            SpawnLoop();
        }
    }

    private void SpawnLoop()
    {
        timeUntilOstacleSpawn += Time.deltaTime;

        if(timeUntilOstacleSpawn >= _obstacleSpawnTime)
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

    private void calculateFactors()
    {
        _obstacleSpawnTime = obstacleSpawnTime / Mathf.Pow(timeAlive, obstacleSpawnTimeFacor);
        _obstacleSpeed = obstacleSpeed * Mathf.Pow(timeAlive, obstacleSpeedFactor);
    }

    private void resetFactors()
    {
        timeAlive = 1f;
        _obstacleSpawnTime = obstacleSpawnTime;
        _obstacleSpeed = obstacleSpeed;
    }

    private void Spawn()
    {
        GameObject obstacleToSpawn = obstaclePrefabs[Random.Range(0, obstaclePrefabs.Length)];

        GameObject spawnedObstacle = Instantiate(obstacleToSpawn, transform.position, Quaternion.identity);
        spawnedObstacle.transform.parent = obstacleParent;

        Rigidbody2D obstacleRB = spawnedObstacle.GetComponent<Rigidbody2D>();
        obstacleRB.linearVelocity = Vector2.left * _obstacleSpeed;
    }
}
