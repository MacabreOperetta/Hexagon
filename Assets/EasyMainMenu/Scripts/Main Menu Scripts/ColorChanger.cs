using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ColorChanger : MonoBehaviour
{
    public GameObject sliderred;
    public GameObject slidergreen;
    public GameObject sliderblue;
    public GameObject square;
    private Color currentcolor;
    // Start is called before the first frame update
    void Start()
    {

    }
    public void changecolor()
    {
        currentcolor = PlayerPrefsX.GetColor(PlayerPrefs.GetInt("Dropdown").ToString());
        sliderred.gameObject.GetComponent<Slider>().value = (currentcolor.r) * 255;
        slidergreen.gameObject.GetComponent<Slider>().value = (currentcolor.g) * 255;
        sliderblue.gameObject.GetComponent<Slider>().value = (currentcolor.b) * 255;

    }
    // Update is called once per frame
    void Update()
    {
        int r = (int)sliderred.gameObject.GetComponent<Slider>().value;
        int g = (int)slidergreen.gameObject.GetComponent<Slider>().value;
        int b = (int)sliderblue.gameObject.GetComponent<Slider>().value;
        Color32 a = new Color32((byte)r, (byte)g, (byte)b, 255);
        square.gameObject.GetComponent<Image>().color = a;
        PlayerPrefsX.SetColor("CurrentColor", a);
    }

}
