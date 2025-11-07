using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ball : MonoBehaviour
{
    public Rigidbody2D rb;

    public static string difficulty = "Hard";

    // As default, let's assume difficulty is Normal.
    float startingSpeed = 4.75f;
    float speedIncrement = 1.2f;  // 20% increment with each bounce!
    int maxIncrements = 4;

    int incrementCount = 0;  // keeps track of how many speed increments have happened.
    float xVelocity;
    float yVelocity;

    // Start is called before the first frame update
    void Start()
    {
        switch (difficulty)  // check if difficulty is Hard or Nightmare - if so, adjust ball variables!
        {
            case "Hard":
                startingSpeed = 5.25f;
                speedIncrement = 1.15f;  // 15% increment with each bounce!
                maxIncrements = 9;
                break;
            case "Nightmare":
                startingSpeed = 6f;
                speedIncrement = 1.12f;  // A LOT OF 12% increments!
                maxIncrements = 100;
                // Nightmare Exclusive -> Bonus Ball!
                break;
        }

        bool isRight = UnityEngine.Random.value >= 0.5f;

        xVelocity = -1f;

        if (isRight)
            xVelocity = 1f;

        yVelocity = UnityEngine.Random.Range(-1,1);

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
            transform.position = new Vector3(0,0);  // teleports ball to the middle of the screen.
            rb.velocity *= -1;  // ball goes in opposite direction.
        }

        // checking win condition - player reaches 3 points
        if (P1.p1score > 2 || P2.p2score > 2)
            SceneManager.LoadScene("Results");  // a player has won the game, so we stop everything and load up the Results screen!
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
