using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialogue : MonoBehaviour
{
    private static Dialogue instance = null;

    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private GameObject nameBackground;
    [SerializeField] private float typingSpeed = 0.025f;

    private string[] sentences;
    private int index;
    public bool isRunning = false;

    private void Awake()
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

    public static Dialogue Instance
    {
        get
        {
            return instance;
        }
    }

    private void Start()
    {
        text.text = string.Empty;
        // StartDialogue();
    }

    // Update is called once per frame
    private void Update()
    {


    }

    public void OnClicked()
    {
        if (text.text == sentences[index])
        {
            NextSentence();
        }
        else
        {
            StopAllCoroutines();
            text.text = sentences[index];
        }
    }

    // public void StartDialogue(string[] _sentence)
    // {
    //     isRunning = true;
    //     sentences = _sentence;
    //     index = 0;
    //     // gameObject.SetActive(true);
    //     GameManager.Instance.OpenGameUI(gameObject);
    //     StartCoroutine(TypeSentence());
    //     GameManager.Instance.IsUIEnabled = true;
    // }

    public void StartDialogue(string[] _sentence, bool isName)
    {
        if (isRunning)
        {
            Debug.Log("Dialogue is already running");
            return;
            // StopAllCoroutines();
        }
        isRunning = true;
        sentences = _sentence;
        index = 0;
        // gameObject.SetActive(true);
        GameManager.Instance.OpenGameUI(gameObject);
        if (isName)
        {
            nameText.gameObject.SetActive(true);
            nameBackground.SetActive(true);
            StartCoroutine(TypeSentence());
        }
        else
        {
            nameText.gameObject.SetActive(false);
            nameBackground.SetActive(false);
            StartCoroutine(TypeSentence());
        }
        GameManager.Instance.IsUIEnabled = true;
    }

    public void SetName(string name)
    {
        nameText.text = name + ": ";
    }

    private IEnumerator TypeSentence()
    {
        // Typing Sound
        yield return new WaitForSecondsRealtime(0.2f);
        GameManager.Instance.PlayTypingSound();
        // Type each Character one by one
        foreach (char letter in sentences[index].ToCharArray())
        {
            text.text += letter;
            yield return new WaitForSecondsRealtime(typingSpeed);
        }
    }

    private void NextSentence()
    {
        // If the sentence is not the last one, go to the next one
        if (index < sentences.Length - 1)
        {
            index++;
            text.text = string.Empty;
            StartCoroutine(TypeSentence());
        }
        else
        {
            Debug.Log("End of Dialogue");
            text.text = string.Empty;
            isRunning = false;
            gameObject.SetActive(false);
            GameManager.Instance.IsUIEnabled = false;
        }
    }
}
