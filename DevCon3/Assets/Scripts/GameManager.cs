using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //Create game manager variable to reference itself
    public static GameManager manager;

    //Enumerator that keeps track of player turns
    public enum playerTurn {player1, player2};

    //A variable that keeps track of which turn to start
    public playerTurn currentTurn;

    private bool hasGameStarted = false;

    [SerializeField] private int randomStart;

    BallBehaviour ball;

    private void Awake()
    {
        //If the game manager doesn't exist, make this the game manager
        if(manager == null)
        {
            manager = this;

            //Make the game object persistent throughout play
            DontDestroyOnLoad(gameObject);
        }

        //Else destroy the game object if it already exists
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        ball = FindObjectOfType<BallBehaviour>(); //Reference the ball behaviour script

        //At the start of the game, randomly generate number
        randomStart = Random.Range(1, 3);

        //If the number generated is 1, its player 1's turn
        if(randomStart == 1)
        {
            currentTurn = playerTurn.player1;
        }

        //If the number generated is 2, its player 2's turn
        if (randomStart == 2)
        {
            currentTurn = playerTurn.player2;
        }
    }

    // Update is called once per frame
    void Update()
    {
        switch(currentTurn)
        {
            case playerTurn.player1:
                ball.ballPos.position = ball.startingPos[0];
                break;
            case playerTurn.player2:
                ball.ballPos.position = ball.startingPos[1];
                break;
        }
    }
}
