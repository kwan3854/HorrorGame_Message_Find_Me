using System.Collections;
using UnityEngine;

public class Door : MonoBehaviour
{
    public bool IsLocked = false;
    public bool IsOpen = false;
    [SerializeField]
    private bool IsRotatingDoor = false;
    [SerializeField]
    private float Speed = 2f;
    [Header("Rotation Configs")]
    [SerializeField]
    private float RotationAmount = 90f;
    [SerializeField]
    private float ForwardDirection = 0;
    [Header("Sliding Configs")]
    [SerializeField]
    private Vector3 SlideDirection = Vector3.back;
    [SerializeField]
    private float SlideAmount = 1.9f;

    private Vector3 StartRotation;
    // private Vector3 StartPosition;
    private Vector3 Forward;

    private Coroutine AnimationCoroutine;
    private bool IsAnimating = false;
    private Vector3 ClosedPosition;

    [Header("Sound Effect Configs")]
    [SerializeField] private AudioClip SlidingOpenSound;
    [SerializeField] private AudioClip SlidingCloseSound;
    [SerializeField] private AudioClip LockedSound;

    private void OnEnable()
    {
        StartRotation = transform.rotation.eulerAngles;
        // Since "Forward" actually is pointing into the door frame, choose a direction to think about as "forward" 
        Forward = transform.right;
        //StartPosition = transform.position;

        // Debug.Log(transform.name + "Start Position: " + StartPosition);
    }

    public void Open(Vector3 UserPosition)
    {
        if (IsLocked == true)
        {
            this.GetComponent<AudioSource>().clip = LockedSound;
            this.GetComponent<AudioSource>().Play();
            StartCoroutine(ScenarioManager.Instance.SendDialogue_Coroutine(new string[] { "이 문은 잠겨있다" }, 1f));
            return;
        }

        if (!IsOpen && IsAnimating == false)
        {
            if (IsRotatingDoor)
            {
                float dot = Vector3.Dot(Forward, (UserPosition - transform.position).normalized);
                Debug.Log($"Dot: {dot.ToString("N3")}");
                StartCoroutine(DoRotationOpen(dot));
            }
            else
            {
                StartCoroutine(DoSlidingOpen());
            }
        }
    }

    private IEnumerator DoRotationOpen(float ForwardAmount)
    {
        IsAnimating = true;
        Quaternion startRotation = transform.rotation;
        Quaternion endRotation;

        if (ForwardAmount >= ForwardDirection)
        {
            endRotation = Quaternion.Euler(new Vector3(0, StartRotation.y + RotationAmount, 0));
        }
        else
        {
            endRotation = Quaternion.Euler(new Vector3(0, StartRotation.y - RotationAmount, 0));
        }

        IsOpen = true;

        float time = 0;
        while (time < 1)
        {
            transform.rotation = Quaternion.Slerp(startRotation, endRotation, time);
            yield return null;
            time += Time.deltaTime * Speed;
        }
        IsAnimating = false;
    }

    private IEnumerator DoSlidingOpen()
    {
        IsAnimating = true;
        Vector3 endPosition = transform.position + SlideAmount * SlideDirection;
        Vector3 startPosition = transform.position;
        ClosedPosition = startPosition;

        float time = 0;
        IsOpen = true;
        // Play sound effect
        this.GetComponent<AudioSource>().clip = SlidingOpenSound;
        this.GetComponent<AudioSource>().Play();

        while (time <= 1)
        {
            transform.position = Vector3.Slerp(startPosition, endPosition, time);
            yield return null;
            time += Time.deltaTime * Speed;
        }
        transform.position = endPosition;
        IsAnimating = false;
    }

    public void Close()
    {
        if (IsOpen && IsAnimating == false)
        {
            if (IsRotatingDoor)
            {
                StartCoroutine(DoRotationClose());
            }
            else
            {
                StartCoroutine(DoSlidingClose());
            }
        }
    }

    private IEnumerator DoRotationClose()
    {
        IsAnimating = true;
        Quaternion startRotation = transform.rotation;
        Quaternion endRotation = Quaternion.Euler(StartRotation);

        IsOpen = false;

        float time = 0;
        while (time < 1)
        {
            transform.rotation = Quaternion.Slerp(startRotation, endRotation, time);
            yield return null;
            time += Time.deltaTime * Speed;
        }
        IsAnimating = false;
    }

    private IEnumerator DoSlidingClose()
    {
        IsAnimating = true;
        Vector3 endPosition = transform.position - SlideAmount * SlideDirection;
        Vector3 startPosition = transform.position;
        float time = 0;

        IsOpen = false;

        // Play sound effect
        this.GetComponent<AudioSource>().clip = SlidingCloseSound;
        this.GetComponent<AudioSource>().Play();

        while (time <= 1)
        {
            transform.position = Vector3.Slerp(startPosition, endPosition, time);
            yield return null;
            time += Time.deltaTime * Speed;
        }
        transform.position = endPosition;
        IsAnimating = false;
    }
}