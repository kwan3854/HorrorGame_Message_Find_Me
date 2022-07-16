using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Memo : MonoBehaviour
{
    private GameObject memoUI;
    private InputManager inputManager;
    [Header("Memo Text")]
    [SerializeField] private string memoString = "Place Holder Text";

    [Header("Sound Effect Configs")]
    [SerializeField] private AudioClip openSound;
    [SerializeField] private AudioClip closeSound;
    private AudioSource audioSource;
    private TextMeshProUGUI memoText;

    private void Awake()
    {
        memoUI = GameObject.Find("MemoUI");

        inputManager = InputManager.Instance;
        audioSource = GameObject.Find("UISoundEffect").GetComponent<AudioSource>();
        memoText = GameObject.Find("MemoText").GetComponent<TextMeshProUGUI>();
    }
    void Start()
    {
        memoUI.gameObject.SetActive(false);
    }
    public void Read()
    {
        memoUI.gameObject.SetActive(true);

        audioSource.clip = openSound;
        audioSource.Play();

        memoText.text = memoString;
    }

    public void Close()
    {
        audioSource.clip = closeSound;
        audioSource.Play();

        memoUI.gameObject.SetActive(false);
    }

}
