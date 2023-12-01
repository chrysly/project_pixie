using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour {
    public Sprite[] states;
    public int _health;
    private SpriteRenderer _spriteRenderer;

    private void Awake() {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _health = states.Length;
    }

    public void Damage(int amount) {
        _health -= amount;

        if (_health > 0) {
            _spriteRenderer.sprite = states[states.Length - _health];
        }
        else {
            Destroy(gameObject);
        }
    } 

    private void OnCollisionEnter(Collision other) {
        if (CollidedIsProjectile()) {
            Damage(1);
        } else if (CollidedIsPlayer()) {
            //player.Damage();
        }
    }

    private bool CollidedIsProjectile() { //blocked by projectile implementation
        return false;
        
    }

    private bool CollidedIsPlayer() { //blocked by player implementaiton
        return false;
    }
}
