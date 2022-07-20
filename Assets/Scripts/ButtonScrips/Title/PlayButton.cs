using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayButton : MonoBehaviour
{
    public void OnPlayButtonClicked()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Main");
    }
}
