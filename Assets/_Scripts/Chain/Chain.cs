using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Chain : MonoBehaviour {
    private List<ChainSegment> _segments = new List<ChainSegment>();
    public ChainSegment chainSegmentPrefab;
    public Obstacle obstalcePrefab;

    public Sprite chainHead;
    public Sprite chainTail;
    public Sprite chainBody;
    public float speed = 5f;

    public int chainSegments = 8;
    
    private int _initSegments;
    private float _initSpeed;

    public LayerMask collisionMask;
    public BoxCollider2D homeBase;
    
    public int pointsHead = 100;
    public int pointsBody = 10;

    private void Awake() {
        _initSegments = chainSegments;
        _initSpeed = speed;
    }

    public void ResetVars() {
        chainSegments = _initSegments;
        speed = _initSpeed;
    }

    public void Respawn() {
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

    public void Remove(ChainSegment segment) {

        GameManager.Instance.IncreaseScore(segment.isHead ? pointsHead : pointsBody);
        
        Vector3 position = GridPosition(segment.transform.position);
        Instantiate(obstalcePrefab, position, Quaternion.identity);

        if (segment.ahead != null) {
            segment.ahead.behind = null;
        }

        if (segment.behind != null) {
            segment.behind.ahead = null;
            segment.behind.spriteRenderer.sprite = chainHead;
            segment.behind.UpdateHeadSegment();
        }
        
        _segments.Remove(segment);
        Destroy(segment.gameObject);

        if (_segments.Count == 0) {
            GameManager.Instance.NextLevel();
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
