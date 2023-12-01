using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainSegment : MonoBehaviour
{
    public SpriteRenderer spriteRenderer { get; private set; }
    public Chain chain { get; set; }
    public ChainSegment ahead { get; set; }
    public ChainSegment behind { get; set; }
    public bool isHead => ahead == null;
    
    private void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
}
