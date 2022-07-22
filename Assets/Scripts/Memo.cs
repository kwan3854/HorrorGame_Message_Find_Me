using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Memo : MonoBehaviour
{
    private GameObject memoUI;
    // private InputManager inputManager;
    [Header("Memo Text")]
    [SerializeField] private string memoString = "Place Holder Text";

    [Header("Sound Effect Configs")]
    [SerializeField] private AudioClip openSound;
    [SerializeField] private AudioClip closeSound;
    private AudioSource audioSource;
    private TextMeshProUGUI memoText;

    private void Awake()
    {
        memoUI = GameObject.FindGameObjectWithTag("MemoUI");

        // inputManager = InputManager.Instance;
        audioSource = GameObject.Find("UISoundEffect").GetComponent<AudioSource>();
        memoText = GameObject.Find("MemoText").GetComponent<TextMeshProUGUI>();

        Debug.Assert(memoUI != null, "Memo UI not found");
        // Debug.Assert(inputManager != null, "Input Manager not found");
        Debug.Assert(audioSource != null, "Audio Source not found");
        Debug.Assert(memoText != null, "Memo Text not found");

        Debug.Assert(openSound != null, "Open Sound not found");
        Debug.Assert(closeSound != null, "Close Sound not found");
    }
    public void Read()
    {
        GameManager.Instance.OpenGameUI(memoUI);

        audioSource.clip = openSound;
        audioSource.Play();

        memoText.text = memoString;
    }

    public void Close()
    {
        audioSource.clip = closeSound;
        audioSource.Play();

        GameManager.Instance.CloseGameUI(memoUI);
    }

}
