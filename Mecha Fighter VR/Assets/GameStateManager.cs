using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Stats;

public class GameStateManager : MonoBehaviour
{
    public float slowdownFactor = .005f;
    public float slowdownLength = 10f;
    private bool gameOver = false;
    private string winner;

    private int pointsPlayer = 0;
    private int pointsOpponent = 0;

    public AudioSource startSound;
    public AudioSource koSound;


    void Start()
    {
        startSound.Play();
    }
    void Update()
    {
        if (gameOver == true)
        {
            Time.timeScale += (1f / slowdownLength) * Time.unscaledDeltaTime;
            Time.timeScale = Mathf.Clamp(Time.timeScale, 0f, 1f);
        }
    }

    public void GameOver(string loser)
    {
        if (gameOver == false)
        {
            gameOver = true;
            string gameCase = "";
            if (loser != "Player Controller Root 2")
            {
                gameCase = "You Win!";
            }
            else
            {
                gameCase = "You Lose";
            }

            Time.timeScale = slowdownFactor;
            Time.fixedDeltaTime = Time.timeScale * .02f;
            print("game over");
            print(gameCase);
            Invoke("EndGame", slowdownLength);
        }
    }

    void EndGame()
    {
        koSound.Play();
        gameOver = false;
        Time.timeScale = 0;
    }

    public void addPoints(bool player, int points)
    {
        if (player)
        {
            pointsPlayer += points;
        }
        else
        {
            pointsOpponent += points;
        }
    }

    public int getPlayerPoints(bool player)
    {
        //if player is true, return the player's points, otherwise return the opponent's
        if (player)
        {
            return pointsPlayer;
        }
        else
        {
            return pointsOpponent;
        }
    }
}
