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

        //StartingPositions();

        rb2D = GetComponent<Rigidbody2D>();
        LaunchBall();

        //Vector2 weight = new Vector2(0, mass * gravity); //Weight calculation for ball
        //rb2D.AddForce(weight, ForceMode2D.Impulse);
    }

    // Update is called once per frame
    void Update()
    {       
        
    }
    void LaunchBall()
    {
        //Launch ball in random direction
        float x = Random.Range(0, 2);
        float y = Random.Range(-2, 2);

        Vector2 launch = new Vector2(x, y).normalized;
        rb2D.velocity = launch * appliedForce;
    }

    void StartingPositions()
    {
        //Ball starts at either player 1 or 2 positions
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
        if (collision.gameObject.CompareTag("Player"))
        {
            //Makes sure that the paddles applies a force 
            Vector2 paddle = collision.rigidbody.velocity;
            rb2D.velocity += paddle * appliedForce;
        }
    }
}
