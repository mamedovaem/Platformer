using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelMenu : MonoBehaviour
{
  public void OnClickLevel1()
    {
        SceneManager.LoadScene(3);
        Time.timeScale = 1;
    }
    public void OnClickLevel2()
    {
        SceneManager.LoadScene(4);
        Time.timeScale = 1;
    }

}
