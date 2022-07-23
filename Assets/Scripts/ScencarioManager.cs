using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScencarioManager : MonoBehaviour
{
    [SerializeField] private GameObject phoneUI;
    [SerializeField] private GameObject memoUI;
    [SerializeField] private GameObject dialougeUI;
    private static ScencarioManager instance = null;
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
    }

    public static ScencarioManager Instance
    {
        get
        {
            return instance;
        }
    }

    void Start()
    {

        StartCoroutine(SendDialogue_Coroutine());
    }

    // ========== Test Code ========== //
    private IEnumerator SendDialogue_Coroutine()
    {
        yield return new WaitForSeconds(2f);
        GameManager.Instance.OpenGameUI(phoneUI);
        PhoneUI.Instance.MessageIncome("테스트 메시지입니다.");
        yield return new WaitForSeconds(2f);
        GameManager.Instance.OpenGameUI(dialougeUI);
        Diaglogue.Instance.StartDialogue(new string[] { "누가 이런 장난을 쳤는지는 모르겠지만…", "일단, 책상 서랍에서 물건을 찾고 돌아가자." });
    }
    // ========== Test Code ========== //
}
