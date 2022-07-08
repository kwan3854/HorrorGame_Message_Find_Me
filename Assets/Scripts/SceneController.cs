using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour
{
    // if Play button is clicked, load the Main game scene
    public void OnPlayButtonClick()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Main");
    }
}
