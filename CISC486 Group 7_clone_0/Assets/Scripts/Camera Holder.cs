using PurrNet;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class CameraHolder : NetworkIdentity
{
    public Transform cameraPosition;

    protected override void OnSpawned()
    {
        base.OnSpawned();

        enabled = isOwner;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = cameraPosition.position;
    }
}