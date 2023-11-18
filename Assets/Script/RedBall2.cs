using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedBall2 : MonoBehaviour
{
    private GameObject player;
    [SerializeField] float speed = 8f;
    Rigidbody2D rb;
    private Vector2 direction;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        direction = Random.insideUnitCircle.normalized;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        rb.velocity = direction * speed;
    }

    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            return; 
        }
        else
        {
            CollisionController(collision);
        }
    }

    private void CollisionController (Collision2D collision)
    {

        if (collision.gameObject.CompareTag("wall"))
        {
            Vector2 playerPosition = player.transform.position;
            direction = (playerPosition - (Vector2)transform.position).normalized;
        }
        else if (collision.gameObject.CompareTag("GreenBall"))
        {
            Vector2 playerPosition = player.transform.position;
            direction = (playerPosition - (Vector2)transform.position).normalized;
        }
        else if (collision.gameObject.CompareTag("RedBall"))
        {
            Vector2 playerPosition = player.transform.position;
            direction = (playerPosition - (Vector2)transform.position).normalized;
        }
    }
}

