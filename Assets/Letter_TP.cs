using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Letter_TP : MonoBehaviour
{
    // Get the UI element from the corner of the screen
    public Text Clue;
    public static int level = 1;
    private bool playerIn;
    private int tick;
    private string[] password;
    private int playerProgress = 0;
    private string[] alphabet = {"a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z"};

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Letter_TP Fired " + level);
        System.Random randy = new System.Random();
        password = new string[level + 3];
        for(int i = 0; i < password.Length; i++)
        {
            password[i] = alphabet[randy.Next(alphabet.Length - 1)];
            Debug.Log(password[i]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(playerIn)
        {
            tick++;
            Debug.Log("tickCount: " + tick);
        } else {
            tick = 0;
        }

        if(tick >= 40)
        {
            Clue.text = password[playerProgress];
            tick = 0;
        }

        Debug.Log(playerProgress + "/" + password.Length);

        if(Input.GetKeyDown(password[playerProgress]))
        {
            Clue.text = ""; 
            playerProgress++;
            if(playerProgress >= password.Length)
            {
                this.gameObject.GetComponent<Load_Level>().Load(level++);
            }
        }
    }

    void OnTriggerEnter(Collider obj)
    {
        Debug.Log("collision Fired");
        if (obj.gameObject.tag == "Player")
        {
            playerIn = true;
        }
    }

    void OnTriggerExit(Collider obj)
    {
        if (obj.gameObject.tag == "Player")
        {
            playerIn = false;
        }
    }
}
