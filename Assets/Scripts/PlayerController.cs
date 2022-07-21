using UnityEngine;
using TMPro;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [Header("Player Configs")]
    [SerializeField] private float playerSpeed = 10.0f;
    [SerializeField] private float jumpHeight = 1.0f;
    [SerializeField] private float gravityValue = -9.81f;

    private bool isFlashLightOn = true;
    [Header("Player Tool Add-On Features")]
    [SerializeField] private GameObject lightSource;

    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    private InputManager inputManager;
    private Transform cameraTransform;

    [Header("Player Use Function Settings")]
    [SerializeField] private TextMeshPro UseText;
    [SerializeField] private float MaxUseDistance = 5f;
    [SerializeField] private LayerMask UseLayers;

    // [Header("Menu Objects")]
    // //[SerializeField] private GameObject inGameMenuObject;
    // [SerializeField] private GameObject MemoUIObject;
    // [SerializeField] private GameObject pauseMenuObject;


    private void Start()
    {
        controller = gameObject.GetComponent<CharacterController>();
        inputManager = InputManager.Instance;
        cameraTransform = Camera.main.transform;

        lightSource.gameObject.SetActive(true);
        isFlashLightOn = true;

        Debug.Assert(lightSource != null, "No Light Source Found");
        Debug.Assert(UseText != null, "No Use Text Found");
        Debug.Assert(cameraTransform != null, "No Camera Found");
        Debug.Assert(inputManager != null, "No Input Manager Found");
        Debug.Assert(controller != null, "No Character Controller Found");
        Debug.Assert(playerSpeed > 0, "Player Speed is not set");
        Debug.Assert(jumpHeight > 0, "Jump Height is not set");
        Debug.Assert(gravityValue < 0, "Gravity Value is not set");
        Debug.Assert(MaxUseDistance > 0, "Max Use Distance is not set");
        Debug.Assert(UseLayers > 0, "Use Layers is not set");
    }

    private void UseDoor(Door door)
    {
        switch (door.IsOpen)
        {
            case true:
                door.Close();
                break;
            case false:
                door.Open(transform.position);
                break;
        }
    }

    private void Use()
    {
        if (Physics.Raycast(cameraTransform.position, cameraTransform.forward, out RaycastHit hit, MaxUseDistance, UseLayers))
        {
            if (hit.collider.TryGetComponent<Door>(out Door door))
            {
                if (inputManager.PlayerUse())
                {
                    UseDoor(door);
                }
                if (door.IsOpen)
                {
                    UseText.SetText("닫기 \"E\"");
                }
                else
                {
                    UseText.SetText("열기 \"E\"");
                }
                UseText.gameObject.SetActive(true);
                UseText.transform.position = hit.point - (hit.point - cameraTransform.position).normalized * 0.01f;
                UseText.transform.rotation = Quaternion.LookRotation((hit.point - cameraTransform.position).normalized);
            }

            else if (hit.collider.TryGetComponent<Memo>(out Memo memo))
            {
                if (inputManager.PlayerUse())
                {
                    memo.Read();
                }

                UseText.SetText("쪽지 읽기 \"E\"");

                UseText.gameObject.SetActive(true);
                UseText.transform.position = hit.point - (hit.point - cameraTransform.position).normalized * 0.01f;
                UseText.transform.rotation = Quaternion.LookRotation((hit.point - cameraTransform.position).normalized);
            }
        }
        else
        {
            UseText.gameObject.SetActive(false);
        }
    }

    private void ESCPressed()
    {
        if (inputManager.PlayerQuit())
        {
            if (GameManager.Instance.IsUIEnabled)
            {
                GameManager.Instance.CloseAllGameUI();
                if (GameManager.Instance.IsGamePaused)
                {
                    GameManager.Instance.ResumeGame();
                }
            }
            else
            {
                GameManager.Instance.PauseGame();
            }
        }
    }

    private void CurorControll()
    {
        switch (GameManager.Instance.IsUIEnabled)
        {
            case true:
                Cursor.visible = true;
                break;
            case false:
                Cursor.visible = false;
                break;
        }
    }

    void Update()
    {
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        Vector2 movement = inputManager.GetPlayerMovement();
        Vector3 move = new Vector3(movement.x, 0, movement.y);
        move = cameraTransform.forward * move.z + cameraTransform.right * move.x;
        move.y = 0f;
        controller.Move(move * Time.deltaTime * playerSpeed);

        // Footstep sound
        if (move.magnitude > 0.1f && groundedPlayer)
        {
            this.GetComponent<AudioSource>().volume = 1f;
        }
        else
        {
            this.GetComponent<AudioSource>().volume = 0f;
        }

        // Changes the height position of the player..
        if (inputManager.PlayerJumped() && groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);

        if (inputManager.PlayerFlashLightOn())
        {
            isFlashLightOn = !isFlashLightOn;
            lightSource.gameObject.SetActive(isFlashLightOn);
        }

        Use();
        ESCPressed();
        CurorControll();
    }
}