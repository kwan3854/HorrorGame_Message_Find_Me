using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Memo : MonoBehaviour
{
    [Header("Memo Text")]
    [SerializeField] private string memoString = "Place Holder Text";

    public int memoNumber = 0;

    private GameObject memoUI;
    private TextMeshProUGUI memoText;

    private void Awake()
    {
        memoUI = GameObject.Find("InGameMenu").transform.Find("MemoUI").gameObject;
        memoText = memoUI.GetComponentInChildren<TextMeshProUGUI>();

        Debug.Assert(memoText != null, "Memo Text not found");
        Debug.Assert(memoUI != null, "Memo UI not found");
        Debug.Assert(memoText != null, "Memo Text not found");
    }
    public void Read()
    {
        GameManager.Instance.OpenGameUI(memoUI);
        memoString = memoString.Replace("\\n", "\n");
        memoText.text = memoString;
        if (memoNumber == 1)
        {
            GameManager.Instance.PlayClockTickSound();
            GameManager.Instance.StartCheckIfClassOut();
        }
        gameObject.SetActive(false);
    }
}
