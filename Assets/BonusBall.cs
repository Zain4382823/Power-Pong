using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BonusBall : MonoBehaviour
{
    public Rigidbody2D rb;

    // It's Bonus Ball, assume difficulty is Nightmare - NO SWITCH STATEMENT!
    float startingSpeed = 2.5f;
    float speedIncrement = 1.12f;  // Death by a THOUSAND increments!
    int maxIncrements = 100;

    int incrementCount = 0;  // keeps track of how many speed increments have happened.
    float xVelocity;
    float yVelocity;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Renderer>().enabled = false;  // at the start, make sure the ball is invisible!

        if (Ball.difficulty == "Nightmare")
            StartCoroutine(WaitAndSpawn());  // bonus ball only spawns in Nightmare mode...
        else
            rb.position = new Vector2(12,12); // If it's any other difficulty, hide bonus ball out of window!
    }

    IEnumerator WaitAndSpawn()  // wait a few seconds and then spawn the bonus ball!
    {
        yield return new WaitForSeconds(1f);
        SpawnBonusBall();
    }

    void SpawnBonusBall()
    {
        GetComponent<Renderer>().enabled = true;  // When spawned, turn the ball visible!
        bool isRight = UnityEngine.Random.value >= 0.5f;

        xVelocity = -1f;

        if (isRight)
            xVelocity = 1f;

        yVelocity = UnityEngine.Random.Range(-1, 1);

        if (yVelocity == 0f)
            yVelocity = 1f;  // Makes sure that ball never has 0 y velocity.

        rb.velocity = new Vector2(xVelocity * startingSpeed, yVelocity * startingSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        // check if the ball goes out of bounds...
        if (rb.position.x < -15 || rb.position.x > 15)
        {
            if (rb.position.x < -15)
                P2.p2score++;  // too far left = player 2 scores a point
            else
                P1.p1score++;  // too far right = player 1 scores a point

            rb.velocity = new Vector2(xVelocity * startingSpeed, yVelocity * startingSpeed);  // reset velocity.
            incrementCount = 0; // reset incrementCount to zero, treat it like a new ball!
            transform.position = new Vector3(0, 0);  // teleports ball to the middle of the screen.
            rb.velocity *= -1;  // ball goes in opposite direction.
        }
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Paddle")  // if the ball collides with a paddle...
        {
            rb.velocity *= -1;  // ball must get deflected NO MATTER WHAT.

            if (incrementCount < maxIncrements)  // also, if we haven't reached max increments yet...
            {
                rb.velocity *= speedIncrement;  // increment ball speed by speedIncrement!
                incrementCount++;
            }
        }
    }
}
