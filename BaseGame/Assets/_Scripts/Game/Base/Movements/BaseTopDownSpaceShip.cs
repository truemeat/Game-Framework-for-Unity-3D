﻿
using UnityEngine;

public class BaseTopDownSpaceShip : ExtendedCustomMonoBehavior
{
    private Quaternion targetRotation;

    private float thePos;
    private float moveXAmount;
    private float moveZAmount;

    public float moveXSpeed = 40f;
    public float moveZSpeed = 15f;

    public float limitX = 15f;
    public float limitZ = 15f;

    private float originZ;

    [System.NonSerialized]
    public Keyboard_Input default_input;

    public float horizontal_input;
    public float vertical_input;

    public virtual void Start()
    {
        // we are overriding Start() so as to call Init, as we
        // want the game controller to do this in this game.
        didinit = false;

        this.Init();
    }

    public virtual void Init()
    {
        // cache refs to our transform and gameObject
        myTransform = this.transform;
        myGO = this.gameObject;
        myBody = rigidbody;

        // add default keyboard input
        default_input = myGO.AddComponent<Keyboard_Input>();
        // grap the starting Z position to useas a baseline for Z
        // position limiting
        originZ = myTransform.localPosition.z;

        // set a flag so that our update function knows when we are Ok To use
        didinit = true;
    }

    public virtual void GameStart()
    {
        // we are good to go, so let's get moving
        canControl = true;
    }

    public virtual void GetInput()
    {
        // this is just a 'default' function that (if needs be) should 
        // be override in the glue code
        horizontal_input = default_input.GetHorizontal();
        vertical_input = default_input.GetVertical();
    }

    public virtual void Update()
    {
        UpdateShip();
    }

    public virtual void UpdateShip()
    {
        // don't do anthing until Init() has been run
        if (!didinit)
            return;

        // check to see if we're supposed to be controlling the player
        // before moving it
        if (!canControl)
            return;

    GetInput();

        // calculate movement amounts for X and Z axis
        moveXAmount = horizontal_input * Time.deltaTime * moveXSpeed;
        moveZAmount = vertical_input * Time.deltaTime * moveZSpeed;

        Vector3 tempRotation = myTransform.eulerAngles;
        tempRotation.z = horizontal_input * -30f;
        myTransform.eulerAngles = tempRotation;

        // move our transform to its updated position
        myTransform.localPosition += new Vector3(moveXAmount, 0, moveZAmount);

        // check the position to make sure that it is within boundaries
        if (myTransform.localPosition.x <= -limitX || myTransform.localPosition.x >= limitX)
        {
            thePos = Mathf.Clamp(myTransform.localPosition.x, -limitX, limitX);
            myTransform.localPosition = new Vector3(thePos, myTransform.localPosition.y, myTransform.localPosition.z);
        }

        if (myTransform.localPosition.z <= originZ || myTransform.localPosition.z >= limitZ)
        {
            thePos = Mathf.Clamp(myTransform.localPosition.z, originZ, limitZ);
            myTransform.localPosition = new Vector3(myTransform.localPosition.x, myTransform.localPosition.y, thePos);
        }

    }
}

