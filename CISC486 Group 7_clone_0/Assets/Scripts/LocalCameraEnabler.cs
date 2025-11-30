using UnityEngine;
using PurrNet;
using System.Globalization;

public class LocalCameraEnabler : NetworkIdentity
{
    public GameObject cameraRig;

    void Start()
    {
        if (!isOwner)
        {
            cameraRig.SetActive(false);
        }
    }
}
