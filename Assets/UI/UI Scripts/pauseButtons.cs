using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement; //dont think i need this


public class pauseButtons : MonoBehaviour {

    public GameObject pauseMenu;

    public void resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
    }


}
