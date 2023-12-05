using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private Fairy _player;
    private Chain _chain;
    private ObstacleField _field;

    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private GameObject gameOverText;

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
        gameOverText.SetActive(false);
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
        gameOverText.SetActive(false);
        scoreText.text = "" + 0;
        healthText.text = "" + 3;
        score = 0;
        lives = 3;
        _chain.Respawn();
        _player.Respawn();
        _field.RegenerateMap();
    }

    private void GameOver() {
        _player.gameObject.SetActive(false);
        gameOverText.SetActive(true);
    }

    public void ResetRound() {
        lives--;
        healthText.text = "" + lives;
        if (lives <= 0) {
            GameOver();
        }
        
        _chain.Respawn();
    }

    public void NextLevel() {
        _chain.speed *= 1.3f;
        _chain.chainSegments += 2;
        _chain.Respawn();
    }

    public void IncreaseScore(int amount) {
        score += amount;
        scoreText.text = "" + score;
    }
}
