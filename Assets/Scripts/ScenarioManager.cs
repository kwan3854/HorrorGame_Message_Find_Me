using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using TMPro;
using UnityEngine.Video;

public class ScenarioManager : MonoBehaviour
{
    [SerializeField] private GameObject phoneUI;
    [SerializeField] private GameObject memoUI;
    [SerializeField] private GameObject dialougeUI;
    [SerializeField] private Transform player;

    [Header("Cut Scenes")]
    [SerializeField] private PlayableDirector timeline_lookUnderTable;
    [SerializeField] private PlayableDirector timeline_lookSpeaker;
    [SerializeField] private PlayableDirector timeline_memoFind_4;
    [SerializeField] private PlayableDirector timeline_blackBoard;
    [SerializeField] private VideoPlayer videoPlayer;

    [Header("Ghosts")]
    [SerializeField] private GameObject ghost_1;
    [SerializeField] private GameObject ghost_2;
    //[SerializeField] private GameObject ghost_3;
    //[SerializeField] private GameObject ghost_4;
    [SerializeField] private GameObject ghost_5;
    [SerializeField] private GameObject ghost_6;
    [SerializeField] private GameObject ghost_7;
    [SerializeField] private GameObject ghost_8;
    [SerializeField] private GameObject ghost_9;
    [SerializeField] private GameObject ghost_10;
    [SerializeField] private GameObject ghost_11;
    //[SerializeField] private GameObject ghost_12;
    //[SerializeField] private GameObject ghost_13;

    [Header("Text")]
    [SerializeField] private TextMeshPro blackBoardText;

    [Header("UI")]
    [SerializeField] private GameObject MainCamera;
    [SerializeField] private GameObject finalMessage;
    [SerializeField] private GameObject blackScreen;
    [SerializeField] private GameObject videoRawImage;


    // [Header("Interactable Objects")]
    // [SerializeField] private GameObject desk;
    private static ScenarioManager instance = null;
    private bool isAllCandleOn;
    public bool isCandle = false;



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
        timeline_lookUnderTable.gameObject.SetActive(false);
        blackBoardText.gameObject.SetActive(false);
        //videoPlayer.gameObject.SetActive(false);
        videoRawImage.gameObject.SetActive(false);

