using UnityEngine;
using UnityEngine.Pool;

public class EnemySpawner : MonoBehaviour
{
    public BasicEnemy enemyPrefab;
    public Transform[] spawnPoints; // Düþmanlarýn doðacaðý noktalar
    public float spawnInterval = 2f; // Kaç saniyede bir düþman çýkacak?

    private ObjectPool<BasicEnemy> enemyPool;
    private float timer;

    void Start()
    {
        // Havuzun kurulumu (Yaratma, Çekme, Ýade Etme, Yok Etme, Hata Kontrolü, Varsayýlan Boyut, Max Boyut)
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

    // 1. Havuz boþsa yeni düþman yarat
    private BasicEnemy CreateEnemy()
    {
        BasicEnemy enemy = Instantiate(enemyPrefab);
        enemy.SetPool(enemyPool); // Düþmana "Senin evin bu havuz" diyoruz
        return enemy;
    }

    // 2. Düþman havuzdan sahneye çýkarken ne olacak?
    private void OnTakeEnemyFromPool(BasicEnemy enemy)
    {
        Transform randomSpawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        enemy.transform.position = randomSpawnPoint.position;
        enemy.gameObject.SetActive(true);
    }

    // 3. Düþman havuza dönerken ne olacak?
    private void OnReturnEnemyToPool(BasicEnemy enemy)
    {
        enemy.gameObject.SetActive(false); // Görünmez yap
    }

    // 4. Havuz kapasitesi dolarsa ne olacak?
    private void OnDestroyEnemy(BasicEnemy enemy)
    {
        Destroy(enemy.gameObject);
    }
}