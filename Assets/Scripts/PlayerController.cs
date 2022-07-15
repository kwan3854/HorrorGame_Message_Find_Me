using UnityEngine;
using TMPro;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float playerSpeed = 10.0f;
    [SerializeField] private float jumpHeight = 1.0f;
    [SerializeField] private float gravityValue = -9.81f;

    private bool isFlashLightOn = true;
    public GameObject lightSource;

    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    private InputManager inputManager;
    private Transform cameraTransform;


    [SerializeField] private TextMeshPro UseText;
    [SerializeField] private float MaxUseDistance = 5f;
    [SerializeField] private LayerMask UseLayers;


    private void Start()
    {
        controller = gameObject.GetComponent<CharacterController>();
        inputManager = InputManager.Instance;
        cameraTransform = Camera.main.transform;

        lightSource.gameObject.SetActive(true);
        isFlashLightOn = true;
    }

    public void OnUse()
    {
        if (Physics.Raycast(cameraTransform.position, cameraTransform.forward, out RaycastHit hit, MaxUseDistance, UseLayers))
        {
            Debug.Log("Hit: " + hit.collider.gameObject.name);
            if (hit.collider.TryGetComponent<Door>(out Door door))
            {
                if (door.IsOpen)
                {
                    // Debug.Log("Door Close: " + door.gameObject.name);
                    door.Close();
                    // foreach (Transform child in door.transform)
                    // {
                    //     door.Close();
                    // }
                }
                else
                {
                    // Debug.Log("Door Open: " + door.gameObject.name);
                    door.Open(transform.position);
                    // foreach (Transform child in door.transform)
                    // {
                    //     door.Open(child.position);
                    // }
                }
            }
        }
    }

    private void DoorUse()
    {
        if (Physics.Raycast(cameraTransform.position, cameraTransform.forward, out RaycastHit hit, MaxUseDistance, UseLayers)
&& hit.collider.TryGetComponent<Door>(out Door door))
        {
            if (door.IsOpen)
            {
                UseText.SetText("Close \"E\"");
            }
            else
            {
                UseText.SetText("Open \"E\"");
            }
            UseText.gameObject.SetActive(true);
            UseText.transform.position = hit.point - (hit.point - cameraTransform.position).normalized * 0.01f;
            UseText.transform.rotation = Quaternion.LookRotation((hit.point - cameraTransform.position).normalized);
        }
        else
        {
            UseText.gameObject.SetActive(false);
        }


        if (inputManager.PlayerUse())
        {
            Debug.Log("Use");
            OnUse();
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

        DoorUse();
    }
}