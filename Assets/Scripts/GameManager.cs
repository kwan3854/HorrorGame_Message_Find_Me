using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class GameManager : MonoBehaviour
{
    private static GameManager instance = null;
    private bool isUIEnabled = false;
    private bool isGamePaused = false;

    private List<string> gameUIs = new List<string>() { "PauseMenu", "MemoUI", "PhoneUI", "DialougeUI", "GameOverMenu" };

    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject gameOverMenu;
    [SerializeField] private GameObject memoUI;

    [Header("Audio Configs")]
    [SerializeField] private AudioSource UISound;
    [SerializeField] private AudioSource PhoneSound;
    [SerializeField] private AudioSource ClockSound;
    [SerializeField] private AudioClip ClickSound;
    [SerializeField] private AudioClip pauseSound;
    [SerializeField] private AudioClip resumeSound;
    [SerializeField] private AudioClip hoverSound;
    [SerializeField] private AudioClip memoOpenSound;
    [SerializeField] private AudioClip memoCloseSound;
    [SerializeField] private AudioClip typingSound;
    [SerializeField] private AudioClip phoneOpenSound;
    [SerializeField] private AudioClip phoneCloseSound;
    [SerializeField] private AudioClip phoneVibrateSound;
    [SerializeField] private AudioClip phoneBeepSound;
    [SerializeField] private AudioClip radioNoiseSound;
    [SerializeField] private AudioClip gameoverSound;



    [Header("Game Play Audio Configs")]
    [SerializeField] private AudioSource gameAudio;
    [SerializeField] private AudioClip flashLightOnSound;
    [SerializeField] private AudioClip clockTickSound;

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
        // ====== Test Code ====== //

        // ====== Test Code ====== //
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
        // isUIEnabled = false;
        isGamePaused = false;
        Time.timeScale = 1f;
    }

    public void OpenGameUI(GameObject ui)
    {
        Debug.Log(ui + " is already enabled");
        if (gameUIs.Contains(ui.tag))
        {
            ui.SetActive(true);
            switch (ui.tag)
            {
                case "PauseMenu":
                    UISound.clip = pauseSound;
                    UISound.Play();
                    break;
                case "MemoUI":
                    UISound.clip = memoOpenSound;
                    UISound.Play();
                    break;
                case "PhoneUI":
                    UISound.clip = phoneOpenSound;
                    UISound.Play();
                    PhoneUI.Instance.OpenPhoneUI();
                    break;
                case "DialougeUI":
                    UISound.clip = ClickSound;
                    UISound.Play();
                    break;
                case "GameOverMenu":
                    UISound.clip = gameoverSound;
                    UISound.Play();
                    break;
            }
        }
        else
        {
            Debug.Assert(false, ui + " is not tagged as GameUI");
        }
    }

    public void CloseGameUI(GameObject ui)
    {
        if (isUIEnabled)
        {
            if (gameUIs.Contains(ui.tag))
            {
                isUIEnabled = false;
                Cursor.visible = false;
                // Cursor.lockState = CursorLockMode.Locked;
                switch (ui.tag)
                {
                    case "PauseMenu":
                        UISound.clip = resumeSound;
                        UISound.Play();
                        ui.SetActive(false);
                        break;
                    case "MemoUI":
                        UISound.clip = memoCloseSound;
                        UISound.Play();
                        ui.SetActive(false);
                        break;
                    case "PhoneUI":
                        UISound.clip = phoneCloseSound;
                        UISound.Play();
                        PhoneUI.Instance.ClosePhoneUI();
                        break;
                    case "DialougeUI":
                        UISound.clip = ClickSound;
                        UISound.Play();
                        break;
                    case "GameOverMenu":
                        ui.SetActive(false);
                        break;
                }
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
        UISound.clip = hoverSound;
        UISound.Play();
    }

    public void PlayerFlashLightOnSound()
    {
        gameAudio.clip = flashLightOnSound;
        gameAudio.Play();
    }

    public void PlayTypingSound()
    {
        UISound.clip = typingSound;
        UISound.Play();
    }

    public void PlayClickSound()
    {
        if (isUIEnabled)
        {
            UISound.clip = ClickSound;
            UISound.Play();
        }
    }

    public void PlayPhoneVibrateSound()
    {
        PhoneSound.clip = phoneVibrateSound;
        PhoneSound.Play();
    }

    public void PlayPhoneBeepSound()
    {
        PhoneSound.clip = phoneBeepSound;
        PhoneSound.Play();
    }

    public void PlayRadioNoiseSound()
    {
        gameAudio.clip = radioNoiseSound;
        gameAudio.Play();
    }

    public void PlayClockTickSound()
    {
        ClockSound.clip = clockTickSound;
        ClockSound.Play();
    }

    public void StopGameAudio()
    {
        gameAudio.Stop();
        ClockSound.Stop();
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void GameOver()
    {
        IsGamePaused = true;
        Time.timeScale = 0;
        OpenGameUI(gameOverMenu);
    }

    public void RestartGameFromSavePoint()
    {
        IsGamePaused = false;
        Time.timeScale = 1;
        CloseAllGameUI();
    }


    private void CheckUIEnabled()
    {
        foreach (string gameUI in gameUIs)
        {
            GameObject gameUIObject = GameObject.FindGameObjectWithTag(gameUI);
            if (gameUIObject != null)
            {
                isUIEnabled = true;
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }
        }
    }

    private IEnumerator CheckIfClassOut()
    {
        Debug.Log("Checking if class out");
        bool isClassOut = false;
        float time = 0;
        // check if class is out for 15 seconds

        while (time < 15.0f)
        {
            time += Time.deltaTime;
            Debug.Log(time);
            if (GameObject.Find("Player").transform.position.x < -60.0f)
            {
                Debug.Log("Player is out");
                GameManager.Instance.StopGameAudio();
                isClassOut = true;
                break;
            }
            Debug.Log(time);
            yield return null;
        }

        // ----- Game Over ------ //
        if (!isClassOut)
        {
            Debug.Log("GAME_OVER: Player is not out in time");
            GameManager.Instance.GameOver();
        }
    }

    public void StartCheckIfClassOut()
    {
        StartCoroutine(CheckIfClassOut());
    }

    private void Update()
    {
        CheckUIEnabled();
    }
}