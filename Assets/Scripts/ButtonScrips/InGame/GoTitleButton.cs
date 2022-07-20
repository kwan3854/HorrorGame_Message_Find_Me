using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoTitleButton : MonoBehaviour
{
    public void OnGoTitleButtonClicked()
    {
        GameManager.Instance.LoadTitleScene();
    }
}
