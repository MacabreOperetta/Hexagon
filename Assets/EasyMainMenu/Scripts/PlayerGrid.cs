using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerGrid : MonoBehaviour
{
    public Text gridtext;
    private int xcount;
    private int ycount;
    public GameMode game;
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetInt("GridX") == 0)
            PlayerPrefs.SetInt("GridX", 8);
        if (PlayerPrefs.GetInt("GridY") == 0)
            PlayerPrefs.SetInt("GridY", 9);

        gridtext.text = PlayerPrefs.GetInt("GridX").ToString() + "X" + PlayerPrefs.GetInt("GridY").ToString();
        game.GridXLength = PlayerPrefs.GetInt("GridX");
        game.GridYLength = PlayerPrefs.GetInt("GridY");
    }

    // Update is called once per frame
    void Update()
    {
        gridtext.text = PlayerPrefs.GetInt("GridX").ToString() + "X" + PlayerPrefs.GetInt("GridY").ToString();
        game.GridXLength = PlayerPrefs.GetInt("GridX");
        game.GridYLength = PlayerPrefs.GetInt("GridY");
    }
    public void GridYplus()
    {
        ycount = PlayerPrefs.GetInt("GridY");
        ycount++;
        PlayerPrefs.SetInt("GridY", ycount);
    }
    public void GridYminus()
    {
        ycount = PlayerPrefs.GetInt("GridY");
        if (ycount == 0)
            ycount = 0;
        else
        { ycount--;
        }

        PlayerPrefs.SetInt("GridY", ycount);
    }
    public void GridXplus()
    {
        xcount = PlayerPrefs.GetInt("GridX");
        xcount++;
        PlayerPrefs.SetInt("GridX", xcount);
    }
    public void GridXminus()
    {
        xcount = PlayerPrefs.GetInt("GridX");
        if (xcount == 0)
            xcount = 0;
        else
        {
            xcount--;
        }
        PlayerPrefs.SetInt("GridX", xcount);
    }
}
