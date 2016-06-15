using UnityEngine;
using System.Collections;
using System;

public class DoorObstacle : Obstacle {

    public Sprite openedSprite;
    public SpriteRenderer sr;

    private bool opened = false;
    
    public void Start()
    {
        
    }

    public override bool Moveable()
    {
        return opened;
    }

    public void Open()
    {
        opened = true;
        sr.sprite = openedSprite;
    }
}
