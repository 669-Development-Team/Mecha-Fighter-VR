using UnityEngine;
using UnityEngine.UI;

public class RegisterUserManager : MonoBehaviour
{
    private GameObject mainObject;
    private MessageQueue msgQueue;
    private ConnectionManager cManager;

    void Awake()
    {
        mainObject = GameObject.Find("Main Object");
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

    public void Register(string username, string email, string password)
    {
        RequestRegister request = new RequestRegister();
        request.send(username, email, password);
        cManager.send(request);
    }

    public void ResponseRegister(ExtendedEventArgs eventArgs)
    {

    }
}

