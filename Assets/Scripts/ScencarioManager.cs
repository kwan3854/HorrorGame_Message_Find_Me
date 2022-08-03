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
        }
    }

    public static ScencarioManager Instance
    {
        get
        {
            return instance;
        }
    }


    // ========== Test Code ========== //
    void OnEnable()
    {
        StoryProceed(0);
    }
    // ========== Test Code ========== //

    private IEnumerator SendDialogue_Coroutine(string[] dialougeScript, float delaySec)
    {
        yield return new WaitForSeconds(delaySec);
        GameManager.Instance.OpenGameUI(dialougeUI);
        Diaglogue.Instance.StartDialogue(dialougeScript);
    }

    private IEnumerator SendMessage_Coroutine(string message, float delaySec, bool isOpenPhoneUI)
    {
        yield return new WaitForSeconds(delaySec);
        if (isOpenPhoneUI)
        {
            GameManager.Instance.OpenGameUI(phoneUI);
        }
        PhoneUI.Instance.MessageIncome(message);
    }

    public void StoryProceed(int index)
    {
        switch (index)
        {
            case 0:
                StartCoroutine(SendMessage_Coroutine("책상 서랍에 둔 물건을 빼앗기고 싶지 않으면 오늘 11시에 학교로 찾아와라.", 2f, true));
                StartCoroutine(SendDialogue_Coroutine(new string[]
                { "누가 이런 장난을 쳤는지는 모르겠지만...",
                "일단, 책상 서랍에서 물건을 찾고 돌아가자."}
                , 5f));
                break;

            case 1:
                StartCoroutine(SendDialogue_Coroutine(new string[]
                { "여기 분명 뒀는데?",
                "왜 없지?"}
                , 1f));
                break;
            case 2:
                StartCoroutine(SendDialogue_Coroutine(new string[]
                { "안녕, 시간 맞춰 학교에 왔구나.",
                "와 줘서 고마운걸.",
                "이제부터 내가 하는 말을 들어주길 바라.",
                "뒤에 있는 사물함에서 쪽지를 찾고 거기 쓰여있는 대로 행동하면",
                "네가 찾던 물건을 찾고 무사히 집으로 돌아갈 수 있을거야.",
                "그럼, 행운이 있길-."}
                , 1f));
                break;

            case 3:
                StartCoroutine(SendDialogue_Coroutine(new string[]
                { "뭐지? 누가 이런 장난을 치는 거야?",
                "찾던 물건은 왜 없는거지?",
                "하... 그냥 집에 갈까? 피곤하기도 하고...",
                "'그 물건' 을 찾으려면 쪽지를 찾아야 한다잖아.",
                "어떡하지?"}
                , 1f));
                break;

            case 4:
                // StartCoroutine(SelectionWindow_Coroutine(new string[] { "방송을 따라 쪽지를 찾는다.", "방송을 무시하고 집에 간다."});
                break;

            case 5:
                StartCoroutine(SendDialogue_Coroutine(new string[]
                { "왜 이런 일을 나에게 시키는 걸까?"}
                , 1f));
                break;
        }
    }
}
