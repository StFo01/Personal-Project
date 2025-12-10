using System.Collections;
using System.Numerics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] enemies;
    public GameObject powerup;
    public float zEnemySpawn = 10.0f;
    public float xEnemySpawn = 20.0f;
    public float ySpawn = 0.5f;
    private float xPowerupSpawn = 14.0f;
    private float zPowerupSpawn = 6.0f;
    private float startDelay = 1.5f;
    private float enemySpawnTime = 3.0f;
    private float powerupSpawnTime = 5.0f;
    public Button restartButton;
    public GameObject player;
    public GameObject titleScreen;
    public AudioClip buttonSound;
    private AudioSource audioSource;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       GameObject audioObject = GameObject.Find("Audio Source");
       
        if (audioObject != null)
        {
            audioSource = audioObject.GetComponent<AudioSource>();
        }
    }
    

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator SpawnRandomEnemy()
    {   
        yield return new WaitForSeconds(startDelay);
        while(player != null)
        {
            float randomX = Random.Range(-xEnemySpawn, xEnemySpawn);
            float randomZ = Random.Range(-zEnemySpawn, zEnemySpawn);
            int randomIndex = Random.Range (0, enemies.Length);

            UnityEngine.Vector3 spawnPosXPositive = new UnityEngine.Vector3(randomX, ySpawn, zEnemySpawn);
            UnityEngine.Vector3 spawnPosXNegative = new UnityEngine.Vector3(randomX, ySpawn, -zEnemySpawn);
            UnityEngine.Vector3 spawnPosZPositive = new UnityEngine.Vector3(xEnemySpawn, ySpawn, randomZ);
            UnityEngine.Vector3 spawnPosZNegative = new UnityEngine.Vector3(-xEnemySpawn, ySpawn, randomZ);

            UnityEngine.Vector3 spawnPosPositive = (Random.Range(0, 2) == 0) ? spawnPosXPositive : spawnPosZPositive;
            UnityEngine.Vector3 spawnPosNegative = (Random.Range(0, 2) == 0) ? spawnPosXNegative : spawnPosZNegative;

            UnityEngine.Vector3 spawnPos = (Random.Range(0, 2) == 0) ? spawnPosNegative : spawnPosPositive;

            Instantiate(enemies[randomIndex], spawnPos, enemies[randomIndex].gameObject.transform.rotation);
            yield return new WaitForSeconds(enemySpawnTime);
        }
    }

    IEnumerator SpawnPowerup()
    {
        yield return new WaitForSeconds(powerupSpawnTime);
        while(player != null){
            float randomPowerUpX = Random.Range(-xPowerupSpawn, xPowerupSpawn);
            float randomPowerUpZ = Random.Range(-zPowerupSpawn, zPowerupSpawn);

            UnityEngine.Vector3 spawnPosPowerUp = new UnityEngine.Vector3(randomPowerUpX, ySpawn, randomPowerUpZ);
            Instantiate(powerup, spawnPosPowerUp, powerup.gameObject.transform.rotation);
            yield return new WaitForSeconds(powerupSpawnTime);
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        audioSource.PlayOneShot(buttonSound);
    }

    public void StartGame(float difficulty)
    {
        enemySpawnTime /= difficulty;
        StartCoroutine(SpawnRandomEnemy());
        StartCoroutine(SpawnPowerup());
        titleScreen.SetActive(false);
    }
}
