using UnityEngine;

public class EnemySpawnerController : MonoBehaviour
{
    [SerializeField]
    public GameObject enemyPrefab;
    [SerializeField]
    public float minSpawnTime = 1f;
    [SerializeField]
    public float maxSpawnTime = 5f;
    [SerializeField]
    public float spawnInterval;
    void Start()
    {
        spawnInterval = Random.Range(minSpawnTime, maxSpawnTime);
    }

    // Update is called once per frame
    void Update()
    {
        spawnInterval -= Time.deltaTime;
        if (spawnInterval <= 0)
        {
            SpawnEnemy();
            spawnInterval = Random.Range(minSpawnTime, maxSpawnTime);
        }
        
    }

    public void SpawnEnemy()
    {
        GameObject enemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity);
        enemy.transform.SetParent(transform);
    }
}
