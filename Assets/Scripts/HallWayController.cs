using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HallWayController : MonoBehaviour
{

    public List<Transform> hallwayPrefabs;
    public Transform player;

    // public Transform lastDoorBillboard;
    // public Transform frontDoor;
    // public Transform backDoor;

    private List<Transform> hallways = new List<Transform>();
    private int playerHallwayIndex = 0;
    private const int MAX_HALLWAYS = 3;
    private const float MAX_Y_BOUND = 58f;
    private const float MIN_Y_BOUND = 0f;
    private const float HALLWAY_HEIGHT = 49f;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < hallwayPrefabs.Count; i++)
        {
            this.hallways.Add(GameObject.Instantiate(this.hallwayPrefabs[i], new Vector3(0, HALLWAY_HEIGHT * i, 0), Quaternion.identity));
        }
        //this.player.position = new Vector3(5f, 2f, 0);
        //this.lastDoorBillboard.position = new Vector3(5f, 1.5f, HALLWAY_LENGTH * MAX_HALLWAYS);
        this.setupHallways();
    }



    private void setupHallways()
    {
        Debug.Log("Player Hallway Index: " + this.playerHallwayIndex);
        this.hideAllHallways();
        for (int i = 0; i < MAX_HALLWAYS; i++)
        {
            hallways[(this.playerHallwayIndex + i) % this.hallways.Count].gameObject.SetActive(true);
            hallways[(this.playerHallwayIndex + i) % this.hallways.Count].position = new Vector3(0, HALLWAY_HEIGHT * i, 0);
        }
        hallways[(this.playerHallwayIndex + (this.hallways.Count - 1)) % this.hallways.Count].gameObject.SetActive(true);
        hallways[(this.playerHallwayIndex + (this.hallways.Count - 1)) % this.hallways.Count].position = new Vector3(0, -HALLWAY_HEIGHT, 0);

    }
    private void hideAllHallways()
    {
        for (int i = 0; i < hallways.Count; i++)
        {
            hallways[i].gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        this.handleInfiniteHallway();
    }

    private void handleInfiniteHallway()
    {
        if (this.isPlayerOutOfBounds())
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
        return this.player.position.y > MAX_Y_BOUND || this.player.position.y < MIN_Y_BOUND;
    }
}
