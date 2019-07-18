using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
public class DropGetValue : MonoBehaviour
{
    public Dropdown drop;
    private Image dropcolor;
    Color32 color = new Color32(0, 0, 0, 0);
    private void Awake()
    {

    }

    
    void Start()
    {
        if (PlayerPrefsX.GetColor(drop.value.ToString()) == color)
        {
            int r = Random.Range(0, 256);
            int g = Random.Range(0, 256);
            int b = Random.Range(0, 256);
            Color32 x = new Color32((byte)r, (byte)g, (byte)b, 255);
            PlayerPrefsX.SetColor(drop.value.ToString(), x);
            drop.image.color = PlayerPrefsX.GetColor(drop.value.ToString());

        }
        else
        {
            drop.image.color = PlayerPrefsX.GetColor(drop.value.ToString());
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerPrefsX.GetColor(drop.value.ToString()) == color)
        {
            int r = Random.Range(0, 256);
            int g = Random.Range(0, 256);
            int b = Random.Range(0, 256);
            Color32 x = new Color32((byte)r, (byte)g, (byte)b, 255);
            PlayerPrefsX.SetColor(drop.value.ToString(), x);
            drop.image.color = PlayerPrefsX.GetColor(drop.value.ToString());
            PlayerPrefs.SetInt("CurrentDropValue", drop.value);
        }
        else {
            drop.image.color = PlayerPrefsX.GetColor(drop.value.ToString());
        }
    }
}
