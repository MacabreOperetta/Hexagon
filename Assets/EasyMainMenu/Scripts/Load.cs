using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Load : MonoBehaviour
{
    public GameMode game;
    // Start is called before the first frame update
    public void LoadGame(string levelname)
    {
        SceneManager.LoadScene(levelname);
        int count = PlayerPrefs.GetInt("Count");
        game.Colors.Clear();
        for (int i = 0; i < count; i++)
        {
            game.Colors.Add(PlayerPrefsX.GetColor(i.ToString()));
        }

    }
}
