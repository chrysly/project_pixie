using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;
using Random = UnityEngine.Random;

public class ObstacleField : MonoBehaviour {
    private BoxCollider2D _area;
    public Obstacle obstalcePrefab;
    public int obstacleCount = 50;

    private List<Obstacle> _activeObstacles;

    private void Awake() {
        _area = GetComponent<BoxCollider2D>();
        _activeObstacles = new List<Obstacle>();
    }

    private void Start() {
        Generate();
    }

    private void Generate() {
        Bounds bounds = _area.bounds;

        for (int i = 0; i < obstacleCount; i++) {
            Vector2 position = Vector2.zero;

            position.x = Mathf.Round(Random.Range(bounds.min.x, bounds.max.x));
            position.y = Mathf.Round(Random.Range(bounds.min.y, bounds.max.y));

            _activeObstacles.Add(Instantiate(obstalcePrefab, position, Quaternion.identity, transform));
        }
    }

    public void RegenerateMap() {
        foreach (Obstacle obstacle in _activeObstacles) {
            Destroy(obstacle.gameObject);
        }

        _activeObstacles = new List<Obstacle>();
        
        Generate();
    }
}
