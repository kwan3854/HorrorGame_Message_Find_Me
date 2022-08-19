using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HallWayController : MonoBehaviour
{
    private static HallWayController instance = null;

    [SerializeField] private List<Transform> hallwayPrefabs;
    [SerializeField] private Transform player;

    private List<Transform> hallways = new List<Transform>();
    private int playerHallwayIndex = 0;
    private int hallwayCount = 0;
    private const int MAX_HALLWAYS = 3;
    // private Door[][] doors = new Door[MAX_HALLWAYS][]; // 10 doors per hallway
    private const float MAX_Y_BOUND = 58f;
    private const float MIN_Y_BOUND = 0f;
    private const float HALLWAY_HEIGHT = 49f;
    public bool isGameEnded = false;

    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            // DontDestroyOnLoad(gameObject);
        }

        Debug.Assert(hallwayPrefabs.Count > 0, "No Hallway Prefabs Found");
        Debug.Assert(player != null, "No Player Found");

        for (int i = 0; i < hallwayPrefabs.Count; i++)
        {
            this.hallways.Add(GameObject.Instantiate(this.hallwayPrefabs[i], new Vector3(0, HALLWAY_HEIGHT * i, 0), Quaternion.identity));
        }
        hallwayCount = this.hallways.Count;
        this.setupHallways();
    }

    public static HallWayController Instance
    {
        get
        {
            return instance;
        }
    }

    public void setupHallwaysReset(int hallwayIndex)
    {
        Debug.Log("Player Hallway Index: " + this.playerHallwayIndex);
        playerHallwayIndex = hallwayIndex;

        this.hideAllHallways();
        for (int i = 0; i < MAX_HALLWAYS; i++)
        {
            hallways[(this.playerHallwayIndex + i) % hallwayCount].gameObject.SetActive(true);
            hallways[(this.playerHallwayIndex + i) % hallwayCount].position = new Vector3(0, HALLWAY_HEIGHT * i, 0);
        }
        hallways[(this.playerHallwayIndex + (hallwayCount - 1)) % hallwayCount].gameObject.SetActive(true);
        hallways[(this.playerHallwayIndex + (hallwayCount - 1)) % hallwayCount].position = new Vector3(0, -HALLWAY_HEIGHT, 0);

    }


    private void setupHallways()
    {
        Debug.Log("Player Hallway Index: " + this.playerHallwayIndex);



        this.hideAllHallways();
        for (int i = 0; i < MAX_HALLWAYS; i++)
        {
            hallways[(this.playerHallwayIndex + i) % hallwayCount].gameObject.SetActive(true);
            hallways[(this.playerHallwayIndex + i) % hallwayCount].position = new Vector3(0, HALLWAY_HEIGHT * i, 0);
        }
        hallways[(this.playerHallwayIndex + (hallwayCount - 1)) % hallwayCount].gameObject.SetActive(true);
        hallways[(this.playerHallwayIndex + (hallwayCount - 1)) % hallwayCount].position = new Vector3(0, -HALLWAY_HEIGHT, 0);

    }

    //private void setupDoors()
    private void hideAllHallways()
    {
        for (int i = 0; i < hallways.Count; i++)
        {
            hallways[i].gameObject.SetActive(false);
        }
    }

    void FixedUpdate()
    {
        if (this.isGameEnded)
        {
            hideAllHallways();
        }
        else
        {
            this.handleInfiniteHallway();
        }
    }

    private void handleInfiniteHallway()
    {
        if (ScenarioManager.Instance.floorNumber != this.playerHallwayIndex)
        {
            ScenarioManager.Instance.floorNumber = this.playerHallwayIndex;
            Debug.Log("Floor Number: " + ScenarioManager.Instance.floorNumber);
        }

        if (this.isPlayerOutOfBounds() && !GameManager.Instance.IsGamePaused)
        {
            float posYmod = HALLWAY_HEIGHT;
            if (this.player.position.y > MAX_Y_BOUND)
            {
                this.playerHallwayIndex = (this.playerHallwayIndex + 1) % this.hallways.Count;
                posYmod *= -1;
            }
            else
            {
                this.playerHallwayIndex = (this.playerHallwayIndex > 0) ? (this.playerHallwayIndex - 1) % this.hallways.Count : this.hallways.Count - 1;
            }
            this.player.position = new Vector3(this.player.position.x, this.player.position.y + posYmod, this.player.position.z);
            // Debug.Log("Player Y position: " + this.player.position.y);
            this.setupHallways();
        }

    }

    private bool isPlayerOutOfBounds()
    {
        return (this.player.position.y > MAX_Y_BOUND || this.player.position.y < MIN_Y_BOUND) && InputManager.Instance.IsPlayerMoving();
    }
}
