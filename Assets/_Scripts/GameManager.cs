using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private Fairy _player;
    private Chain _chain;
    private ObstacleField _field;

    private int score;
    private int lives;

    private void Awake() {
        if (Instance == null) {
            Instance = this;
        }
        else {
            Destroy(gameObject);
        }
    }

    private void OnDestroy() {
        if (Instance == this) {
            Instance = null;
        }
    }

    private void Start() {
        _player = FindObjectOfType<Fairy>();
        _chain = FindObjectOfType<Chain>();
        _field = FindObjectOfType<ObstacleField>();
        
        NewGame();
    }

    private void Update() {
        if (lives <= 0 && Input.anyKeyDown) {
            NewGame();
        }
    }

    private void NewGame() {
        score = 0;
        lives = 3;
        _chain.Respawn();
        _player.Respawn();
        _field.RegenerateMap();
    }

    private void GameOver() {
        _player.gameObject.SetActive(false);
    }

    public void ResetRound() {
        lives--;
        if (lives <= 0) {
            GameOver();
        }
        
        _chain.Respawn();
    }

    public void NextLevel() {
        
    }
}
