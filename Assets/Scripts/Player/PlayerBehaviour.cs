using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerBehaviour : MonoBehaviour
{
    [Header("Player Stats")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float runSpeedMultiplier;
    [SerializeField] private float dashSpeed;
    [SerializeField] private float dashTime;

    [Header("Player Components")]
    [SerializeField] private Rigidbody2D _rigidbody2D;

    [Header("Player auxiliar variables")]
    [SerializeField] private Vector2 movement;
    [SerializeField] private Vector2 mousePosition;
    [SerializeField] private float speed;
    [SerializeField] private bool isDashing;

    void Awake()
    {
        _rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
        movement = new Vector2(0f, 0f);
    }

    void Update()
    {
        // Captura o input do movimento (WASD ou setas)
        movement.x = Input.GetAxis("Horizontal");
        movement.y = Input.GetAxis("Vertical");

        // Captura a posição do mouse na tela
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Adicionada ação de correr ao player
        speed = moveSpeed;
        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed *= runSpeedMultiplier;
        }

        // Adicionado dash
        if (Input.GetKey(KeyCode.Space))
        {
            StartCoroutine(Dash());
        }

        if (isDashing) return;

        var moveDir = transform.up * movement.y + transform.right * movement.x;
        Vector2 smoothMovement = speed * Time.deltaTime * moveDir;
        var desiredPosition = _rigidbody2D.position + smoothMovement;
        _rigidbody2D.MovePosition(desiredPosition);
    }

    void FixedUpdate()
    {
        var lookDir = mousePosition - _rigidbody2D.position;
        var angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        _rigidbody2D.rotation = angle;
    }

    IEnumerator Dash()
    {
        isDashing = true;
        var dashDirection = transform.up * movement.y + transform.right * movement.x;
        var startTime = Time.time;

        while (Time.time < startTime + dashTime)
        {
            Vector2 smoothMovement = dashSpeed * Time.deltaTime * dashDirection;
            var desiredPosition = _rigidbody2D.position + smoothMovement;
            _rigidbody2D.MovePosition(desiredPosition);

            yield return null;
        }

        isDashing = false;
    }
}
