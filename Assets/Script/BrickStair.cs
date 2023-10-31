using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickStair : ColorGameObject
{
    private void Start()
    {
        Renderer brickStairRender = GetComponent<Renderer>();
        //this.colorType = ColorType.None;
        brickStairRender.enabled = false;
        
    }
    
}
