using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashLightPositionController : MonoBehaviour
{
    public GameObject Camera;
    public GameObject FlashLight;

    [SerializeField] private float followSpeed = 5f;

    void FixedUpdate()
    {
        // FlashLight follows position and rotation of Camera
        // flashlight is attached below the camera
        FlashLight.transform.position = Camera.transform.position + new Vector3(0, -1f, 0);
        FlashLight.transform.rotation = Quaternion.Slerp(transform.rotation, Camera.transform.rotation, followSpeed * Time.deltaTime);
    }
}
