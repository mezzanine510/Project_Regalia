﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    [SerializeField] Transform target;
    // Camera camera;
    
    void Awake()
    {
        // camera = Camera.main;
    }

    void LateUpdate()
    {
        transform.position = target.transform.position;
        
        // Camera.main.transform.position = new Vector3(
        //     target.transform.position.x,
        //     camera.transform.position.y,
        //     target.transform.position.z
        // );
    }
}
