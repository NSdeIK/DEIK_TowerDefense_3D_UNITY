using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopSystemManager : MonoBehaviour
{
    TurretBuilderManager turretBuilderManager;
    void Start()
    {
        turretBuilderManager = TurretBuilderManager.instance;
    }

    public void BuyAlapTurret()
    {
        turretBuilderManager.SetSelectedTurretToBuild(0);
    }

    public void BuyShotgunTurret()
    {
        turretBuilderManager.SetSelectedTurretToBuild(1);
    }

    public void BuyMinigunTurret()
    {
        turretBuilderManager.SetSelectedTurretToBuild(2);
    }

}
