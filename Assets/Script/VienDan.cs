using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VienDan : MonoBehaviour
{
    [SerializeField] float time;
    void Update()
    {
        Destroy(this.gameObject,time);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Tuong")
        {
            Destroy(gameObject);
        }

    }
}
