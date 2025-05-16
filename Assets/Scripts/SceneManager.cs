using System;
using System.Collections;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    public float waveStartDelay;
    public int enemySpawnMax;
    public float enemySpawnMaxDelay;
    public int enemySpawnMultiplier;

    private int wave;
    private bool waveSpawnComplete = true;


    public GameObject enemyPrefab;
    public GameObject pickupPrefab;

    private SceneUI sceneUI;
    private Player player;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        wave = 0;
        sceneUI = GameObject.Find("SceneUI").GetComponent<SceneUI>();
        player = GameObject.Find("Player").GetComponent<Player>();
        player.PlayerDied += GameOver;
    }

    void Update()
    {
        sceneUI.SetPlayerHealth(player.health, 100);
        int enemyCount = FindObjectsByType<Enemy>(FindObjectsSortMode.None).Length;
        if (enemyCount == 0 && waveSpawnComplete == true)
        {
            wave++;
            StartCoroutine(sceneUI.ShowWaveIntroText(wave));
            StartCoroutine(SpawnEnemyWave(wave * enemySpawnMultiplier));
        }
    }

    private void GameOver()
    {
        sceneUI.ShowGameOverScreen();
    }

    private Vector3 GetRandomEnemySpawnPos(GameObject obj)
    {
        // Spawn position will be between a minimum and maximum distance from the player so
        // we will pick a random direction then randomly scale to find our position
        float dist = UnityEngine.Random.Range(10, 20);
        float angle = UnityEngine.Random.Range(0f, 360f);
        return new Vector3((float)Math.Sin(angle) * dist, obj.transform.position.y, (float)Math.Cos(angle) * dist);
    }

    IEnumerator SpawnEnemyWave(int n)
    {
        Debug.Log("Spawning " + n + " enemies");
        waveSpawnComplete = false;
        SpawnPickups();
        yield return new WaitForSeconds(waveStartDelay);
        for (int i = 0; i < n; i++)
        {
            // If we have already spawned 
            if (i > 0 && i % enemySpawnMax == 0)
            {
                yield return new WaitForSeconds(enemySpawnMaxDelay);
            }
            Instantiate(enemyPrefab, GetRandomEnemySpawnPos(enemyPrefab), enemyPrefab.transform.rotation);
            yield return new WaitForSeconds(UnityEngine.Random.Range(0.01f, 0.1f));
        }
        waveSpawnComplete = true;
    }

    void SpawnPickups()
    {
        int itemCount = FindObjectsByType<Pickup>(FindObjectsSortMode.None).Length;
        if (itemCount <= 0)
        {
            Instantiate(pickupPrefab, GetRandomEnemySpawnPos(pickupPrefab), pickupPrefab.transform.rotation);
        }
    }
}
