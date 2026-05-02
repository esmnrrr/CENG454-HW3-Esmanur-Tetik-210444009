using UnityEngine;
using UnityEngine.Pool;

public class EnemySpawner : MonoBehaviour
{
    public BasicEnemy enemyPrefab;
    public Transform[] spawnPoints; // Düþmanlarýn doðacaðý noktalar
    public float spawnInterval = 5f; // Kaç saniyede bir düþman çýkacak?

    private ObjectPool<BasicEnemy> enemyPool;
    private float timer;

    void Start()
    {
        // Havuzun kurulumu
        enemyPool = new ObjectPool<BasicEnemy>(
            CreateEnemy,
            OnTakeEnemyFromPool,
            OnReturnEnemyToPool,
            OnDestroyEnemy,
            true, 10, 20);
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            enemyPool.Get(); // Havuzdan 1 düþman iste
            timer = 0;
        }
    }

    private BasicEnemy CreateEnemy()
    {
        BasicEnemy enemy = Instantiate(enemyPrefab);
        enemy.SetPool(enemyPool); 
        return enemy;
    }

    private void OnTakeEnemyFromPool(BasicEnemy enemy)
    {
        Transform randomSpawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        enemy.transform.position = randomSpawnPoint.position;
        enemy.gameObject.SetActive(true);
    }

    private void OnReturnEnemyToPool(BasicEnemy enemy)
    {
        enemy.gameObject.SetActive(false); // Görünmez yap
    }

    private void OnDestroyEnemy(BasicEnemy enemy)
    {
        Destroy(enemy.gameObject);
    }
}