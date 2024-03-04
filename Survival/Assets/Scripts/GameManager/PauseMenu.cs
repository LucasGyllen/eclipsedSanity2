using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour
{

    public Slider brightnessSlider;
    public Light spotLight;

    public GameObject pauseMenu;
    public KeyCode pauseKey;
    public static bool isPaused;


    // Start is called before the first frame update
    void Start()
    {
        pauseMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

        brightnessSlider.onValueChanged.AddListener(ChangeBrightness);

        if (Input.GetKeyUp(pauseKey))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void ChangeBrightness(float value)
    {
        if (value <= 0.1f)
            spotLight.intensity = 0.1f;
        else
        spotLight.intensity = value;
    }

    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void RestartGame()
    {
        SceneManager.LoadSceneAsync(1);
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    public void QuítGame()
    {
        Application.Quit();
    }

}
