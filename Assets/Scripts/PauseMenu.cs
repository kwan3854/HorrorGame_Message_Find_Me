using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private Button goTitleButton;
    [SerializeField] private Button resumeButton;

    void Awake()
    {
        pauseMenu.SetActive(false);
        // goTitleButton = GameObject.Find("GoTitleButton").GetComponent<Button>();
        // resumeButton = GameObject.Find("ResumeButton").GetComponent<Button>();
        goTitleButton.onClick.AddListener(GoTitle);
        resumeButton.onClick.AddListener(Resume);
    }

    public void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }

    public void GoTitle()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Title");
    }

}
