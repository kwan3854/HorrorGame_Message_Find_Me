using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PhoneUI : MonoBehaviour
{
    private static PhoneUI instance = null;

    [SerializeField] private float speed = 0.1f;
    [SerializeField] private Transform realPhoneTransform;
    [SerializeField] private Transform phoneTransform;
    [SerializeField] private TextMeshPro PhoneTextUI;
    [SerializeField] private float realTimeRotationAmount = 1.5f;

    private Vector3 startPosition = new Vector3(0, -5, 5);
    private Vector3 endPosition = new Vector3(0, 0, 3);
    private Quaternion realPhoneStartRotation = Quaternion.Euler(new Vector3(90, 90, 0));
    private Quaternion realPhoneEndRotation = Quaternion.Euler(new Vector3(80, 180, -20));
    // private Quaternion phoneStartRotation = Quaternion.Euler(new Vector3(0, -90, 0));
    // private Quaternion phoneEndRotation = Quaternion.Euler(new Vector3(0, 0, 0));

    private float playerMouseX;
    private float playerMouseY;
    private Quaternion movingPhoneRotation;
    private float maxRotationAmount = 20f;

    public static PhoneUI Instance
    {
        get
        {
            return instance;
        }
    }

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

        // phoneTransform = this.transform.Find("Phone");
        gameObject.SetActive(false);
    }

    public void OpenPhoneUI()
    {
        GameManager.Instance.IsUIEnabled = true;

        Debug.Log("StartPosition: " + startPosition);
        Debug.Log("PhoneTransform: " + phoneTransform.position);
        Debug.Log("PhoneTransform: " + phoneTransform.name);
        StartCoroutine(OpenPhoneUI_Coroutine());
    }

    public void ClosePhoneUI()
    {
        StartCoroutine(ClosePhoneUI_Coroutine());
        GameManager.Instance.IsUIEnabled = false;
    }

    private IEnumerator OpenPhoneUI_Coroutine()
    {
        float time = 0f;
        while (time < 1f)
        {
            realPhoneTransform.localPosition = Vector3.Lerp(startPosition, endPosition, time);
            realPhoneTransform.localRotation = Quaternion.Slerp(realPhoneStartRotation, realPhoneEndRotation, time);
            time += Time.fixedDeltaTime * speed;
            yield return null;
        }
        StartCoroutine(Phone3DMoving_Coroutine());
    }

    private IEnumerator ClosePhoneUI_Coroutine()
    {
        float time = 0f;
        while (time < 1f)
        {
            realPhoneTransform.localPosition = Vector3.Lerp(endPosition, startPosition, time);
            realPhoneTransform.localRotation = Quaternion.Slerp(realPhoneEndRotation, realPhoneStartRotation, time);
            time += Time.fixedDeltaTime * speed;
            yield return null;
        }
        gameObject.SetActive(false);
        GameManager.Instance.IsUIEnabled = false;
        StopAllCoroutines();
    }

    private IEnumerator Phone3DMoving_Coroutine()
    {
        while (true)
        {
            playerMouseX = InputManager.Instance.GetPlayerLook().x;
            playerMouseY = InputManager.Instance.GetPlayerLook().y;
            playerMouseX = Mathf.Clamp(playerMouseX, -maxRotationAmount, maxRotationAmount);
            playerMouseY = Mathf.Clamp(playerMouseY, -maxRotationAmount, maxRotationAmount);
            movingPhoneRotation = Quaternion.Euler
            (new Vector3(realPhoneEndRotation.eulerAngles.x + playerMouseY + realTimeRotationAmount,
             realPhoneEndRotation.eulerAngles.y + playerMouseX * realTimeRotationAmount,
             realPhoneEndRotation.eulerAngles.z));

            realPhoneTransform.localRotation =
            Quaternion.Slerp(realPhoneTransform.localRotation, movingPhoneRotation, Time.deltaTime * speed);

            yield return null;
        }
    }

    private IEnumerator PhoneMessageIncome_Coroutine(string message)
    {
        GameManager.Instance.PlayPhoneVibrateSound();

        PhoneTextUI.text = message;
        yield return null;
    }

    public void MessageIncome(string message)
    {
        // OpenPhoneUI();
        StartCoroutine(PhoneMessageIncome_Coroutine(message));
    }
}
