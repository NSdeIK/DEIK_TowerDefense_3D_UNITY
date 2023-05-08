using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public float towerHP;

    public GameObject loadingPanel;
    public GameObject turretsPanel;
    public GameObject gameInfoPanel;

    public TextMeshProUGUI towerHPText;
    public TextMeshProUGUI waveLevelText;
    public TextMeshProUGUI coinsText;
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
        towerHPText.enabled = false;
        waveLevelText.enabled = false;
        coinsText.enabled = false;
    }

    public void EnemyAttackedTower(float enemyDmg)
    {
        towerHP -= enemyDmg;
        towerHPText.SetText("Tower HP: " + towerHP);

        if (towerHP <= 1)
        {
            //TODO - GAME OVER!
        }
    }

    public void ReadyNextWave()
    {
        if(checkWaveEnemies != null)
        {
            StopCoroutine(checkWaveEnemies);
        }

        destroyedEnemy = 0;

        waveManager.getNextWave();
        waveLevelButton.gameObject.SetActive(false);

        waveLevelText.SetText("Wave: " + waveManager.getWaveLevel() + "/" + waveManager.getMaxWaveLevel());
        checkWaveEnemies = checkAllEnemy();
       
        StartCoroutine(checkWaveEnemies);
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
                ShowReadyButton();
                break;
            }

            yield return new WaitForSeconds(0.5f);
        }

        yield return null;
    }

    void Update()
    {
        
    }
}
