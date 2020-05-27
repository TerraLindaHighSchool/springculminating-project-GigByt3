using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Load_Level : MonoBehaviour
{
    private int tick;
    // Array of levels scene names
    private string[] levels = {"Lvl_1", "Lvl_2", "Lvl_3", "Lvl_4", "Lvl_5", "Lvl_6", "Lvl_7"};

    //to be called to load new scenes.
    public void Load(int level)
    {
        if (level == 1) { GameObject.Find("Canvas").GetComponent<Restart>().Set(); }
        SceneManager.LoadScene(levels[level-1]);
    }

    private void Update()
    {
        if (GameObject.Find("Player") == null && SceneManager.GetActiveScene().name != "Game Opener") {
            tick++;
            if (tick >= 100)
            {
                SceneManager.LoadScene("Game Opener");
                GameObject.Find("Sphere").GetComponent<Letter_TP>().setLevel(1);
            }
        }
    }
}
