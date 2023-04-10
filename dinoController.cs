using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class dinoController : MonoBehaviour
{
    Rigidbody2D rb;
    public float jumpForce;
    float timer;
    public GameObject obstaclePrefab;
    public GameObject newObstacle;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        timer = 0;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
        timer += Time.deltaTime;
        if (timer >= 4)
        {
            newObstacle = Instantiate(obstaclePrefab, new Vector3(-59f, 0.31f, 0f), Quaternion.identity);

            timer = 0f;
        }
        var rb2 = newObstacle.AddComponent<Rigidbody2D>();
        newObstacle.AddComponent<BoxCollider2D>();
        newObstacle.tag="Obstacle";
        rb2.isKinematic = true;
        rb2.velocity = Vector3.left * 800 * Time.deltaTime;
        Destroy(newObstacle, 15f);


    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Obstacle"))
        {
            Debug.Log("öldü");
        }
    }
}
