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

        switch (memoNumber)
        {
            case 1:
                ScenarioManager.Instance.StartCheckIfClassOut();
                Debug.Log("Memo 1");
                break;

            case 2:
                ScenarioManager.Instance.StoryProceed(6, false);
                Debug.Log("Memo 2");
                break;

            case 3:
                // ScenarioManager.Instance.StoryProceed(11, false);
                Debug.Log("Memo 3");
                break;

            case 4:
                ScenarioManager.Instance.StoryProceed(11, false);
                Debug.Log("Memo 4");
                break;

            case 5:
                ScenarioManager.Instance.StoryProceed(12, false);
                Debug.Log("Memo 5");
                break;

            case 6:
                ScenarioManager.Instance.StoryProceed(15, false);
                Debug.Log("Memo 6");
                break;

            case 7:
                // 쪽지8 생성
                GameObject.Find("Floor_10(Clone)").transform.Find("Memo_8_Parent").gameObject.SetActive(true);
                Debug.Log("Memo 7");
                break;

            case 8:
                ScenarioManager.Instance.StoryProceed(19, false);
                Debug.Log("Memo 8");
                break;

            case 9:
                ScenarioManager.Instance.StoryProceed(20, false);
                Debug.Log("Memo 9");
                break;
            default:
                break;
        }

        gameObject.SetActive(false);
    }
}
