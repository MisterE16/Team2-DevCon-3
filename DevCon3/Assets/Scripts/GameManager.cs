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

    [SerializeField] private float startCountdown; //A countdown for when the game can start

    //Text mesh pro variables
    public TextMeshProUGUI player1ScoreText;
    public TextMeshProUGUI player2ScoreText;

    //Score tracking variables
    private int p1Score;
    private int player1CurrentScore = 0;
    private int p2Score;
    private int player2CurrentScore = 0;

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
        //currentState = gameState.pause;
        //GameStates(currentState);

        //Set the current score values to be equal to the current value
        p1Score = player1CurrentScore;
        p2Score = player2CurrentScore;
        startCountdown = 3;

        //As the game loads in, it will be in a paused game state
        //currentState = gameState.pause;
        //GameStates(currentState);
        UpdateUI();
    }

    // Update is called once per frame
    void Update()
    {
        SceneConditions();
    }

    //Defining game state enumerators for when this function is called
    //can change the game depending on the purpose
    public void GameStates(gameState state)
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
