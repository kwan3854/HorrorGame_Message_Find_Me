using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class GameManager : MonoBehaviour
{
    private static GameManager instance = null;
    private bool isUIEnabled = false;
    private bool isGamePaused = false;
    private List<string> gameUIs = new List<string>() { "PauseMenu", "MemoUI", "PhoneUI" };

    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject memoUI;

    [Header("UI Audio Configs")]
    [SerializeField] private GameObject UISound;
    [SerializeField] private AudioClip ClickSound;
    [SerializeField] private AudioClip pauseSound;
    [SerializeField] private AudioClip resumeSound;
    [SerializeField] private AudioClip hoverSound;
    [SerializeField] private AudioClip memoOpenSound;
    [SerializeField] private AudioClip memoCloseSound;
    [SerializeField] private AudioClip typingSound;

    [Header("Game Play Audio Configs")]
    [SerializeField] private AudioSource gameAudio;
    [SerializeField] private AudioClip flashLightOnSound;

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

    void Start()
    {
        // ---------- Test Code ----------------------
        // string[] dialouge = { "테스트 다이얼로그1", "테스트 다이얼로그2", "테스트 다이얼로그3", "테스트 끝" };
        // Diaglogue.Instance.StartDialogue(dialouge);
        // -------------------------------------------------------------
    }

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
        OpenGameUI(pauseMenu);
        isUIEnabled = true;
        isGamePaused = true;
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        CloseGameUI(pauseMenu);
        isUIEnabled = false;
        isGamePaused = false;
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
                switch (ui.tag)
                {
                    case "PauseMenu":
                        UISound.GetComponent<AudioSource>().clip = pauseSound;
                        UISound.GetComponent<AudioSource>().Play();
                        break;
                    case "MemoUI":
                        UISound.GetComponent<AudioSource>().clip = memoOpenSound;
                        UISound.GetComponent<AudioSource>().Play();
                        break;
                    case "PhoneUI":
                        UISound.GetComponent<AudioSource>().clip = ClickSound;
                        UISound.GetComponent<AudioSource>().Play();
                        PhoneUI.Instance.OpenPhoneUI();
                        break;
                }
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
                switch (ui.tag)
                {
                    case "PauseMenu":
                        UISound.GetComponent<AudioSource>().clip = resumeSound;
                        UISound.GetComponent<AudioSource>().Play();
                        ui.SetActive(false);
                        break;
                    case "MemoUI":
                        UISound.GetComponent<AudioSource>().clip = memoCloseSound;
                        UISound.GetComponent<AudioSource>().Play();
                        ui.SetActive(false);
                        break;
                    case "PhoneUI":
                        UISound.GetComponent<AudioSource>().clip = ClickSound;
                        UISound.GetComponent<AudioSource>().Play();
                        PhoneUI.Instance.ClosePhoneUI();
                        break;
                }
                isUIEnabled = false;
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
                CloseGameUI(GameObject.FindGameObjectWithTag(ui));
            }
        }
    }

    public void PlayButtonHoverSound()
    {
        UISound.GetComponent<AudioSource>().clip = hoverSound;
        UISound.GetComponent<AudioSource>().Play();
    }

    public void PlayerFlashLightOnSound()
    {
        gameAudio.clip = flashLightOnSound;
        gameAudio.Play();
    }

    public void PlayTypingSound()
    {
        UISound.GetComponent<AudioSource>().clip = typingSound;
        UISound.GetComponent<AudioSource>().Play();
    }

    public void PlayClickSound()
    {
        if (isUIEnabled)
        {
            gameAudio.clip = ClickSound;
            gameAudio.Play();
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}