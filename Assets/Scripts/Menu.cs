using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [SerializeField] private InputField nameField;
    private void Start()
    {
        if (PlayerPrefs.HasKey("PlayerName"))
            nameField.text = PlayerPrefs.GetString("PlayerName");
    }

    public void OnEndEditName()
    {
        PlayerPrefs.SetString("PlayerName", nameField.text);
    }
    public void OnClickPlay()
    {
        SceneManager.LoadScene(3);
        Time.timeScale = 1;
    }
    public void OnClickLevel()
    {
        SceneManager.LoadScene(2);
    }

    public void OnClickExit()
    {
        Application.Quit();
    }
}
