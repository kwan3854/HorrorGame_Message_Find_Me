using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandleOnPlane : MonoBehaviour
{
    [SerializeField] private GameObject candle;
    public void CandleOnTable()
    {
        candle.SetActive(true);
        gameObject.SetActive(false);
    }
}
