using UnityEngine;
using UnityEngine.SceneManagement;

using System;
using System.Collections;
using System.Collections.Generic;


public class NetworkManager : MonoBehaviour
{
    private LevelManager levelManager;

    public enum GameState { LOBBY, LOADING_GAME, INGAME }
    public static GameState gameState { get; set; }

    //Determine what state the game is in
    private static ConnectionManager cManager;

    public static ConnectionManager GetConnectionManager()
    {
        return cManager;
    }

    void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();

        if (!levelManager)
            throw new UnityException("Level manager could not be found");

        gameState = GameState.LOBBY;

        gameObject.AddComponent<MessageQueue>();
        gameObject.AddComponent<ConnectionManager>();

        NetworkRequestTable.init();
        NetworkResponseTable.init();

        cManager = gameObject.GetComponent<ConnectionManager>();

        if (cManager)
        {
            cManager.setupSocket();
            Debug.Log("Connecting to server...");
            StartCoroutine(UpdateLoop(1f / Constants.updatesPerSecond));
        }
    }

    public IEnumerator UpdateLoop(float updateDelay)
    {
        yield return new WaitForSeconds(updateDelay);

        if (cManager)
        {
            switch (gameState)
            {
                case GameState.LOBBY:
                    RequestKeepAlive keepAlive = new RequestKeepAlive();
                    keepAlive.send();
                    cManager.send(keepAlive);
                    break;

                case GameState.LOADING_GAME:
                    while (SceneManager.GetActiveScene().name != "Sandbox")
                        yield return null;

                    gameState = GameState.INGAME;

                    break;

                case GameState.INGAME:                    
                    //Create the two request objects that will be sent to the server
                    RequestHeartbeat heartbeat = (RequestHeartbeat)NetworkRequestTable.get(Constants.CMSG_HEARTBEAT);
                    RequestPushUpdate pushUpdate = (RequestPushUpdate)NetworkRequestTable.get(Constants.CMSG_PUSHUPDATE);

                    //Create the messages to be sent
                    heartbeat.send();
                    pushUpdate.send();

                    //Send the messages
                    cManager.send(heartbeat);
                    cManager.send(pushUpdate);
                    break;

                default:
                    break;
            }
        }
        StartCoroutine(UpdateLoop(updateDelay));
    }
}