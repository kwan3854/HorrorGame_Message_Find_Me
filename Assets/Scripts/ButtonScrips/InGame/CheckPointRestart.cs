using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointRestart : MonoBehaviour
{
    public void OnCheckPointRestartButtonClicked()
    {
        GameManager.Instance.CheckPointRestart();
    }
}
