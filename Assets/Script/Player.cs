using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{

    float horizontal;
    float vertical;
    public float speed = 6f;
    Rigidbody2D rb;
    public string sceneLoader;
    [SerializeField] float deathDelay = 1f;
    [SerializeField] float sceneTimer = 30f;
    private bool playerDeath = false;
    private Vector3 initialScale;
    private Animator anime;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anime = GetComponent<Animator>();
        initialScale = transform.localScale;
    }
    private void Update()
    {
        // stops the movement when the player dies
        if (playerDeath) return; 

        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        sceneTimer -= Time.deltaTime;

        if ( sceneTimer <= 0.0f)
        {
            SceneSwap();
        }

        bool walk = Mathf.Abs(horizontal) > 0.1f || Mathf.Abs(vertical) > 0.1f;
        anime.SetBool("walk", walk);

        if(horizontal<0)
        {
            transform.localScale = new Vector3(-initialScale.x, initialScale.y, initialScale.z);
        }
        else if (horizontal > 0)
        {
            transform.localScale = initialScale;
        }
    }

    private void FixedUpdate()
    {

        if (playerDeath) return; // repeats the same logic where the players stops moving when he dies
        Vector2 pos = rb.position;
        pos.x += speed * horizontal * Time.deltaTime;
        pos.y += speed * vertical * Time.deltaTime;
        rb.MovePosition(pos);
    }


    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("RedBall") || collision.gameObject.CompareTag("RedBall2"))
        {
            StartCoroutine(DelayedScene());
            anime.SetTrigger("dead");
        }
    }
    void SceneSwap()
    {
        Vector3 playerPos = GameObject.FindGameObjectWithTag("Player").transform.position;
        SceneManager.LoadScene(sceneLoader);

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            player.transform.position = playerPos;
        }
    }
    IEnumerator DelayedScene()
    {
        //verifies if the player is dead
        playerDeath = true; 
        yield return new WaitForSeconds(deathDelay);
        SceneManager.LoadScene("GameOver");
    }
}
   