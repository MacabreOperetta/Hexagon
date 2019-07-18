using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour {

    Animator anim;

    public string newGameSceneName;
    public int quickSaveSlotID;

    [Header("Options Panel")]
    public GameObject MainOptionsPanel;
    public GameObject StartGameOptionsPanel;
    public GameObject GamePanel;
    public GameObject ControlsPanel;
    public GameObject GfxPanel;
    public GameObject LoadGamePanel;
    public GameObject background;
    public Dropdown drop;
    public AudioSource audiosource;
    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();
        //new key
        PlayerPrefs.SetInt("quickSaveSlot", quickSaveSlotID);
    }

    #region Open Different panels

    public void openOptions()
    {
        //enable respective panel
        MainOptionsPanel.SetActive(true);
        StartGameOptionsPanel.SetActive(false);

        //play anim for opening main options panel
        anim.Play("buttonTweenAnims_on");

        //play click sfx
        playClickSound();

        //enable BLUR
        //Camera.main.GetComponent<Animator>().Play("BlurOn");
       
    }

    public void openStartGameOptions()
    {
        //enable respective panel
        MainOptionsPanel.SetActive(false);
        StartGameOptionsPanel.SetActive(true);

        //play anim for opening main options panel
        anim.Play("buttonTweenAnims_on");

        //play click sfx
        playClickSound();

        //enable BLUR
        //Camera.main.GetComponent<Animator>().Play("BlurOn");
        
    }

    public void openOptions_Game()
    {
        //enable respective panel
        playClickSound();
        GamePanel.SetActive(true);
        ControlsPanel.SetActive(false);
        MainOptionsPanel.SetActive(false);
        GfxPanel.SetActive(false);
        LoadGamePanel.SetActive(false);
        PlayerPrefs.SetInt("Dropdown",drop.value);
        //play anim for opening game options panel
        anim.Play("OptTweenAnim_on");

        //play click sfx


    }
    public void openOptions_Controls()
    {
        //enable respective panel
        playClickSound();
        GamePanel.SetActive(false);
        ControlsPanel.SetActive(true);
        GfxPanel.SetActive(false);
        LoadGamePanel.SetActive(false);

        //play anim for opening game options panel
        anim.Play("OptTweenAnim_on");

        //play click sfx


    }
    public void openOptions_Gfx()
    {
        //enable respective panel
        playClickSound();
        GamePanel.SetActive(false);
        ControlsPanel.SetActive(false);
        GfxPanel.SetActive(true);
        MainOptionsPanel.SetActive(false);
        LoadGamePanel.SetActive(false);

        //play anim for opening game options panel
        anim.Play("OptTweenAnim_on");

        //play click sfx


    }

    public void openContinue_Load()
    {
        //enable respective panel
        playClickSound();
        GamePanel.SetActive(false);
        ControlsPanel.SetActive(false);
        GfxPanel.SetActive(false);
        LoadGamePanel.SetActive(true);

        //play anim for opening game options panel
        anim.Play("OptTweenAnim_on");

        //play click sfx


    }

    public void newGame()
    {
        if (!string.IsNullOrEmpty(newGameSceneName))
            SceneManager.LoadScene(newGameSceneName);
        else
            Debug.Log("Please write a scene name in the 'newGameSceneName' field of the Main Menu Script and don't forget to " +
                "add that scene in the Build Settings!");
    }
    #endregion

    #region Back Buttons

    public void back_options()
    {
        //simply play anim for CLOSING main options panel
        playClickSound();
        anim.Play("buttonTweenAnims_off");
        MainOptionsPanel.SetActive(false);
        background.GetComponent<Image>().color = new Color(227, 227, 229);
        //disable BLUR
        // Camera.main.GetComponent<Animator>().Play("BlurOff");

        //play click sfx

    }

    public void back_options_panels()
    {
        //simply play anim for CLOSING main options panel
        playClickSound();
        anim.Play("OptTweenAnim_off");
        GamePanel.SetActive(false);
        MainOptionsPanel.SetActive(true);
        //play click sfx


    }
    public void SaveColor()
    {
        playClickSound();
        PlayerPrefsX.SetColor(PlayerPrefs.GetInt("Dropdown").ToString(), PlayerPrefsX.GetColor("CurrentColor"));
        anim.Play("OptTweenAnim_off");
        GamePanel.SetActive(false);
        MainOptionsPanel.SetActive(true);
        //play click sfx

    }
    public void Quit()
    {
        Application.Quit();
    }
    #endregion

    #region Sounds
    public void playHoverClip()
    {
       
    }

    void playClickSound() {
        audiosource.Play();
    }


    #endregion
}
