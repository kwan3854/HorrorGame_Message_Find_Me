using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Memo : MonoBehaviour
{
    private GameObject memoUI;
    private InputManager inputManager;
    [Header("Memo Text")]
    [SerializeField] private string memoText = "Place Holder Text";

    [Header("Sound Effect Configs")]
    [SerializeField] private AudioClip OpenSound;
    [SerializeField] private AudioClip CloseSound;
    private AudioSource audioSource;

    private void Start()
    {
        memoUI = GameObject.Find("MemoUI");
        memoUI.gameObject.SetActive(false);

        inputManager = InputManager.Instance;
        audioSource = GameObject.Find("UISoundEffect").GetComponent<AudioSource>();
    }
    public void Read()
    {
        memoUI.gameObject.SetActive(true);

        audioSource.clip = OpenSound;
        audioSource.Play();

        GameObject.Find("MemoText").GetComponent<TextMeshProUGUI>().text = memoText;
    }

    public void Close()
    {
        audioSource.clip = CloseSound;
        audioSource.Play();

        memoUI.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (inputManager.PlayerQuit())
        {
            Close();
        }
    }
}
