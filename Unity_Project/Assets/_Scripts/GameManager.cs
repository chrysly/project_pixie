using System;
using System.Collections;
using System.Collections.Generic;
using MoreMountains.Feedbacks;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private Fairy _player;
    private Chain _chain;
    private ObstacleField _field;
    private MMF_Player _impulse;

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
        _impulse = GetComponentInChildren<MMF_Player>();
        _player = FindObjectOfType<Fairy>();
        _chain = FindObjectOfType<Chain>();
        _field = FindObjectOfType<ObstacleField>();
        
        NewGame();
    }

    private void Update() {
        if (lives <= 0 && Input.GetKeyDown(KeyCode.Return)) {
            NewGame();
        }
    }

    private void NewGame() {
        gameOverText.SetActive(false);
        scoreText.text = "" + 0;
        healthText.text = "" + 3;
        score = 0;
        lives = 3;
        _chain.ResetVars();
        _chain.Respawn();
        _player.Respawn();
        _field.RegenerateMap();
    }

    private void GameOver() {
        Impulse();
        _player.gameObject.SetActive(false);
        gameOverText.SetActive(true);
    }

    public void ResetRound() {
        lives--;
        Impulse();
        healthText.text = "" + lives;
        if (lives <= 0) {
            GameOver();
        }
        
        _chain.Respawn();
    }

    public void NextLevel() {
        _chain.speed *= 1.3f;
        _chain.chainSegments += 2;
        Impulse();
        _chain.Respawn();
    }

    public void IncreaseScore(int amount) {
        score += amount;
        scoreText.text = "" + score;
    }

    public void Impulse() {
        _impulse.PlayFeedbacks();
    }
}
