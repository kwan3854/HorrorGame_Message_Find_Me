using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Candle_Controller : MonoBehaviour
{
    public bool isLit = false;
    [SerializeField] AudioClip candleSound;
    public void LightOn()
    {
        transform.Find("flame").gameObject.SetActive(true);
        // light on sound
        isLit = true;
        //GameManager.Instance.PlayFireOnSound();
        gameObject.GetComponent<AudioSource>().PlayOneShot(candleSound);
    }
}
