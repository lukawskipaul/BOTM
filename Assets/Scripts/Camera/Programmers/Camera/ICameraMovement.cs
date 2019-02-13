using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICameraMovement{
    GameObject targetObject { get; set; }
    void Start(Transform cameraTransform);
    void LateUpdate(Transform cameraTransform);
}
