using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class GameManager : MonoBehaviour
{
    private static GameManager instance = null;
    private bool isUIEnabled = false;
    private bool isGamePaused = false;
    private List<string> gameUIs = new List<string>() { "PauseMenu", "MemoUI" };

    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject memoUI;

    [Header("Audio Configs")]
    [SerializeField] private GameObject UISound;
    [SerializeField] private AudioClip PauseSound;
    [SerializeField] private AudioClip ResumeSound;
    [SerializeField] private AudioClip HoverSound;

    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            // DontDestroyOnLoad(gameObject);
        }

        Assert.IsNotNull(pauseMenu, "Pause Menu not found");
        Assert.IsNotNull(memoUI, "Memo UI not found");
        pauseMenu.SetActive(false);
        memoUI.SetActive(false);
        isUIEnabled = false;
        isGamePaused = false;
        Time.timeScale = 1f;
    }

    // void Start()
    // {

    // }

    public static GameManager Instance
    {
        get
        {
            return instance;
        }
    }

    public bool IsUIEnabled
    {
        get
        {
            return isUIEnabled;
        }
        set
        {
            isUIEnabled = value;
        }
    }

    public bool IsGamePaused
    {
        get
        {
            return isGamePaused;
        }
        set
        {
            isGamePaused = value;
        }
    }


    public void LoadTitleScene()
    {
        // ResumeGame();

        UnityEngine.SceneManagement.SceneManager.LoadScene("Title");
    }

    public void LoadMainScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Main");
    }

    public void LoadGameOverScene()
    {
        // UnityEngine.SceneManagement.SceneManager.LoadScene("GameOver");
    }

    public void PauseGame()
    {
        // GameObject pauseMenu = GameObject.FindGameObjectWithTag("PauseMenu");
        // Assert.IsNotNull(pauseMenu);

        OpenGameUI(pauseMenu);
        isUIEnabled = true;
        isGamePaused = true;
        UISound.GetComponent<AudioSource>().clip = PauseSound;
        UISound.GetComponent<AudioSource>().Play();
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        // GameObject pauseMenu = GameObject.FindGameObjectWithTag("PauseMenu");
        // Assert.IsNotNull(pauseMenu);

        CloseGameUI(pauseMenu);
        isUIEnabled = false;
        isGamePaused = false;
        UISound.GetComponent<AudioSource>().clip = ResumeSound;
        UISound.GetComponent<AudioSource>().Play();
        Time.timeScale = 1f;
    }

    public void OpenGameUI(GameObject ui)
    {
        if (isUIEnabled)
        {
            Debug.Log(ui + " is already enabled");
        }
        else
        {
            if (gameUIs.Contains(ui.tag))
            {
                ui.SetActive(true);
                isUIEnabled = true;
            }
            else
            {
                Debug.Assert(false, ui + " is not tagged as GameUI");
            }
        }
    }

    public void CloseGameUI(GameObject ui)
    {
        if (isUIEnabled)
        {
            if (gameUIs.Contains(ui.tag))
            {
                isUIEnabled = false;
                ui.SetActive(false);
            }
            else
            {
                Debug.Assert(false, ui + " is not tagged as GameUI");
            }
        }
        else
        {
            Debug.Log(ui + " is already disabled");
        }
    }

    public void CloseAllGameUI()
    {
        foreach (string ui in gameUIs)
        {
            GameObject uiObject = GameObject.FindGameObjectWithTag(ui);
            if (uiObject != null)
            {
                // uiObject.PlaySound("Close");
                uiObject.SetActive(false);
            }
        }
        isUIEnabled = false;
    }

    public void PlayButtonHoverSound()
    {
        UISound.GetComponent<AudioSource>().clip = HoverSound;
        UISound.GetComponent<AudioSource>().Play();
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}