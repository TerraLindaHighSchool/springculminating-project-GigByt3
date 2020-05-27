using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Load_Level : MonoBehaviour
{
    // Array of levels scene names
    private string[] levels = {"Lvl_1", "Lvl_2", "Lvl_3", "Lvl_4", "Lvl_5", "Lvl_6", "Lvl_7"};

    //to be called to load new scenes.
    public void Load(int level)
    {
        SceneManager.LoadScene(levels[level-1]);
    }
}
