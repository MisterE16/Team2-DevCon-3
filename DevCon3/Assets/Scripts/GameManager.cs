using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    //Create game manager variable to reference itself
    public static GameManager manager;

    //Enumerator that keeps track of player turns
    public enum playerTurn {player1, player2};

    //Enumerator that is holding two different game states
    public enum gameState { pause, play};

    //A variable that keeps track of which turn to start
    public playerTurn currentTurn;

    //Keep track of what the current game state is
    public gameState currentState;

    private bool hasGameStarted = false;

    [SerializeField] private float startCountdown; //A countdown for when the game can start

    //Text mesh pro variables
    public TextMeshProUGUI player1ScoreText;
    public TextMeshProUGUI player2ScoreText;
    public TextMeshProUGUI countdownText;

    //Score tracking variables
    private int p1Score;
    private int player1CurrentScore = 0;
    private int p2Score;
    private int player2CurrentScore = 0;

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
        //currentState = gameState.pause;
        //GameStates(currentState);

        //Set the current score values to be equal to the current value
        p1Score = player1CurrentScore;
        p2Score = player2CurrentScore;
        startCountdown = 3;

        //StartCoroutine(StartCountdown());
        UpdateUI();
    }

    // Update is called once per frame
    void Update()
    {
        SceneConditions();
        //StartGame();
    }

    //At the start of the game there will be a countdown 
    //before either player can hit the start button
    //sending the ball in a direction depending on who hit first
    void StartGame()
    {
        bool player1Start = Input.GetKeyDown(KeyCode.Tab);
        bool player2Start = Input.GetKeyDown(KeyCode.Backspace);

        //Properly check input and prevent multiple coroutine calls
        if (!hasGameStarted && (player1Start || player2Start))
        {
            hasGameStarted = true; //Prevent multiple countdowns from happening at  once
            StartCoroutine(StartCountdown());            
        }
    }

    IEnumerator StartCountdown()
    {
        
        hasGameStarted = false;
        currentState = gameState.pause;
        GameStates(currentState);

        //Loop until countdown is 0
        while(startCountdown > 0)
        {
            Debug.Log($"Countdown: {startCountdown}");
            yield return new WaitForSeconds(1f); //Wait 1 second between each number
            startCountdown--;
        }

        hasGameStarted = true;        
        currentState = gameState.play;
        GameStates(currentState);

        startCountdown = 3;
    }

    void GameStates(gameState state)
    {
        switch (currentState)
        {
            case gameState.pause:
                Time.timeScale = 0;
                break;
            case gameState.play: 
                Time.timeScale = 1;
                break;
        }
    }

    void UpdateUI()
    {
        //Make text values that will translate to the score value on the UI
        player1ScoreText.text = "P1 Score: " + p1Score.ToString();
        player2ScoreText.text = "P2 Score: " + p2Score.ToString();

        //Turn the countdown value into text
        countdownText.text = "Start in: " + startCountdown.ToString();
    }

    //If this function is called, add a point then update UI
    public void P1ScoreTracker()
    {
        p1Score++;
        UpdateUI();
    }

    //If this function is called, add a point then update UI
    public void P2ScoreTracker()
    {
        p2Score++;
        UpdateUI();
    }

    public void Countdown()
    {
        while(startCountdown > 0)
        {
            startCountdown--;
        }        
    }

    void SceneConditions()
    {
        //If the r key is pressed then the scene resets
        //Only for occasions where if something unexpected happens 
        if(Input.GetKey(KeyCode.R))
        {
            SceneManager.LoadScene("Gameplay");
        }

        //If either player 1 or player 2's score goes above 10 
        //Load the win screen for either that reaches a score of 10 first
        if(p1Score >= 10)
        {
            SceneManager.LoadScene("Player1 Wins");
        }

        if (p2Score >= 10)
        {
            SceneManager.LoadScene("Player2 Wins");
        }
    }
}