        StoryProceed(0, false);
    }
    // ========== Test Code ========== //

    private void GhostController()
    {
        switch (floorNumber)
        {
            case 0:
                ghost_1.SetActive(true);
                ghost_2.SetActive(true);
                //ghost_3.SetActive(false);
                //ghost_4.SetActive(false);
                ghost_5.SetActive(false);
                ghost_6.SetActive(false);
                ghost_7.SetActive(false);
                ghost_8.SetActive(false);
                ghost_9.SetActive(false);
                ghost_10.SetActive(false);
                ghost_11.SetActive(false);
                //ghost_12.SetActive(false);
                //ghost_13.SetActive(false);
                break;
            case 1:
                ghost_1.SetActive(true);
                ghost_2.SetActive(false);
                //ghost_3.SetActive(false);
                //ghost_4.SetActive(false);
                ghost_5.SetActive(false);
                ghost_6.SetActive(false);
                ghost_7.SetActive(false);
                ghost_8.SetActive(false);
                ghost_9.SetActive(false);
                ghost_10.SetActive(false);
                ghost_11.SetActive(false);
                //ghost_12.SetActive(false);
                //ghost_13.SetActive(false);
                break;
            case 2:
                ghost_1.SetActive(false);
                ghost_2.SetActive(false);
                //ghost_3.SetActive(true);
                //ghost_4.SetActive(true);
                ghost_5.SetActive(true);
                ghost_6.SetActive(true);
                ghost_7.SetActive(true);
                ghost_8.SetActive(true);
                ghost_9.SetActive(true);
                ghost_10.SetActive(true);
                ghost_11.SetActive(true);
                //ghost_12.SetActive(true);
                //ghost_13.SetActive(true);
                break;
            case 3:
                ghost_1.SetActive(false);
                ghost_2.SetActive(false);
                //ghost_3.SetActive(true);
                //ghost_4.SetActive(true);
                ghost_5.SetActive(true);
                ghost_6.SetActive(true);
                ghost_7.SetActive(true);
                ghost_8.SetActive(true);
                ghost_9.SetActive(true);
                ghost_10.SetActive(true);
                ghost_11.SetActive(true);
                //ghost_12.SetActive(true);
                //ghost_13.SetActive(true);
                break;

            case 4:
                ghost_1.SetActive(false);
                ghost_2.SetActive(false);
                //ghost_3.SetActive(false);
                //ghost_4.SetActive(false);
                ghost_5.SetActive(false);
                ghost_6.SetActive(false);
                ghost_7.SetActive(false);
                ghost_8.SetActive(false);
                ghost_9.SetActive(false);
                ghost_10.SetActive(false);
                ghost_11.SetActive(false);
                //ghost_12.SetActive(false);
                //ghost_13.SetActive(false);
                break;

            case 5:
                ghost_1.SetActive(false);
                ghost_2.SetActive(false);
                //ghost_3.SetActive(false);
                //ghost_4.SetActive(false);
                ghost_5.SetActive(false);
                ghost_6.SetActive(false);
                ghost_7.SetActive(false);
                ghost_8.SetActive(false);
                ghost_9.SetActive(false);
                ghost_10.SetActive(false);
                ghost_11.SetActive(false);
                //ghost_12.SetActive(false);
                //ghost_13.SetActive(false);
                break;

            case 6:
                ghost_1.SetActive(false);
                ghost_2.SetActive(false);
                //ghost_3.SetActive(true);
                //ghost_4.SetActive(true);
                ghost_5.SetActive(true);
                ghost_6.SetActive(true);
                ghost_7.SetActive(true);
                ghost_8.SetActive(true);
                ghost_9.SetActive(true);
                ghost_10.SetActive(true);
                ghost_11.SetActive(true);
                //ghost_12.SetActive(true);
                //ghost_13.SetActive(true);
                break;

            case 7:
                ghost_1.SetActive(false);
                ghost_2.SetActive(false);
                //ghost_3.SetActive(true);
                //ghost_4.SetActive(true);
                ghost_5.SetActive(true);
                ghost_6.SetActive(true);
                ghost_7.SetActive(true);
                ghost_8.SetActive(true);
                ghost_9.SetActive(true);
                ghost_10.SetActive(true);
                ghost_11.SetActive(true);
                //ghost_12.SetActive(true);
                //ghost_13.SetActive(true);
                break;

            case 8:
                ghost_1.SetActive(false);
                ghost_2.SetActive(false);
                //ghost_3.SetActive(false);
                //ghost_4.SetActive(false);
                ghost_5.SetActive(false);
                ghost_6.SetActive(false);
                ghost_7.SetActive(false);
                ghost_8.SetActive(false);
                ghost_9.SetActive(false);
                ghost_10.SetActive(false);
                ghost_11.SetActive(false);
                //ghost_12.SetActive(false);
                //ghost_13.SetActive(false);
                break;

            case 9:
                ghost_1.SetActive(false);
                ghost_2.SetActive(false);
                //ghost_3.SetActive(true);
                //ghost_4.SetActive(true);
                ghost_5.SetActive(true);
                ghost_6.SetActive(true);
                ghost_7.SetActive(true);
                ghost_8.SetActive(true);
                ghost_9.SetActive(true);
                ghost_10.SetActive(true);
                ghost_11.SetActive(true);
                //ghost_12.SetActive(true);
                //ghost_13.SetActive(true);
                break;

            case 10:
                ghost_1.SetActive(false);
                ghost_2.SetActive(false);
                //ghost_3.SetActive(false);
                //ghost_4.SetActive(false);
                ghost_5.SetActive(false);
                ghost_6.SetActive(false);
                ghost_7.SetActive(false);
                ghost_8.SetActive(false);
                ghost_9.SetActive(false);
                ghost_10.SetActive(false);
                ghost_11.SetActive(false);
                //ghost_12.SetActive(false);
                //ghost_13.SetActive(false);
                break;
            case 11:
                ghost_1.SetActive(false);
                ghost_2.SetActive(false);
                //ghost_3.SetActive(false);
                //ghost_4.SetActive(false);
                ghost_5.SetActive(false);
                ghost_6.SetActive(false);
                ghost_7.SetActive(false);
                ghost_8.SetActive(false);
                ghost_9.SetActive(false);
                ghost_10.SetActive(false);
                ghost_11.SetActive(false);
                //ghost_12.SetActive(false);
                //ghost_13.SetActive(false);
                break;
            case 12:
                ghost_1.SetActive(false);
                ghost_2.SetActive(false);
                //ghost_3.SetActive(true);
                //ghost_4.SetActive(true);
                ghost_5.SetActive(true);
                ghost_6.SetActive(true);
                ghost_7.SetActive(true);
                ghost_8.SetActive(true);
                ghost_9.SetActive(true);
                ghost_10.SetActive(true);
                ghost_11.SetActive(true);
                //ghost_12.SetActive(true);
                //ghost_13.SetActive(true);
                break;
            default:
                ghost_1.SetActive(false);
                ghost_2.SetActive(false);
                //ghost_3.SetActive(false);
                //ghost_4.SetActive(false);
                ghost_5.SetActive(false);
                ghost_6.SetActive(false);
                ghost_7.SetActive(false);
                ghost_8.SetActive(false);
                ghost_9.SetActive(false);
                ghost_10.SetActive(false);
                ghost_11.SetActive(false);
                //ghost_12.SetActive(false);
                //ghost_13.SetActive(false);
                break;
        }
    }

    void Update()
    {
        GhostController();
        HallwayOverflowController();
    }

    private void HallwayOverflowController()
    {
        if (floorNumber >= 29)
        {
            GameManager.Instance.GameOver(6);
        }
    }


    public IEnumerator SendDialogue_Coroutine(string[] dialougeScript)
    {
        yield return null;
        GameManager.Instance.OpenGameUI(dialougeUI);
        Dialogue.Instance.StartDialogue(dialougeScript, false);
    }

    public IEnumerator SendDialogue_Coroutine(string[] dialougeScript, float delaySec)
    {
        yield return new WaitForSeconds(delaySec);
        GameManager.Instance.OpenGameUI(dialougeUI);
        Dialogue.Instance.StartDialogue(dialougeScript, false);
    }

    public IEnumerator SendDialogue_Coroutine(string[] dialougeScript, string name, float delaySec)
    {
        yield return new WaitForSeconds(delaySec);
        GameManager.Instance.OpenGameUI(dialougeUI);
        Dialogue.Instance.StartDialogue(dialougeScript, true);
        Dialogue.Instance.SetName(name);
    }

    public IEnumerator SendDialogue_Coroutine(string[] dialougeScript, string name)
    {
        yield return null;
        GameManager.Instance.OpenGameUI(dialougeUI);
        Dialogue.Instance.StartDialogue(dialougeScript, true);
        Dialogue.Instance.SetName(name);
    }

    public void StartSendDialogue_Coroutine(string[] dialougeScript)
    {
        StartCoroutine(SendDialogue_Coroutine(dialougeScript));
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

    public IEnumerator WaitForDialougeEnd(int phase)
    {
        yield return new WaitForSeconds(0.5f);
        yield return new WaitUntil(() => !Dialogue.Instance.isRunning);
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

    private IEnumerator CheckIfClassOut()
    {
        Debug.Log("Checking if class out");
        bool isClassOut = false;
        bool firstTime = true;
        float time = 0;
        // check if class is out for 15 seconds

        while (time < 25.0f)
        {
            time += Time.deltaTime;
            Debug.Log(time);
            if (firstTime == true && time > 15.0f)
            {
                GameManager.Instance.PlayClockTickSound();
                firstTime = false;
            }
            if (GameObject.Find("Player").transform.position.x < -60.0f)
            {
                Debug.Log("Player is out");
                GameManager.Instance.StopGameAudio();
                isClassOut = true;
                break;
            }
            Debug.Log(time);
            yield return null;
        }

        // ----- Game Over ------ //
        if (!isClassOut)
        {
            Debug.Log("GAME_OVER: Player is not out in time");
            GameManager.Instance.GameOver(1);
        }
        else
        {
            StoryProceed(5, false);
        }
    }

    private IEnumerator CheckIfClassOut_2()
    {
        Debug.Log("Checking if class out");
        bool isClassOut = false;
        // check if class is out for 15 seconds
        while (true)
        {
            if (GameObject.Find("Player").transform.position.x < -60.0f)
            {
                Debug.Log("Player is out");
                isClassOut = true;
                break;
            }
            yield return null;
        }
        // ----- Game Over ------ //
        if (!isClassOut)
        {
            // blank
        }
        else
        {
            StoryProceed(10, false);
        }
    }

    private IEnumerator CheckIfClassBackIn()
    {
        Debug.Log("Checking if 2-4 class back in");
        bool isClassIn = false;
        // check if class is out for 15 seconds
        while (!isCandle)
        {
            if (GameObject.Find("Player").transform.position.x > -58f
            && GameObject.Find("Player").transform.position.z < -55.0f
            && GameObject.Find("Player").transform.position.z > -155f)
            {
                Debug.Log("Player is in");
                yield return new WaitForSeconds(1.0f);
                if (GameObject.Find("Player").transform.position.x > -58f
                && GameObject.Find("Player").transform.position.z < -55.0f
                && GameObject.Find("Player").transform.position.z > -155f
                && floorNumber == 1)
                {
                    isClassIn = true;
                    break;
                }
                else
                {
                    continue;
                }
            }
            yield return null;
        }

        if (isClassIn)
        {
            GameManager.Instance.GameOver(4);
        }
    }

    private IEnumerator CheckIfSecondFloor()
    {
        Debug.Log("Checking if second floor");
        bool isSecondFloor = false;
        bool firstTime = true;
        float time = 0;

        // check if class is out for 15 seconds
        while (time < 60.0f)
        {
            time += Time.deltaTime;
            Debug.Log(time);

            if (firstTime == true && time > 45.0f)
            {
                GameManager.Instance.PlayClockTickSound();
                firstTime = false;
            }
            if (player.transform.position.y < 34f && player.transform.position.y > 31f && floorNumber == 1)
            {
                Debug.Log("Player is out");
                GameManager.Instance.StopGameAudio();
                isSecondFloor = true;
                break;
            }
            Debug.Log(time);
            yield return null;
        }
        // ----- Game Over ------ //
        if (!isSecondFloor)
        {
            Debug.Log("GAME_OVER: Player is not out in time");
            GameManager.Instance.GameOver(1);
        }
        else
        {
            StoryProceed(7, false);
        }
    }

    private IEnumerator CheckIf24Classroom()
    {
        Debug.Log("Checking if 2-4 class in");
        bool isClassIn = false;
        // check if class is out for 15 seconds
        while (!isCandle)
        {
            if (GameObject.Find("Player").transform.position.x > -58f
            && GameObject.Find("Player").transform.position.z < -55.0f
            && GameObject.Find("Player").transform.position.z > -155f
            && floorNumber == 1)
            {
                Debug.Log("Player is in");
                isClassIn = true;
                break;
            }
            yield return null;
        }

        if (isClassIn)
        {
            StoryProceed(8, false);
        }
    }

    private IEnumerator CheckIf24ClassroomAndFloor8()
    {
        Debug.Log("Checking if 2-4 class in");
        bool isClassIn = false;
        // check if class is out for 15 seconds
        while (!isClassIn)
        {
            if (GameObject.Find("Player").transform.position.x > -58f
            && GameObject.Find("Player").transform.position.z < -55.0f
            && GameObject.Find("Player").transform.position.z > -155f
            && floorNumber == 8)
            {
                Debug.Log("Player is in");
                isClassIn = true;
                break;
            }
            yield return null;
        }
        if (isClassIn)
        {
            StoryProceed(16, false);
        }
    }

    private IEnumerator CheckIf27Classroom()
    {
        Debug.Log("Checking if 2-7 class in");
        bool isClassIn = false;
        // check if class is out for 15 seconds
        while (!isCandle)
        {
            if (GameObject.Find("Player").transform.position.x > -58f
            && GameObject.Find("Player").transform.position.z < -480f
            && GameObject.Find("Player").transform.position.z > -600
            && floorNumber == 1)
            {
                Debug.Log("Player is in");
                yield return new WaitForSeconds(1.0f);
                if (GameObject.Find("Player").transform.position.x > -58f
                && GameObject.Find("Player").transform.position.z < -480f
                && GameObject.Find("Player").transform.position.z > -600
                && floorNumber == 1)
                {
                    isClassIn = true;
                    break;
                }
                else
                {
                    continue;
                }
            }
            yield return null;
        }
        // ----- Game Over ------ //
        if (isClassIn)
        {
            GameManager.Instance.GameOver(3);
        }
    }

    private IEnumerator CheckIfGoHome_Coroutine()
    {
        GameObject memo = GameObject.Find("Floor_2(Clone)").transform.Find("Memo_1").gameObject;
        Debug.Log(memo);
        Vector3 playerPastPosition = player.transform.localPosition;
        Vector3 playerPastPastPosition = player.transform.localPosition;
        while (memo.gameObject.activeSelf)
        {
            yield return null;
            if (floorNumber != 0)
            {
                //player.transform.localPosition = new Vector3(-42, 33, 190);
                //yield return new WaitForSeconds(0.1f);
                GameManager.Instance.GameOver(3);
                break;
            }
        }
    }

    private IEnumerator CheckAllCandleOn()
    {
        isAllCandleOn = false;

        while (!isAllCandleOn)
        {
            yield return new WaitForSeconds(0.1f);
            GameObject[] candles = GameObject.FindGameObjectsWithTag("Candle_Usable");
            if (candles.Length == 7)
            {
                isAllCandleOn = true;
                foreach (GameObject candle in candles)
                {
                    if (candle.GetComponent<Candle_Controller>().isLit == false)
                    {
                        isAllCandleOn = false;
                    }
                }
            }
        }

        if (isAllCandleOn)
        {
            StoryProceed(13, false);
        }
    }

    private IEnumerator WaitForSecAndProceed(int sec, int proceed)
    {
        yield return new WaitForSeconds(sec);
        StoryProceed(proceed, false);
    }

    private IEnumerator WaitForESCKeyPressedAndProceed(int proceed)
    {
        Debug.Log("Waiting for ESC key pressed");
        while (true)
        {
            yield return null;
            if (InputManager.Instance.PlayerQuit())
            {
                StoryProceed(proceed, false);
                break;
            }
        }

    }

    private IEnumerator CheckIf30SecOnCandle()
    {
        float time = 0;
        bool isOn = false;
        while (time < 30.0f && !isOn)
        {
            time += Time.deltaTime;
            yield return null;
            if (GameObject.Find("Floor_10(Clone)").transform.Find("Candle_Usable").GetComponent<Candle_Controller>().isLit)
            {
                StoryProceed(17, false);
                isOn = true;
                break;
            }
        }
        if (!isOn)
        {
            GameManager.Instance.GameOver(5);
        }

    }

    private IEnumerator CheckIfStairDownOrUp()
    {
        bool isMoved = false;
        while (!isMoved)
        {
            yield return null;
            if (floorNumber != 10)
            {
                StoryProceed(22, false);
                isMoved = true;
                break;
            }
        }
    }

    private IEnumerator CheckIfLeave21Classroom()
    {
        while (true)
        {
            yield return null;
            if (GameObject.Find("Player").transform.position.x < -60.0f)
            {
                StoryProceed(21, false);
                break;
            }
        }
    }

    private IEnumerator BlackScreenForSec(float sec)
    {
        blackScreen.SetActive(true);
        yield return new WaitForSeconds(sec);
        blackScreen.SetActive(false);
    }


    public void StartCheckIfClassOut()
    {
        StartCoroutine(CheckIfClassOut());
    }


    public void ResetAllGhosts()
    {
        Vector3 playerPosition = player.transform.position;
        player.transform.position = new Vector3(10000, 10000, 10000);
        foreach (GameObject ghost in GameObject.FindGameObjectsWithTag("Ghost"))
        {
            // ghost.GetComponent<AIController>().Reset();
            ghost.SetActive(false);
            ghost.SetActive(true);
        }
        player.transform.position = playerPosition;
    }

    private void SetPlayerPosition(Vector3 position)
    {
        player.gameObject.SetActive(false);
        player.transform.position = position;
        player.gameObject.SetActive(true);
    }

    public void StoryProceed(int index, bool isRegame)
    {
        gamePhase = index;
        switch (index)
        {
            case 0:
                StartCoroutine(CheckIfGoHome_Coroutine());
                if (isRegame)
                {
                    ResetAllGhosts();
                    HallWayController.Instance.setupHallwaysReset(0);
                    SetPlayerPosition(new Vector3(-42, 33, 190));

                    GameObject.Find("Floor_2(Clone)").transform.Find("Memo_1").gameObject.SetActive(true);
                }
                StartCoroutine(SendPhoneMemo_Coroutine("내 책상은 왼쪽에서 4번째 줄, 앞에서 4번째 자리다.", 2f, false));
                StartCoroutine(SendMessage_Coroutine("책상 서랍에 둔 물건을 빼앗기고 싶지 않으면 오늘 12시에 학교로 찾아와라.", 2f, true));
                StartCoroutine(SendDialogue_Coroutine(new string[]
                { "누가 이런 장난을 쳤는지는 모르겠지만...",
                "일단, 책상 서랍에서 물건을 찾고 돌아가자."},
                5f));
                break;

            case 1:
                if (isRegame)
                {
                    ResetAllGhosts();
                    HallWayController.Instance.setupHallwaysReset(0);
                    SetPlayerPosition(new Vector3(-42, 33, 190));
                    GameObject.Find("Floor_2(Clone)").transform.Find("Memo_1").gameObject.SetActive(true);
                }
                TimeLinePlay(timeline_lookUnderTable);
                StartCoroutine(SendDialogue_Coroutine(new string[]
                { "여기 분명 뒀는데?",
                "왜 없지?"}));
                // wait for dialogue to finish
                StartCoroutine(WaitForDialougeEnd(2));
                break;
            case 2:
                if (isRegame)
                {
                    ResetAllGhosts();
                    HallWayController.Instance.setupHallwaysReset(0);
                    SetPlayerPosition(new Vector3(-42, 33, 190));
                    GameObject.Find("Floor_2(Clone)").transform.Find("Memo_1").gameObject.SetActive(true);
                }
                // GameManager.Instance.PlayRadioNoiseSound();
                TimeLinePlay(timeline_lookSpeaker);
                StartCoroutine(SendDialogue_Coroutine(new string[]
                { "안녕, 시간 맞춰 학교에 왔구나.",
                "와 줘서 고마운걸.",
                "이제부터 내가 하는 말을 들어주길 바라.",
                "뒤에 있는 사물함에서 쪽지를 찾고 거기 쓰여있는 대로 행동하면",
                "네가 찾던 물건을 찾고 무사히 집으로 돌아갈 수 있을거야.",
                "그럼, 행운이 있길-."}
                , "의문의 목소리"));
                StartCoroutine(WaitForDialougeEnd(3));
                break;

            case 3:
                if (isRegame)
                {
                    ResetAllGhosts();
                    HallWayController.Instance.setupHallwaysReset(0);
                    SetPlayerPosition(new Vector3(-42, 33, 190));
                    GameObject.Find("Floor_2(Clone)").transform.Find("Memo_1").gameObject.SetActive(true);
                }
                StartCoroutine(SendDialogue_Coroutine(new string[]
                { "뭐지? 누가 이런 장난을 치는 거야?",
                "찾던 물건은 왜 없는거지?",
                "하... 그냥 집에 갈까? 피곤하기도 하고...",
                "'그 물건' 을 찾으려면 쪽지를 찾아야 한다잖아.",
                "어떡하지?"}));
                StartCoroutine(WaitForDialougeEnd(4));
                break;

            case 4:
                if (isRegame)
                {
                    ResetAllGhosts();
                    HallWayController.Instance.setupHallwaysReset(0);
                    SetPlayerPosition(new Vector3(-42, 33, 190));
                    GameObject.Find("Floor_2(Clone)").transform.Find("Memo_1").gameObject.SetActive(true);
                }
                StartCoroutine(SendPhoneMemo_Coroutine("기분나쁜 장난이다. 교실 뒤에 있는 사물함에서 쪽지를 찾아볼까? 아니면 그냥 집으로 돌아갈까?", 1f, true));
                break;

            case 5:
                // StartCoroutine(SendDialogue_Coroutine(new string[]
                // { "왜 이런 일을 나한테 시키는 걸까?"}
                // , 0.5f));
                if (isRegame)
                {
                    ResetAllGhosts();
                    HallWayController.Instance.setupHallwaysReset(0);
                    SetPlayerPosition(new Vector3(-62, 32.6f, 219));
                    GameObject.Find("Floor_2(Clone)").transform.Find("Memo_2").gameObject.SetActive(true);
                }
                if (GameObject.Find("Floor_2(Clone)").transform.Find("Memo_2").gameObject.activeSelf)
                {
                    TimeLinePlay(timeline_memoFind_4);
                }
                break;

            case 6:
                if (isRegame)
                {
                    ResetAllGhosts();
                    HallWayController.Instance.setupHallwaysReset(0);
                    SetPlayerPosition(new Vector3(-62, 32.6f, 219));
                }
                StartCoroutine(SendDialogue_Coroutine(new string[]
                { "'위층'의 2학년 4반? 분명 2학년 교실은 2층에만 있을텐데..."}
                , 1f));
                StartCoroutine(SendPhoneMemo_Coroutine("위층의 2학년 4반이 무슨 뜻일까? 위층으로 올라가보자", 1f, true));
                StartCoroutine(CheckIfSecondFloor());
                break;

            case 7:
                if (isRegame)
                {
                    ResetAllGhosts();
                    HallWayController.Instance.setupHallwaysReset(1);
                    SetPlayerPosition(new Vector3(-84.36806f, 32.53757f, -110.6176f));
                }
                StartCoroutine(SendDialogue_Coroutine(new string[]
                { "분명 한층 더 올라왔는데...",
                "또 2학년 교실들이 있어...",
                "일단 2학년 4반교실로 들어가 보자"}
                , 0f));
                StartCoroutine(CheckIf24Classroom());
                break;

            case 8:
                if (isRegame)
                {
                    ResetAllGhosts();
                    HallWayController.Instance.setupHallwaysReset(1);
                    SetPlayerPosition(new Vector3(-47.7f, 33.24f, -148));
                }
                StartCoroutine(SendDialogue_Coroutine(new string[]
                { "분명 이 교실안에 물건이 있을거야",
                "찾아보자."}
                , 0f));
                StartCoroutine(SendPhoneMemo_Coroutine("2학년 4반 교실에서 책과 쪽지를 찾아보자.", 3f, true));
                break;

            case 9:
                if (isRegame)
                {
                    ResetAllGhosts();
                    HallWayController.Instance.setupHallwaysReset(1);
                    SetPlayerPosition(new Vector3(3.3724f, 33.24f, -137.49f));
                    GameObject.Find("Floor_3(Clone)").transform.Find("Memo_3").gameObject.SetActive(true);
                    GameObject.Find("Floor_3(Clone)").transform.Find("book").gameObject.SetActive(true);
                }
                StartCoroutine(SendDialogue_Coroutine(new string[]
                { "뭐가 어떻게 돌아가는 거야...",
                "아까 봤던 연기는 뭐지?",
                "일단 찾으라는 책을 찾긴 했는데...",
                "가방에 넣어두자."}
                , 0f));
                StartCoroutine(CheckIfClassOut_2());
                break;

            case 10:
                if (isRegame)
                {
                    ResetAllGhosts();
                    HallWayController.Instance.setupHallwaysReset(1);
                    SetPlayerPosition(new Vector3(-65, 32.6f, -146.23f));
                    Debug.Log("1: " + player.transform.localPosition);
                    GameObject.Find("Floor_3(Clone)").transform.Find("Memo_4").gameObject.SetActive(true);
                }
                // 떨어지는 소리 플레이
                Debug.Log("1: " + player.transform.localPosition);
                StartCoroutine(SendPhoneMemo_Coroutine("2학년 7반에서 노트를 찾자", 3f, true));
                GameManager.Instance.PlayDropSound();
                StartCoroutine(SendDialogue_Coroutine(new string[]
                { "!!!",
                "뭐가 떨어졌나?",
                "무슨 소리지?",
                "교실 안으로 되돌아 가볼까?",
                "뭔가 불안한데 그냥 무시할까?"}
                , 1f));
                // 2학년 7반 들어가는지 체크
                StartCoroutine(CheckIf27Classroom());
                StartCoroutine(CheckIfClassBackIn());
                Debug.Log("2: " + player.transform.localPosition);
                Debug.Log("2: " + player.transform.position);
                break;
            case 11:
                if (isRegame)
                {
                    ResetAllGhosts();
                    HallWayController.Instance.setupHallwaysReset(1);
                    SetPlayerPosition(new Vector3(-66, 32.6f, -570));
                }
                StartCoroutine(SendPhoneMemo_Coroutine("2학년 7반에서 노트를 찾기 전, 세 층 위 2학년 3반에서 양초를 찾자.", 3f, true));

                break;

            case 12:
                if (isRegame)
                {
                    ResetAllGhosts();
                    HallWayController.Instance.setupHallwaysReset(4);
                    SetPlayerPosition(new Vector3(-62, 32.6f, 219));
                }
                StartCoroutine(SendPhoneMemo_Coroutine("위 층 모든 교실의 교탁에 촛불을 올리고 켜자.", 3f, true));
                StartCoroutine(CheckAllCandleOn());
                break;

            case 13:
                // 모든 촛불을 다 켰을 때
                if (isRegame)
                {
                    ResetAllGhosts();
                    HallWayController.Instance.setupHallwaysReset(5);
                    // 1반 위치로 세트
                    SetPlayerPosition(new Vector3(-49, 33.24f, 278));
                }
                //StartCoroutine(SendPhoneMemo_Coroutine("위 층 모든 교실의 교탁에 촛불을 올리고 켜자.", 3f, true));
                // 칠판 글씨
                TimeLinePlay(timeline_blackBoard);
                // 칠판 글씨 출력 완료하면 다음으로 이동
                StartCoroutine(WaitForSecAndProceed(8, 14));
                break;

            case 14:
                if (isRegame)
                {
                    ResetAllGhosts();
                    HallWayController.Instance.setupHallwaysReset(5);
                    // 1반 위치로 세트
                    SetPlayerPosition(new Vector3(-49, 33.24f, 278));
                }
                StartCoroutine(BlackScreenForSec(2f));
                MainCamera.transform.position = player.transform.position;
                StartCoroutine(SendDialogue_Coroutine(new string[]
                { "쪽지와 같은 글씨체...",
                "아까 그 검은 연기가 빛을 무서워하나?",
                "촛불이 꺼질 수도 있으니까 성냥은 가지고 다니자"
                }
                , 1f));
                StartCoroutine(SendPhoneMemo_Coroutine("이제 4층 아래에 있는 2학년 7반으로 가서 노트를 찾자.", 3f, true));
                break;
            case 15:
                if (isRegame)
                {
                    ResetAllGhosts();
                    HallWayController.Instance.setupHallwaysReset(1);
                    // 7반 위치로 세트
                    SetPlayerPosition(new Vector3(-49, 33.24f, -571));
                }
                StartCoroutine(SendPhoneMemo_Coroutine("일곱 층 위 2학년 4반으로 가서 책을 찾자.", 3f, true));
                // 8번 hallway의 2학년 4반에 들어가는지 체크 들어가면 다음으로 이동
                StartCoroutine(CheckIf24ClassroomAndFloor8());
                break;
            case 155:
                if (isRegame)
                {
                    ResetAllGhosts();
                    HallWayController.Instance.setupHallwaysReset(1);
                    // 7반 위치로 세트
                    SetPlayerPosition(new Vector3(-49, 33.24f, -571));
                }
                StartCoroutine(SendDialogue_Coroutine(new string[]
                { "익숙한 노트인데...",
                "일단 챙기자"
                }
                , 1f));
                break;
            case 16:
                if (isRegame)
                {
                    ResetAllGhosts();
                    HallWayController.Instance.setupHallwaysReset(8);
                    // 4반 입구 위치로 세트
                    SetPlayerPosition(new Vector3(-49, 33.24f, -136.71f));
                }
                // 빈 초가 있음
                StartCoroutine(SendDialogue_Coroutine(new string[]
                { "교탁위에 초가 보인다",
                "초를 켜는게 좋을까?"
                }
                , 1f));
                // 30초 내로 초를 켜는지 검사. 안켜면 귀신에게 당하며 게임오버 켜면 다음 진행
                StartCoroutine(CheckIf30SecOnCandle());
                break;
            case 17:
                if (isRegame)
                {
                    ResetAllGhosts();
                    HallWayController.Instance.setupHallwaysReset(8);
                    // 4반 입구 위치로 세트
                    SetPlayerPosition(new Vector3(-49, 33.24f, -136.71f));
                }
                StartCoroutine(SendDialogue_Coroutine(new string[]
                { "이제 책을 찾아볼까?"
                }
                , 1f));
                StartCoroutine(SendPhoneMemo_Coroutine("2학년 4반 교실에서 책을 찾자", 3f, true));
                // 책을 찾으면 다음으로 이동
                break;
            case 18:
                if (isRegame)
                {
                    ResetAllGhosts();
                    HallWayController.Instance.setupHallwaysReset(8);
                    // 4반 입구 위치로 세트
                    SetPlayerPosition(new Vector3(-49, 33.24f, -136.71f));
                }
                StartCoroutine(SendDialogue_Coroutine(new string[]
                { ".............",
                "이건 너무 익숙한 책인데?",
                "그리고 2학년 4반 이 자리면...",
                }
                , 1f));
                break;
            case 19:
                if (isRegame)
                {
                    ResetAllGhosts();
                    HallWayController.Instance.setupHallwaysReset(8);
                    // 4반 입구 위치로 세트
                    SetPlayerPosition(new Vector3(-49, 33.24f, -136.71f));
                }
                StartCoroutine(SendDialogue_Coroutine(new string[]
                { "..."
                }
                , 1f));
                StartCoroutine(SendPhoneMemo_Coroutine("두 층 위 2학년 2반의 내 자리(4행 4열)로 가자", 3f, true));

                break;
            case 20:
                if (isRegame)
                {
                    ResetAllGhosts();
                    HallWayController.Instance.setupHallwaysReset(10);
                    // 2반 입구 위치로 세트
                    SetPlayerPosition(new Vector3(-42, 33, 190));
                }
                // 계단 내려가거나 올라가면 탈출 탈출검사 (시간제한?) 탈출시 22로 이동
                StartCoroutine(CheckIfStairDownOrUp());
                // 교실 나가기 검사, 교실 나가면 다음으로 이동
                StartCoroutine(CheckIfLeave21Classroom());
                break;
            case 21:
                if (isRegame)
                {
                    ResetAllGhosts();
                    HallWayController.Instance.setupHallwaysReset(10);
                    // 2반 입구 위치로 세트
                    SetPlayerPosition(new Vector3(-42, 33, 190));
                }
                // 컷씬 시작 교실 칠판쪽에서 들리는 소리 (다이얼로그도 추가)
                StartCoroutine(SendDialogue_Coroutine(new string[]
                { "고마워..."
                }
                , "알 수 없는 목소리", 1f));
                break;

            case 22:
                // 엔딩 씬 재생
                if (isRegame)
                {
                    ResetAllGhosts();
                    HallWayController.Instance.setupHallwaysReset(10);
                    // 2반 입구 위치로 세트
                    SetPlayerPosition(new Vector3(-42, 33, 190));
                }
                videoRawImage.SetActive(true);
                videoPlayer.Play();
                // 편지화면 띄우기
                GameManager.Instance.OpenGameUI(finalMessage);
                StartCoroutine(WaitForESCKeyPressedAndProceed(221));
                break;
            case 221:
                StartCoroutine(SendDialogue_Coroutine(new string[]
                { "나는 편지에 적힌대로",
                "그 물건들을 모두 태웠다."
                }
                ));
                // hallwaycontroller setactive false
                // 모든 hallway setactive false
                HallWayController.Instance.isGameEnded = true;


                GameManager.Instance.CloseGameUI(finalMessage);
                StartCoroutine(WaitForDialougeEnd(23));
                break;
            case 23:
                if (isRegame)
                {
                    ResetAllGhosts();
                    HallWayController.Instance.setupHallwaysReset(10);
                    // 2반 입구 위치로 세트
                    SetPlayerPosition(new Vector3(-42, 33, 190));
                }
                videoPlayer.Stop();
                // go to scene title
                GameManager.Instance.LoadTitleScene();
                break;

        }
    }
}
