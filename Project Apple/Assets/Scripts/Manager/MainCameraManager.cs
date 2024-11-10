using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCameraManager : CameraManager
{
    void OnPreCull() => GL.Clear(true, true, Color.black);
}
