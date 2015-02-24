using UnityEngine;
public class SimplePlayerController : BaseTopDownSpaceShip
{
    public BasePlayerManager myPlayManager;
    public BaseUserManager myDataManager;

    public override void Start()
    {
        // tell our base class to initialize
        base.Init();

        // now do our own initialize
        this.Init();
    }

    public override void Init()
    {
        // if a player manager is not set in the editor, let's try
        // to find one
        if (myPlayManager == null)
            myPlayManager = myGO.GetComponent<BasePlayerManager>();

        myDataManager = myPlayManager.DataManager;
        myDataManager.SetName("Player");
        myDataManager.SetHealth(3);

        didinit = true;
    }

    public override void Update()
    {
        // do the Update in our base 
        UpdateShip();

        // don't do anything until Init() has been run
        if (!didinit)
            return;

        // check to see if we're supposed to be controlling the
        // player before checking for firing
        if (!canControl)
            return;
    }
    public override void GetInput()
    {
        // we're onerriding the default input function to add in the 
        // ability to fire
        horizontal_input = default_input.GetHorizontal();
        vertical_input = default_input.GetVertical();
    }

    void OnCollisionEnter(Collision collider)
    {
        // react to collisions here
    }

    void OnTriggerEnter(Collider other)
    {
        // react to triggers here
    }

    public void PlayerFinished()
    {
        // Deal with the end of the game for this player
    }

    public void ScoredPoints(int howMany)
    {
        myDataManager.AddScore(howMany);
    }
}
