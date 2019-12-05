using UnityEngine;
using UnityEngine.UI;

public class JoinGameManager : MonoBehaviour
{
    private GameObject mainObject;
    private MessageQueue msgQueue;
    private ConnectionManager cManager;
    private LevelManager levelManager;

    void Awake()
    {
        mainObject = GameObject.Find("Main Object");
        cManager = mainObject.GetComponent<ConnectionManager>();
        msgQueue = mainObject.GetComponent<MessageQueue>();
        levelManager = mainObject.GetComponent<LevelManager>();
        msgQueue.AddCallback(Constants.SMSG_JOINGAME, ResponseJoinGame);
    }

    private void Start()
    {
        if (cManager)
        {
            cManager.setupSocket();
        }
    }

    public void JoinGame()
    {        
        RequestJoinGame request = new RequestJoinGame();
        request.send();
        cManager.send(request);
    }

    public void ResponseJoinGame(ExtendedEventArgs eventArgs)
    {         
        ResponseJoinGameEventArgs args = eventArgs as ResponseJoinGameEventArgs;
        NetworkManager.gameState = NetworkManager.GameState.LOADING_GAME;
        levelManager.loadScene("Sandbox");
    }
}