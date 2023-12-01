using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.UIElements;

public class Chain : MonoBehaviour {
    private List<ChainSegment> _segments = new List<ChainSegment>();
    public ChainSegment chainSegmentPrefab;

    public Sprite chainHead;
    public Sprite chainTail;
    public Sprite chainBody;

    public int chainSegments = 12;
    
    private void Start() {
        Respawn();
    }

    private void Respawn() {
        foreach (ChainSegment segment in _segments) {
            Destroy(segment.gameObject);
        }
        
        _segments.Clear();

        for (int i = 0; i < chainSegments; i++) {
            Vector2 position = GridPosition(transform.position) + (Vector2.left * i);
            ChainSegment segment = Instantiate(chainSegmentPrefab, position, Quaternion.identity); 
            segment.spriteRenderer.sprite = i == 0 ? chainHead : chainBody;
            segment.chain = this;
            _segments.Add(segment);
        }

        for (int i = 0; i < _segments.Count; i++) {
            ChainSegment segment = _segments[i];
            segment.ahead = GetSegmentAt(i - 1);
            segment.behind = GetSegmentAt(i + 1);
        }
    }

    private ChainSegment GetSegmentAt(int index) {
        if (index >= 0 && index < _segments.Count) {
            return _segments[index];
        } else {
            return null;
        }
    }

    private Vector2 GridPosition(Vector2 position) {
        position.x = Mathf.Round(position.x);
        position.y = Mathf.Round(position.y);
        return position;
    }
}
