using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Diaglogue : MonoBehaviour
{
    private static Diaglogue instance = null;

    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private float typingSpeed = 0.025f;

    private string[] sentences;
    private int index;

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

    public static Diaglogue Instance
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
        if (InputManager.Instance.PlayerMouseLeftClick())
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

    }

    public void StartDialogue(string[] _sentence)
    {
        sentences = _sentence;
        index = 0;
        StartCoroutine(TypeSentence());
        gameObject.SetActive(true);
        GameManager.Instance.IsUIEnabled = true;
    }

    private IEnumerator TypeSentence()
    {
        // Typing Sound
        GameManager.Instance.PlayTypingSound();
        // Type each Character one by one
        foreach (char letter in sentences[index].ToCharArray())
        {
            text.text += letter;
            yield return new WaitForSeconds(typingSpeed);
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
            text.text = string.Empty;
            gameObject.SetActive(false);
            GameManager.Instance.IsUIEnabled = false;
        }
    }
}
