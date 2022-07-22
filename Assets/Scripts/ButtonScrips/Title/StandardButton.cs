using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StandardButton : MonoBehaviour
{
    [SerializeField] private Color hoverColor;
    [SerializeField] private float hoverScale = 1.3f;

    private Image buttonImage;
    private Color defaultColor;
    private Vector3 defaultScale;
    private Vector3 hoverSize;

    private float speed = 10f;
    // private ColorBlock colorBlock;

    void Start()
    {
        buttonImage = GetComponent<Image>();
        defaultColor = buttonImage.color;
        defaultScale = transform.localScale;
        hoverSize = new Vector3(defaultScale.x * hoverScale, defaultScale.y * hoverScale, defaultScale.z * hoverScale);
    }

    public void OnHover()
    {
        buttonImage.color = hoverColor;
        StartCoroutine(OnMouseHover());
    }

    public void OnExit()
    {
        buttonImage.color = defaultColor;
        StartCoroutine(OnMouseExit());
    }

    private IEnumerator OnMouseHover()
    {
        float time = 0f;
        GameTitleManager.Instance.PlayButtonHoverSound();
        while (time < 1f)
        {
            transform.localScale = Vector3.Lerp(defaultScale, hoverSize, time);
            time += Time.deltaTime * speed;
            yield return null;
        }
    }

    private IEnumerator OnMouseExit()
    {
        float time = 0f;
        while (time < 1f)
        {
            transform.localScale = Vector3.Lerp(hoverSize, defaultScale, time);
            time += Time.deltaTime * speed;
            yield return null;
        }
    }

    public void OnPlayButtonClicked()
    {
        GameTitleManager.Instance.OnPlayButtonClicked();
    }

    public void OnQuitButtonClicked()
    {
        GameTitleManager.Instance.OnQuitButtonClicked();
    }

    public void OnCreditsButtonClicked()
    {
        GameTitleManager.Instance.OnCreditsButtonClicked();
    }

    public void OnInstructionsButtonClicked()
    {
        GameTitleManager.Instance.OnInstructionsButtonClicked();
    }
}