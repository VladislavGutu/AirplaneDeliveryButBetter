using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab; // Префаб врага
    public float initialSpawnDelay = 2f; // Начальная задержка перед первым спауном
    public float spawnRate = 5f; // Исходная частота спауна
    public float spawnRateIncrease = 0.1f; // Увеличение частоты спауна
    public float minSpawnRate = 1f; // Минимальная частота спауна
    public float enemyLifetime = 15f; // Время жизни врага
    public float spawnRadius = 10f; // Радиус полукруга

    private float nextSpawnTime;
    private Vector3 lastKnownPlayerPosition;

    void Start()
    {
        nextSpawnTime = initialSpawnDelay;
        lastKnownPlayerPosition = transform.position; // Используем начальную позицию объекта, который содержит скрипт
    }

    void Update()
    {
        if (Time.time >= nextSpawnTime)
        {
            SpawnEnemy();

            nextSpawnTime = Time.time + 1f / spawnRate;
            spawnRate = Mathf.Max(spawnRate - spawnRateIncrease, minSpawnRate);
        }
    }

    void SpawnEnemy()
    {
        int numberOfSpawns = 5; // Количество точек спауна в полукруге
        SetLastKnownPlayerPosition(transform.position);
        for (int i = 0; i < numberOfSpawns; i++)
        {
            float angle = Random.Range(0f, Mathf.PI); // Угол от 0 до π (половина круга)
            float spawnX = lastKnownPlayerPosition.x + Mathf.Cos(angle) * spawnRadius;
            float spawnZ = lastKnownPlayerPosition.z + Mathf.Sin(angle) * spawnRadius;
            Vector3 spawnPosition = new Vector3(spawnX, lastKnownPlayerPosition.y, spawnZ);

            GameObject newEnemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
            Destroy(newEnemy, enemyLifetime); // Уничтожаем врага через определенное время
        }
    }

    public void SetLastKnownPlayerPosition(Vector3 position)
    {
        lastKnownPlayerPosition = position;
    }
}
