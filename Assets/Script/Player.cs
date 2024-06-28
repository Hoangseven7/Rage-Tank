using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float rotationSpeed;
    [SerializeField] GameObject bullet;
    [SerializeField] Transform firePoint;
    [SerializeField] float TimeFire = 0.1f;
    [SerializeField] float bulletForce;
    private float timeFrice;
    private Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }



    void Update()
    {
        float moveDirection = Input.GetAxis("Vertical");
        transform.Translate(Vector3.up * moveDirection * speed * Time.deltaTime);


        float turnDirection = Input.GetAxis("Horizontal");
        transform.Rotate(Vector3.back, turnDirection * rotationSpeed * Time.deltaTime);


        timeFrice -= Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.Space) && timeFrice < 0)
        {
            Shoot();

        }

    }

    private void Shoot()
    {

        timeFrice = TimeFire;
        GameObject bulletTmp = Instantiate(bullet, firePoint.position, Quaternion.identity);
        Rigidbody2D rb = bulletTmp.GetComponent<Rigidbody2D>();
        rb.AddForce(transform.up * bulletForce, ForceMode2D.Impulse);
    }
}
