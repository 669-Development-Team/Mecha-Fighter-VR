using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoginUserManager : MonoBehaviour
{
    public TextMesh usernameText;
    public TextMesh passwordText;

    private string username = "";
    private string password = "";
    private GameObject mainObject;
    private MessageQueue msgQueue;
    private ConnectionManager cManager;

    void Awake()
    {
        mainObject = GameObject.Find("Main Object");        
        cManager = mainObject.GetComponent<ConnectionManager>();
        msgQueue = mainObject.GetComponent<MessageQueue>();
        msgQueue.AddCallback(Constants.SMSG_LOGIN, ResponseLogin);
    }

    private void Start()
    {
        if (cManager)
        {
            cManager.setupSocket();
        }
    }

    public void Login()
    {
        username = usernameText.text.Trim();
        password = passwordText.text.Trim();
        RequestLogin request = new RequestLogin();
        request.send(username, password);
        cManager.send(request);
    }

    public void ResponseLogin(ExtendedEventArgs eventArgs)
    {
        ResponseLoginEventArgs args = eventArgs as ResponseLoginEventArgs;

        if (args.status == 0)
            NetworkManager.gameState = NetworkManager.GameState.LOBBY;
    }
}
