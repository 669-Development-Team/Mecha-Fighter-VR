using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using Stats;

public class GameStateManager : MonoBehaviour
{
    public float slowdownFactor = .005f;
    public float slowdownLength = 3f;
    public bool gameOver = false;
    private string winner;

    private int pointsPlayer = 0;
    private int pointsOpponent = 0;

    public AudioSource startSound;
    public AudioSource koSound;

    public static GameStateManager instance;

    public GameObject YouWinText;

    void Start()
    {
        instance = this;
        startSound.Play();
    }

    bool stopTime = false;

    void Update()
    {
        if (gameOver == true && !stopTime)
        {
            Time.timeScale += (1f / slowdownLength) * Time.unscaledDeltaTime;
            Time.timeScale = Mathf.Clamp(Time.timeScale, 0f, 1f);
            YouWinText.SetActive(true);
            Invoke("ReturnToLobby", 5f);
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
            Invoke("playKO", 1f);
        }
    }

    void EndGame()
    {
        gameOver = false;
        stopTime = true;
        Time.timeScale = 0;
    }
    
    private void playKO(){
        koSound.Play();
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

    public void ReturnToLobby()
    {
        int current = SceneManager.GetActiveScene().buildIndex;
        
        SceneManager.LoadScene("Lobby");
        SceneManager.UnloadSceneAsync(current);
    }
}
