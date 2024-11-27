using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallBehaviour : MonoBehaviour
{
    [SerializeField] private int playerNum;

    public Transform ballPos;
    public GameObject[] paddleObject;
    private Vector2[] startingPos;

    // Start is called before the first frame update
    void Start()
    {
        playerNum = Random.Range(1, 3);
        //startingPos = new Vector2[]
        //{
        //    new Vector2(paddleObject[0].transform.position.x + 2, paddleObject[0].transform.position.y),
        //    new Vector2(paddleObject[1].transform.position.x - 2, paddleObject[1].transform.position.y)
        //};
    }

    // Update is called once per frame
    void Update()
    {
        //StartingPositions();
        BallPhysics();
    }

    void BallPhysics()
    {

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
}
