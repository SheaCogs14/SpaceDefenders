using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 2.0f;
    public float rotateSpeed = 200.0f;

    private float _horizontalInput;
    private float _verticalInput;

    private Rigidbody2D rb;

    [Header("Shooting Settings")]
    public float fireRate = 0.2f;
    private float _nextFireTime = 0.0f;
    public float fireSpeed = 15.0f;
    public float bulletDestroyTime = 5f;
    public int damage = 1;
    public GameObject normalBullet;
    public Transform barrel1;
    public Transform barrel2;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        GetPlayerInput();
        RotatePlayer();
        MovePlayer();

        if (Input.GetButton("Fire1") && Time.time >= _nextFireTime)
        {
            Shoot();
            _nextFireTime = Time.time + fireRate;  
        }
    }

    private void GetPlayerInput()
    {
        _horizontalInput = Input.GetAxisRaw("Horizontal");
        _verticalInput = Input.GetAxisRaw("Vertical");
    }

    private void RotatePlayer()
    {
        if (_horizontalInput != 0)
        {
            float rotationAmount = _horizontalInput * rotateSpeed * Time.deltaTime;
            transform.Rotate(Vector3.forward * rotationAmount);
        }
    }

    private void MovePlayer()
    {
        if (_verticalInput != 0)
        {
            rb.velocity = transform.up * moveSpeed * _verticalInput;
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
    }

    public void Shoot()
    {
        if (normalBullet != null && barrel1 != null && barrel2 != null)
        {
            GameObject newBullet1 = Instantiate(normalBullet, barrel1.position, transform.rotation);
            GameObject newBullet2 = Instantiate(normalBullet, barrel2.position, transform.rotation);

            Rigidbody2D bulletRb1 = newBullet1.GetComponent<Rigidbody2D>();
            Rigidbody2D bulletRb2 = newBullet2.GetComponent<Rigidbody2D>();

            if (bulletRb1 != null && bulletRb2 != null)
            {
                bulletRb1.velocity = transform.up * fireSpeed; 
                bulletRb2.velocity = transform.up * fireSpeed; 
            }

            Destroy(newBullet1, bulletDestroyTime);
            Destroy(newBullet2, bulletDestroyTime);
        }
    }
}
