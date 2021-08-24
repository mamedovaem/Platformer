using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private Canvas pauseMenu;
    [SerializeField] private Button soundButton;
    // Start is called before the first frame update
    void Start()
    {
        pauseMenu.gameObject.SetActive(false);

        if (PlayerPrefs.GetInt("IsSoundActive") == 0)
        {
            PlayerPrefs.SetInt("IsSoundActive", 0);
            Text soundButtonName = soundButton.GetComponentInChildren<Text>();
            soundButtonName.text = "Enable sound";

        }

        else
        {
            PlayerPrefs.SetInt("IsSoundActive", 1);
            Text soundButtonName = soundButton.GetComponentInChildren<Text>();
            soundButtonName.text = "Disable sound";
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClickPause()
    {
        if (Time.timeScale > 0)
        {
            Time.timeScale = 0;
            
        }

        pauseMenu.gameObject.SetActive(true);

    }

    public void OnClickResume()
    {
        pauseMenu.gameObject.SetActive(false);
        Time.timeScale = 1;
    }

    public void OnClickSound()
    {
        if (PlayerPrefs.GetInt("IsSoundActive") == 1)
        {
            PlayerPrefs.SetInt("IsSoundActive", 0);
            Text soundButtonName = soundButton.GetComponentInChildren<Text>();
            soundButtonName.text = "Enable sound";

        }

        else
        {
            PlayerPrefs.SetInt("IsSoundActive", 1);
            Text soundButtonName = soundButton.GetComponentInChildren<Text>();
            soundButtonName.text = "Disable sound";
        }

    }

    public void OnClickMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void OnClickExit()
    {
        Application.Quit();
    }
}
