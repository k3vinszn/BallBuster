using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RedBall : MonoBehaviour
{
    [SerializeField] float speed = 8f;
    Rigidbody2D rb;
    private Vector2 direction;
    private GameObject player;

   void Start()
    {
        direction = Random.insideUnitCircle.normalized;
        rb = GetComponent<Rigidbody2D>();
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
            direction = Vector2.Reflect(direction, collision.contacts[0].normal);
        }
        if (collision.gameObject.CompareTag("RedBall"))
        {
            Vector2 playerPosition = player.transform.position;
            direction = (playerPosition - (Vector2)transform.position).normalized;
        }
        if (collision.gameObject.CompareTag("GreenBall"))
        {
            Vector2 playerPosition = player.transform.position;
            direction = (playerPosition - (Vector2)transform.position).normalized;
        }
        if (collision.gameObject.CompareTag("RedBall2"))
        {
            Vector2 playerPosition = player.transform.position;
            direction = (playerPosition - (Vector2)transform.position).normalized;
        }
    }
}