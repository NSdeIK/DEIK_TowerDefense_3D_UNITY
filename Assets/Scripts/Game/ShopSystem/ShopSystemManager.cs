using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopSystemManager : MonoBehaviour
{
    TurretBuilderManager turretBuilderManager;
    GameManager gameManager;
    void Start()
    {
        turretBuilderManager = TurretBuilderManager.instance;
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    public void BuyAlapTurret()
    {
        if (canBuy(250))
        {
            turretBuilderManager.SetSelectedTurretToBuild(0);
        }
    }

    public void BuyShotgunTurret()
    {
        if (canBuy(400))
        {
            turretBuilderManager.SetSelectedTurretToBuild(1);
        }
    }

    public void BuyMinigunTurret()
    {
        if (canBuy(600))
        {
            turretBuilderManager.SetSelectedTurretToBuild(2);
        }
    }


    private bool canBuy(int turretPrice)
    {
        int? coins = gameManager?.GetCoins();

        if (coins != null && (coins - turretPrice) >= 0)
        {
            gameManager?.minusCoins(turretPrice);
            return true;
        }

        Debug.Log("Nincs coins!");
        return false;
    }
}
