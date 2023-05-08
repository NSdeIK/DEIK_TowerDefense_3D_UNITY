using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuSystem : MonoBehaviour
{

    public GameObject NewGameButton;
    public GameObject DifficultyPanel;

    public void NewGame()
    {
        NewGameButton?.gameObject.SetActive(false);
        DifficultyPanel?.gameObject.SetActive(true);
    }

    public void NewGameWithEasyDifficulty()
    {
        //TODO - Könnyû scene
        SceneManager.LoadScene(1);
        Debug.Log("Könnyû mód aktiválva");
    }

    public void NewGameWithHardDifficulty()
    {
        //TODO - Nehéz
        //scene
        SceneManager.LoadScene(2);
        Debug.Log("Nehéz mód aktiválva");
    }

}
