using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerStats : MonoBehaviour
{
    private int hexcount=5;
    public Text counttext;
    public GameMode currentgamemode;
    void Start()
    {
        if (PlayerPrefs.GetInt("Count") == 0)
            PlayerPrefs.SetInt("Count", 5);
        counttext.text = PlayerPrefs.GetInt("Count").ToString();
        for (int i = 0; i < currentgamemode.Colors.Count; i++)
        {
            currentgamemode.Colors[i] = PlayerPrefsX.GetColor((i).ToString());
        }

    }
    void Update()
    {
        counttext.text = PlayerPrefs.GetInt("Count").ToString();
    }
    public void minus()
    {
        hexcount = PlayerPrefs.GetInt("Count");
        if (hexcount == 0)
            hexcount = 0;
        else
        {
            hexcount--;
            //currentgamemode.Colors.RemoveAt(hexcount - 1);
            PlayerPrefs.SetInt("Count", hexcount);
        }

        
        //currentgamemode.Colors.Capacity = PlayerPrefs.GetInt("Count");
    }
    public void plus()
    {
        hexcount = PlayerPrefs.GetInt("Count");
        hexcount++;
        PlayerPrefs.SetInt("Count", hexcount);
        //currentgamemode.Colors.Add(PlayerPrefsX.GetColor((hexcount-1).ToString()));

    }

}
