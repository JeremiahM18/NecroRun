using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.VFX;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject[] obstaclePrefabs;
    [SerializeField] private Transform obstacleParent;
    public float obstacleSpawnTime = 2f;
    [Range(0, 1)] public float obstacleSpawnTimeFactor = 0.1f;
    public float obstacleSpeed = 3f;
    [Range(0, 1)] public float obstacleSpeedFactor = 0.2f;

    private float _obstacleSpawnTime;
    private float _obstacleSpeed;
    private float timeAlive;
    private float timeUntilObstacleSpawn;

    private void Start()
    {
       GameManager.Instance.onGameOver.AddListener(clearObstacles);
       GameManager.Instance.onPlay.AddListener(resetFactors);
    }


    private void Update()
    {
        if (!GameManager.Instance.isPlaying)
        {
            return;
        } else 
            {
                timeAlive += Time.deltaTime;
                calculateFactors();
                SpawnLoop();
            }
    }

    private void SpawnLoop()
    {
        timeUntilObstacleSpawn += Time.deltaTime;

        if (timeUntilObstacleSpawn >= _obstacleSpawnTime)
        {
            Spawn();
            timeUntilObstacleSpawn = 0;
        }
    }
    
    private void Spawn()
    {
        float xPos = Random.Range(-1.6f, 1.6f);
        float yPos = 7f;
        Vector3 spawnPos = new Vector3(xPos, yPos, 0f);

        GameObject prefab = obstaclePrefabs[Random.Range(0, obstaclePrefabs.Length)];
        GameObject spawned = Instantiate(prefab, spawnPos, Quaternion.identity, obstacleParent);


        if (spawned.TryGetComponent(out Rigidbody2D rb))
        {
            rb.gravityScale = 0;
            rb.linearVelocity = Vector2.down * _obstacleSpeed;
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
        _obstacleSpawnTime = obstacleSpawnTime / Mathf.Pow(timeAlive, obstacleSpawnTimeFactor);
        _obstacleSpeed = obstacleSpeed * Mathf.Pow(timeAlive, obstacleSpeedFactor);
    }

    private void resetFactors()
    {
        timeAlive = 1f;
        _obstacleSpawnTime = obstacleSpawnTime;
        _obstacleSpeed = obstacleSpeed;
    }

    
}
