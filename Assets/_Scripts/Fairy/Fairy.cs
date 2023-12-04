using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Fairy : MonoBehaviour {
    private new Rigidbody2D rigidbody;
    private Vector2 direction;
    private Vector2 spawnPosiiton;
    [SerializeField] private float speed;

    private void Awake() {
        rigidbody = GetComponent<Rigidbody2D>();
        spawnPosiiton = transform.position;
    }

    // Update is called once per frame
    void Update() {
        direction.x = Input.GetAxis("Horizontal");
        direction.y = Input.GetAxis("Vertical");
    }

    private void FixedUpdate() {
        Vector2 position = rigidbody.position;
        position += direction.normalized * speed * Time.fixedDeltaTime;
        rigidbody.MovePosition(position);
    }

    public void Respawn() {
        transform.position = spawnPosiiton;
        gameObject.SetActive(true);
    }
}
