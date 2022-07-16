using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Memo : MonoBehaviour
{
    private GameObject memoUI;
    private InputManager inputManager;
    [SerializeField] private string memoText = "Place Holder Text";

    private void Start()
    {
        memoUI = GameObject.Find("MemoUI");
        memoUI.gameObject.SetActive(false);
        inputManager = InputManager.Instance;
    }
    public void Read()
    {
        memoUI.gameObject.SetActive(true);
        Debug.Log(memoText);
        GameObject.Find("MemoText").GetComponent<TextMeshProUGUI>().text = memoText;
    }

    public void Close()
    {
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
