using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallBehaviour : MonoBehaviour
{
    [SerializeField] private int playerNum;
    [SerializeField] public Rigidbody2D rb2D {  get; private set; }
    [SerializeField] public KeyCode launchKey { get; private set; } = KeyCode.Space;

    private bool isLaunched;

    public Transform ballPos;
    public GameObject[] paddleObject;
    private Vector2[] startingPos;

    //Physic calculation variables
    [SerializeField] private float gravity = 9.81f; 
    private float mass = 0.0594f; //average tennis ball mass (kg)
    private float frictionCoefficient = 0.8f; //friction coefficient assuming level is made with hard court material
    [SerializeField] public float appliedForce = 0.5f;
    private float bounceForce = 2f;

    // Start is called before the first frame update
    void Start()
    {
        isLaunched = false;
        playerNum = Random.Range(1, 3);
        startingPos = new Vector2[]
        {
            new Vector2(paddleObject[0].transform.position.x + 2, paddleObject[0].transform.position.y),
            new Vector2(paddleObject[1].transform.position.x - 2, paddleObject[1].transform.position.y)
        };

        StartingPositions();

        rb2D = GetComponent<Rigidbody2D>();

        Vector2 weight = new Vector2(0, mass * gravity); //Weight calculation for ball
        rb2D.AddForce(weight, ForceMode2D.Impulse);
    }

    // Update is called once per frame
    void Update()
    {       
        if (Input.GetKey(launchKey))
        {            
            BallPhysics();
        }
    }

    void BallPhysics()
    {
        //Apply force to ball 
        Vector2 applyForce = new Vector2(appliedForce, 0);
        rb2D.AddForce(applyForce * Time.deltaTime, ForceMode2D.Impulse);
    }

    void StartingPositions()
    {
        //int randomNum = playerNum;

        //switch (randomNum)
        //{
        //    case 0:
        //        ballPos.position = paddleObject[0].transform.position;
        //        break;
        //    case 1:
        //        ballPos.position = paddleObject[1].transform.position;
        //        break;
        //}

        if(playerNum == 1)
        {
            ballPos.position = startingPos[0];
        }
        else
        {
            ballPos.position = startingPos[1];
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Walls"))
        {
            // Get the collision normal (direction perpendicular to the paddle's surface)
            Vector2 collisionNormal = collision.GetContact(0).normal;

            // Reverse the velocity along the normal
            Vector2 newVelocity = Vector2.Reflect(rb2D.velocity, collisionNormal).normalized * bounceForce;

            // Apply the new velocity to the ball
            rb2D.velocity = newVelocity;
        }
    }
}
