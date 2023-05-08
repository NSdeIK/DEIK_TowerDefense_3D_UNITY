using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretBuilderManager : MonoBehaviour
{
    public static TurretBuilderManager instance;
    public GameObject[] turrets;
    private GameObject selectedTurretToBuild;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            return;
        }
    }

    public GameObject GetSelectedTurretToBuild()
    {
        return selectedTurretToBuild;
    }

    public void SetSelectedTurretToBuild(int index)
    {
        if(turrets.Length > 0 && turrets.Length > index)
        {
            selectedTurretToBuild = turrets[index];
        }
    }

    public void SetDefault()
    {
        selectedTurretToBuild = null;
    }

}
