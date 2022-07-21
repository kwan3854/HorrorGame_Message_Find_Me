using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTitleManager : MonoBehaviour
{
    private static GameTitleManager instance = null;
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

        Cursor.visible = true;
    }
}
