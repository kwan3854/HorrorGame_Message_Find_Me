using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class GameManager : MonoBehaviour
{
    private static GameManager instance = null;
    private bool isUIEnabled = false;
    private bool isGamePaused = false;


    private List<string> gameUIs = new List<string>() { "PauseMenu", "MemoUI", "PhoneUI", "DialougeUI", "GameOverMenu", "FinalMessage" };

    [SerializeField] private GameObject player;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject gameOverMenu;
    [SerializeField] private GameObject memoUI;

    [Header("Audio Configs")]
    [SerializeField] private AudioSource UISound;
    [SerializeField] private AudioSource PhoneSound;
    [SerializeField] private AudioSource ClockSound;
    [SerializeField] private AudioSource PlayerSound;
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
    [SerializeField] private AudioClip screamSound;
    [SerializeField] private AudioClip dropSound;
    [SerializeField] private AudioClip fireOnSound;



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
        // CloseAllGameUI();
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
                case "FinalMessage":
                    // Intentionally left blank
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
        //CheckUIEnabled();

        if (ui.activeSelf)
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
                        Debug.Log("Game OverClose");
                        ui.SetActive(false);
                        break;
                    case "FinalMessage":
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

    public void PlayScreamSound()
    {
        PlayerSound.clip = screamSound;
        PlayerSound.Play();
    }

    public void PlayDropSound()
    {
        PlayerSound.clip = dropSound;
        PlayerSound.Play();
    }

    public void PlayFireOnSound()
    {
        PlayerSound.clip = fireOnSound;
        PlayerSound.Play();
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

    public void CheckPointRestart()
    {
        int currentGamePhase = ScenarioManager.Instance.gamePhase;
        player.SetActive(true);
        IsGamePaused = false;
        Time.timeScale = 1;
        CloseAllGameUI();
        ScenarioManager.Instance.StoryProceed(currentGamePhase, true);
    }

    public void GameOver(int gameOverType)
    {
        // GameManager.Instance.CloseAllGameUI();
        // 귀신에게 죽는 씬 만들어서 넣기
        gameAudio.clip = gameoverSound;
        gameAudio.Play();
        IsGamePaused = true;
        Time.timeScale = 0;
        switch (gameOverType)
        {
            case 1: // Time Up
                OpenGameUI(gameOverMenu);
                ScenarioManager.Instance.StartSendDialogue_Coroutine(new string[]
                { "머릿속에서 울리던 시계소리가 멈췄다.",
                "몸에 힘이 빠져나간다.",
                "아... 너무 늦어버렸나..."});
                break;
            case 2: // GhostKill
                PlayScreamSound();
                OpenGameUI(gameOverMenu);
                ScenarioManager.Instance.StartSendDialogue_Coroutine(new string[]
                { "연기 덩어리가 나를 덮쳤다",
                "몸에 힘이 빠져나간다.",
                "눈 앞이 캄캄해져 아무것도 보이지 않는다,",});
                break;
            case 3: // Scenario Out
                OpenGameUI(gameOverMenu);
                ScenarioManager.Instance.StartSendDialogue_Coroutine(new string[]
                { "갑자기 몸에 힘이 빠져나간다.",
                "눈 앞이 캄캄해져 아무것도 보이지 않는다,",
                "역시 요구를 따랐어야 했던걸까..."});
                break;
            case 4: // 호기심
                OpenGameUI(gameOverMenu);
                ScenarioManager.Instance.StartSendDialogue_Coroutine(new string[]
                { "괜한 호기심이었나",
                    "갑자기 몸에 힘이 빠져나간다.",
                "눈 앞이 캄캄해져 아무것도 보이지 않는다,"});
                break;
            case 5: // 성냥 켜지 않음
                OpenGameUI(gameOverMenu);
                ScenarioManager.Instance.StartSendDialogue_Coroutine(new string[]
                { "어두운 교실안으로 연기들이 들이닥쳤다",
                    "어디로도 도망갈 수 없었다"});
                break;
            case 6:
                OpenGameUI(gameOverMenu);
                ScenarioManager.Instance.StartSendDialogue_Coroutine(new string[]
                { "아무리 달려도 교실은 끝없이 반복되었다.",
                    "나는 영원히 반복되는 이 복도에 갇혀버렸다."});
                break;
        }
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

    private void Update()
    {
        CheckUIEnabled();
    }
}