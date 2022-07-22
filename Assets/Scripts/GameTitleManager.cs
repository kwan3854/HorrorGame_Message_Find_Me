using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class GameTitleManager : MonoBehaviour
{
    private static GameTitleManager instance = null;

    // ----------Use this for Intro TimeLine ----------------------
    [Header("Intro TimeLine")]
    [SerializeField] private PlayableDirector introTimeline;
    // -------------------------------------------------------------

    [Header("Audio Configs")]
    [SerializeField] private AudioSource UISound;
    [SerializeField] private AudioClip StartSound;
    [SerializeField] private AudioClip ClickSound;
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
        }

        Time.timeScale = 1f;

        introTimeline.Play();

        Cursor.visible = true;
    }

    public static GameTitleManager Instance
    {
        get
        {
            return instance;
        }
    }

    public void PlayButtonHoverSound()
    {
        UISound.clip = HoverSound;
        UISound.Play();
    }

    public void PlayButtonClickSound()
    {
        UISound.clip = ClickSound;
        UISound.Play();
    }

    public void PlayButtonStartSound()
    {
        UISound.clip = StartSound;
        UISound.Play();
    }

    public void OnPlayButtonClicked()
    {
        PlayButtonStartSound();
        UnityEngine.SceneManagement.SceneManager.LoadScene("Main");
    }

    public void OnQuitButtonClicked()
    {
        PlayButtonClickSound();
        Application.Quit();
    }

    public void OnCreditsButtonClicked()
    {
        PlayButtonClickSound();
        // UnityEngine.SceneManagement.SceneManager.LoadScene("Credits");
    }

    public void OnInstructionsButtonClicked()
    {
        PlayButtonClickSound();
        // UnityEngine.SceneManagement.SceneManager.LoadScene("Instructions");
    }
}
