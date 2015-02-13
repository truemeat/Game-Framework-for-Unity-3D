using UnityEngine;
using System.Collections;

public class BasePlayerManager : MonoBehaviour {

    public bool didInit;

    //the user manager an AI controllers are publically accessible so that
    //our individual control scripts can access them eadily
    public BaseUserManager DataManager;


    public virtual void Awake()
    {
        didInit = false;

        Init();
    }

    public virtual void Init()
    {
        DataManager = gameObject.GetComponent<BaseUserManager>();

        if (DataManager == null)
            DataManager = gameObject.GetComponent<BaseUserManager>();

        // do play init things in this function
        didInit = true;
    }

    public virtual void GameFinished()
    {
        DataManager.SetIsFinished(true);
    }

    public virtual void GameStart()
    {
        DataManager.SetIsFinished(false);
    }

}
