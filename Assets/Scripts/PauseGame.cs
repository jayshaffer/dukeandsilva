using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class PauseGame : MonoBehaviour {

	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }
	}

    public void Pause()
    {
        Canvas pauseCanvas = GetComponent<Canvas>();
        if (pauseCanvas.enabled == false)
        {
            pauseCanvas.enabled = true;
            Time.timeScale = 0;
        }
        else
        {
            pauseCanvas.enabled = false;
            Time.timeScale = 1;
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1;
    }
    
    public void Quit()
    {
        Application.Quit();
    }
}
