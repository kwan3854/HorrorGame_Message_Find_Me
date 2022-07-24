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

    void OnEnable()
    {

        StartCoroutine(SendDialogue_Coroutine());
    }

    // ========== Test Code ========== //
    private IEnumerator SendDialogue_Coroutine()
    {
        yield return new WaitForSeconds(2f);
        GameManager.Instance.OpenGameUI(phoneUI);
        PhoneUI.Instance.MessageIncome("데모 테스트 메시지입니다.");
        yield return new WaitForSeconds(2f);
        GameManager.Instance.OpenGameUI(dialougeUI);
        Diaglogue.Instance.StartDialogue(new string[] { "데모 게임입니다. 데모용 기본 튜토리얼 대화창입니다. 화면을 마우스 클릭하여 다음 대화창으로 넘어갑니다.", "w,a,s,d 키로 이동 합니다.","f키로 손전등을 켜고 끌 수 있습니다.", "e키로 물체와 상호작용 할 수 있습니다." ,"R 키로 휴대전화를 꺼내거나 넣을 수 있습니다.", "ESC 키로 뒤로가거나 게임을 일시정지 할 수 있습니다.", "이제 안내를 마칩니다." });
    }
    // ========== Test Code ========== //
}
