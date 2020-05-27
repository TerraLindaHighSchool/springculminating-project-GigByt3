using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Restart : MonoBehaviour
{
    public static bool newGame = true;
    public GameObject gameOver;
    public GameObject accent;

    public void Set()
    {
        newGame = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        if(newGame)
        {
            gameOver.SetActive(false);
            accent.SetActive(true);
        }
        else
        {
            gameOver.SetActive(true);
            accent.SetActive(false);
        }
    }

}
