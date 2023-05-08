using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public float towerHP;

    public GameObject loadingPanel;
    public GameObject turretsPanel;
    public GameObject gameOverPanel;

    public TextMeshProUGUI towerHPText;
    public TextMeshProUGUI waveLevelText;
    public TextMeshProUGUI coinsText;
    public TextMeshProUGUI gameOverText;
    public GameObject waveLevelButton;

    private GameObject cameraObj;

    MapGenerator mapGenerator;
    WaveManager waveManager;

    public int coins = 1000;
    private int destroyedEnemy = 0;

    private IEnumerator checkWaveEnemies;

    void Start()
    {
        cameraObj = GameObject.Find("Main Camera");
        mapGenerator = GetComponent<MapGenerator>();
        waveManager = GetComponent<WaveManager>();
        StartCoroutine(generateMap());

    }

    IEnumerator generateMap()
    {
        yield return StartCoroutine(mapGenerator.GenerateMap());
        yield return setWaveManagerConfig();
        startGame();
    }

    IEnumerator setWaveManagerConfig()
    {
        waveManager.SetPathCells(mapGenerator.getPathCells());
        waveManager.SetEnemyBeginCell(mapGenerator.getEnemyBaseObject().transform);
        yield return null;
    }

    private void startGame()
    {
        enableGameInfo();
        cameraObj.GetComponent<CameraMovement>().enabled = true;
    }

    private void enableGameInfo()
    {
        loadingPanel.SetActive(false);
        turretsPanel.SetActive(true);
        towerHPText.gameObject.SetActive(true);
        waveLevelText.gameObject.SetActive(true);
        coinsText.gameObject.SetActive(true);

        towerHPText.SetText(this.towerHPText.text + " " + towerHP);
        coinsText.SetText("Coins: " + coins);
    }

    private void disableGameInfo()
    {
        turretsPanel.SetActive(false);
        towerHPText.gameObject.SetActive(false);
        waveLevelText.gameObject.SetActive(false);
        coinsText.gameObject.SetActive(false);
    }

    public void EnemyAttackedTower(float enemyDmg)
    {
        towerHP -= enemyDmg;
        towerHPText.SetText("Tower HP: " + towerHP);

        if (towerHP <= 1)
        {
            GameOver("Game Over! :(");
        }
    }

    public void ReadyNextWave()
    {
        if (waveManager.getNextWave())
        {

            if (checkWaveEnemies != null)
            {
                StopCoroutine(checkWaveEnemies);
            }

            destroyedEnemy = 0;

            waveLevelButton.gameObject.SetActive(false);

            waveLevelText.SetText("Wave: " + waveManager.getWaveLevel() + "/" + waveManager.getMaxWaveLevel());
            checkWaveEnemies = checkAllEnemy();

            StartCoroutine(checkWaveEnemies);
        }
    }

    private void GameOver(string text)
    {
        waveLevelButton.gameObject.SetActive(false);
        disableGameInfo();
        gameOverText.SetText(text);
        gameOverPanel.SetActive(true);
    }

    private void ShowReadyButton()
    {
        waveLevelButton.gameObject.SetActive(true);
    } 

    public void AddEnemyDestroyed()
    {
        destroyedEnemy++;
    }

    public void AddCoins(int randomCoins)
    {
        coins+=randomCoins;
        coinsText.SetText("Coins: " + coins);
    }

    public int GetCoins()
    {
        return coins;
    }

    public void minusCoins(int price)
    {
        if(coins > 0)
        {
            coins -= price;
            coinsText.SetText("Coins: " + coins);
        }
    }

    IEnumerator checkAllEnemy()
    {
        while (true)
        {
            Debug.Log("Ellenorzes");
            Debug.Log("[Destroyed]: " + destroyedEnemy + " [AllEnemy]: " + waveManager?.GetAllEnemyCount());
            if(destroyedEnemy == waveManager?.GetAllEnemyCount())
            {
                if(waveManager?.getMaxWaveLevel() == waveManager?.getWaveLevel())
                {
                    GameOver("Game Over! :)");
                }
                else
                {
                    if(towerHP > 0)
                    {
                        ShowReadyButton();
                    }  
                }

                break;
            }

            yield return new WaitForSeconds(0.5f);
        }

        yield return null;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void Update()
    {
        
    }
}
