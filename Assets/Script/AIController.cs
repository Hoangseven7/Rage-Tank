using System.Collections;
using UnityEngine;

public class AIController : MonoBehaviour
{
    [SerializeField] private Transform firePoint;      
    [SerializeField] private GameObject bullet;     
    [SerializeField] private float bulletForce = 10f;
    [SerializeField] private float attackRange = 5f;
    [SerializeField] private float fireRate = 5f;     

    private bool canFire = true;                    

    private void Update()
    {
        Vector3 playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
        float distanceToPlayer = Vector3.Distance(transform.position, playerPosition);

        if (distanceToPlayer <= attackRange && canFire)
        {
            StartCoroutine(FireRoutine());
        }
    }

    private IEnumerator FireRoutine()
    {
        canFire = false; 

 
        GameObject bulletTmp = Instantiate(bullet, firePoint.position, Quaternion.identity);
        Rigidbody2D rb = bulletTmp.GetComponent<Rigidbody2D>();


        rb.AddForce(transform.up * bulletForce, ForceMode2D.Impulse);

        yield return new WaitForSeconds(1f / fireRate); 

        canFire = true;
    }
}
