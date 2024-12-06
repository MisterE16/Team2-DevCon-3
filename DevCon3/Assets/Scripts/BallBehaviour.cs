using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallBehaviour : MonoBehaviour
{
    [SerializeField] public Rigidbody2D rb2D {  get; private set; }

    public Transform ballPos;
    public GameObject[] paddleObject;
    GameManager gameManager;

    [SerializeField] public Vector2[] startingPos {  get; private set; }

    //Physic calculation variables
    [SerializeField] private float gravity = 9.81f; 
    private float mass = 0.1f; //average tennis ball mass (kg)
    private float dragCoefficient = 0.55f; //Drag coefficient of a tennis ball, found on google search
    [SerializeField] public float appliedForce = 0.05f;
    private float bounceForce = 2f;
    [SerializeField] private float spinForce;
    [SerializeField] private float dragForce;
    private SpriteRenderer ballSprite;
    private float ballVelocity = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        ballPos = transform;
        gameManager = FindAnyObjectByType<GameManager>();

        spinForce = appliedForce;
        startingPos = new Vector2[]
        {
            new Vector2(paddleObject[0].transform.position.x + 2, paddleObject[0].transform.position.y),
            new Vector2(paddleObject[1].transform.position.x - 2, paddleObject[1].transform.position.y)
        };

        //StartingPositions();
        ballSprite = GetComponent<SpriteRenderer>();
        rb2D = GetComponent<Rigidbody2D>();
        CalculateForces();

        //Add gravity onto 
        Vector2 weight = new Vector2(0, mass * gravity); //Weight calculation for ball
        rb2D.AddForce(-weight, ForceMode2D.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        //At the start of the game either players 1 or 2 can start the game if they press the keys below
        //bool player1Start = Input.GetKey(KeyCode.E);
        //bool player2Start = Input.GetKey(KeyCode.LeftArrow);

        //if(player1Start)
        //{
        //    gameManager.currentState = GameManager.gameState.play;
        //    gameManager.GameStates(gameManager.currentState);
        //}

        //if (player2Start)
        //{
        //    gameManager.currentState = GameManager.gameState.play;
        //    gameManager.GameStates(gameManager.currentState);
        //}
    }

    void CalculateForces()
    {
        //Launch ball in random direction
        float x = 5;
        float y = 0;

        Vector2 launch = new Vector2(x, y).normalized;
        rb2D.velocity = launch * appliedForce;

        bool isLeft = rb2D.transform.position.x < 0;

        //Apply a torque force onto the ball
        float torque = spinForce;
        
        if (isLeft)
        {
            rb2D.AddTorque(-torque, ForceMode2D.Impulse);
        }
        else
        {
            rb2D.AddTorque(torque, ForceMode2D.Impulse);
        }
       

        //Calculate the area of the sprite
        float radius = ballSprite.sprite.bounds.extents.x;
        float area = Mathf.PI * Mathf.Pow(radius, 2);

        //Calculate the drag force
        dragForce = 0.5f * dragCoefficient * area * Mathf.Pow(ballVelocity, 2);
        Vector3 drag = -rb2D.velocity.normalized * dragForce;
        rb2D.AddForce(drag);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //Makes sure that the paddles applies a force
            Vector2 bounceAngle = new Vector2(3, 5).normalized;

            if(collision.transform.position.x > transform.position.x)
            {
                bounceAngle.x = -1.5f;
            }

            rb2D.velocity = bounceAngle * appliedForce;
        }

        if (collision.gameObject.CompareTag("Walls"))
        {
            //Reset the ball's position when it collides with the right or left walls
            if(collision.gameObject.name == "Left")
            {
                Vector2 startPosition = new Vector2(0, 0);
                transform.position = startPosition;
                gameManager.P2ScoreTracker();
            }
            if (collision.gameObject.name == "Right")
            {
                Vector2 startPosition = new Vector2(0, 0);
                transform.position = startPosition;
                gameManager.P1ScoreTracker();
            }
        }
    }
}
