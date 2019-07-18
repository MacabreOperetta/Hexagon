using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DropdownItems : MonoBehaviour
{
    public Dropdown drop;
    List<string> list = new List<string>();
    string s = "Hex ";
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetInt("Count") == 0)
            drop.ClearOptions();
        
        else
        {
            for (int i = 0; i < PlayerPrefs.GetInt("Count"); i++)
            {
                list.Add(s + (i+1));
            }
            drop.options.Clear();
            foreach (string option in list)
            {
                drop.options.Add(new Dropdown.OptionData(option));
            }
        }
    }

    // Update is called once per frame
    public void updatedrop()
    {
        if (PlayerPrefs.GetInt("Count") == 0)
            drop.ClearOptions();

        else
        {
            list.Clear();
            for (int i = 0; i < PlayerPrefs.GetInt("Count"); i++)
            {
                list.Add(s + (i + 1));
            }
            drop.options.Clear();
            foreach (string option in list)
            {
                drop.options.Add(new Dropdown.OptionData(option));
            }
        }
    }
}
