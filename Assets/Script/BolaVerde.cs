using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BolaVerde : MonoBehaviour
{
    public float speed = 30f;
    private Vector3 direction;
    private Rigidbody2D rb;

    [SerializeField] GameObject RedBall1;
    [SerializeField] GameObject RedBall2;
    public float RedbRangeX = 9f;
    public float RedbRangeY = 4f;

    void Start()

    {
        rb = GetComponent<Rigidbody2D>();
        direction = Random.insideUnitSphere.normalized;
    }

    void Update()
    {
        rb.transform.position += direction.normalized * 2 * Time.deltaTime;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "wall") // balls reflect on the walls
        {
            direction = Vector3.Reflect(direction, collision.GetContact(0).normal);

        }
        else if (collision.gameObject.CompareTag("Player"))
        {
            // calls in the funcion to change direction and spawn red balls
            ballCollision();
        }
    }

    private void ballCollision()
    {
        //  generates a new position inside the unity circle while setting the z coordinate to 0
        Vector2 newPosition = new Vector2(Random.Range(-10f, 10f), Random.Range(-4f, 4f));
        transform.position = newPosition;

        transform.position = newPosition; // changes the ball position to a new random position

        // red ball spawner
        ObjectSpawner();

        ScoreManager.Instance.IncreasePoints();

        Camera.main.backgroundColor = Random.ColorHSV();
    }

    private void ObjectSpawner()
    {
        int rSpawn = Random.Range(0, 2); // chooses the prefabs from the value 0 to 1
        float rSpawnX = Random.Range(-RedbRangeX, RedbRangeX);
        float rSpawnY = Random.Range(-RedbRangeY, RedbRangeY);
        Vector2 spawnpos = new Vector2(rSpawnX, rSpawnY);

        if (rSpawn == 0)
        {
            Instantiate(RedBall1, spawnpos, Quaternion.identity);
        }
        else
        {
            Instantiate(RedBall2, spawnpos, Quaternion.identity);

        }
    }




}

