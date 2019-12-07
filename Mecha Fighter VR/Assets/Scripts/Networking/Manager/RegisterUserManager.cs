using UnityEngine;
using UnityEngine.UI;

public class RegisterUserManager : MonoBehaviour
{
    [Header("RESOURCES")]
    public Animator wrongAnimator;
    public Text usernameText;
    public Text emailText;
    public Text passwordText;
        
    private string username = "";
    private string password = "";
    private string email = "";
    private GameObject mainObject;
    private MessageQueue msgQueue;
    private ConnectionManager cManager;

    void Awake()
    {
        mainObject = GameObject.Find("MainObject");
        cManager = mainObject.GetComponent<ConnectionManager>();
        msgQueue = mainObject.GetComponent<MessageQueue>();
        msgQueue.AddCallback(Constants.SMSG_REGISTER, ResponseRegister);
    }

    private void Start()
    {
        if (cManager)
        {
            cManager.setupSocket();
        }
    }

    public void Register()
    {
        username = usernameText.text;
        password = passwordText.text;
        email = emailText.text;
        RequestRegister request = new RequestRegister();
        request.send(username, email, password);
        cManager.send(request);
    }

    public void ResponseRegister(ExtendedEventArgs eventArgs)
    {

    }
}

