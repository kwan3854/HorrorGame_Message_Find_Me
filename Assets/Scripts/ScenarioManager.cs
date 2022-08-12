using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class ScenarioManager : MonoBehaviour
{
    [SerializeField] private GameObject phoneUI;
    [SerializeField] private GameObject memoUI;
    [SerializeField] private GameObject dialougeUI;

    [Header("Cut Scenes")]
    [SerializeField] private PlayableDirector timeline_lookUnderTable;
    [SerializeField] private PlayableDirector timeline_lookSpeaker;
    private static ScenarioManager instance = null;


    public int floorNumber = 0;
    public int gamePhase = 0;

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

    public static ScenarioManager Instance
    {
        get
        {
            return instance;
        }
    }


    // ========== Test Code ========== //
    void OnEnable()
    {
        StoryProceed(0, false);

        timeline_lookUnderTable.gameObject.SetActive(false);
    }
    // ========== Test Code ========== //

    public IEnumerator SendDialogue_Coroutine(string[] dialougeScript, float delaySec)
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

    private IEnumerator SendPhoneMemo_Coroutine(string message, float delaySec, bool isOpenPhoneUI)
    {
        yield return new WaitForSeconds(delaySec);
        if (isOpenPhoneUI)
        {
            GameManager.Instance.OpenGameUI(phoneUI);
        }
        PhoneUI.Instance.PhoneMemo(message);
    }

    public void TimeLinePlay(PlayableDirector timeline)
    {
        // GameManager.Instance.CloseGameUI(phoneUI);
        timeline.gameObject.SetActive(true);
        timeline.Play();
    }

    private IEnumerator WaitForDialougeEnd(int phase)
    {
        yield return new WaitForSeconds(0.5f);
        yield return new WaitUntil(() => !Diaglogue.Instance.isRunning);
        // Debug.Log("WaitForDialougeEnd");
        // while (Diaglogue.Instance.isRunning)
        // {
        //     // blank
        // }
        // Debug.Log("Dialouge End");
        StoryProceed(phase, false);

    }

    private IEnumerator WaitForSecond(float sec)
    {
        yield return new WaitForSeconds(sec);
    }

    public void StoryProceed(int index, bool isRegame)
    {
        gamePhase = index;
        switch (index)
        {
            case 0:
                if (isRegame)
                {
                    // 플레이어 포지션, 맵 세팅 초기화
                }
                StartCoroutine(SendMessage_Coroutine("책상 서랍에 둔 물건을 빼앗기고 싶지 않으면 오늘 11시에 학교로 찾아와라.", 2f, true));
                StartCoroutine(SendDialogue_Coroutine(new string[]
                { "누가 이런 장난을 쳤는지는 모르겠지만...",
                "일단, 책상 서랍에서 물건을 찾고 돌아가자."}
                , 5f));
                StartCoroutine(SendPhoneMemo_Coroutine("내 책상은 왼쪽에서 4번째 줄, 앞에서 4번째 자리다.", 2f, false));
                break;

            case 1:
                if (isRegame)
                {
                    // 플레이어 포지션, 맵 세팅 초기화
                }
                TimeLinePlay(timeline_lookUnderTable);
                StartCoroutine(SendDialogue_Coroutine(new string[]
                { "여기 분명 뒀는데?",
                "왜 없지?"}
                , 0f));
                // wait for dialogue to finish
                StartCoroutine(WaitForDialougeEnd(2));
                break;
            case 2:
                if (isRegame)
                {
                    // 플레이어 포지션, 맵 세팅 초기화
                }
                GameManager.Instance.PlayRadioNoiseSound();
                TimeLinePlay(timeline_lookSpeaker);
                StartCoroutine(SendDialogue_Coroutine(new string[]
                { "안녕, 시간 맞춰 학교에 왔구나.",
                "와 줘서 고마운걸.",
                "이제부터 내가 하는 말을 들어주길 바라.",
                "뒤에 있는 사물함에서 쪽지를 찾고 거기 쓰여있는 대로 행동하면",
                "네가 찾던 물건을 찾고 무사히 집으로 돌아갈 수 있을거야.",
                "그럼, 행운이 있길-."}
                , 1f));
                StartCoroutine(WaitForDialougeEnd(3));
                break;

            case 3:
                if (isRegame)
                {
                    // 플레이어 포지션, 맵 세팅 초기화
                }
                StartCoroutine(SendDialogue_Coroutine(new string[]
                { "뭐지? 누가 이런 장난을 치는 거야?",
                "찾던 물건은 왜 없는거지?",
                "하... 그냥 집에 갈까? 피곤하기도 하고...",
                "'그 물건' 을 찾으려면 쪽지를 찾아야 한다잖아.",
                "어떡하지?"}
                , 1f));
                StartCoroutine(SendPhoneMemo_Coroutine("기분나쁜 장난이다. 교실 뒤에 있는 사물함에서 쪽지를 찾아볼까? 아니면 그냥 집으로 돌아갈까?", 1f, false));
                break;

            case 4:
                StartCoroutine(SendDialogue_Coroutine(new string[]
                { "왜 이런 일을 나한테 시키는 걸까?"}
                , 0.5f));
                break;

            case 5:
                StartCoroutine(SendDialogue_Coroutine(new string[]
                { "왜 이런 일을 나에게 시키는 걸까?"}
                , 1f));
                break;
        }
    }
}
