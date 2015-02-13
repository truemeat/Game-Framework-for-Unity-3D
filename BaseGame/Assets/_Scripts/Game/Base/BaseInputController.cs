using UnityEngine;
using System.Collections;

public class BaseInputController : MonoBehaviour {

	//directional buttons
    public bool Up;
    public bool Down;
    public bool Left;
    public bool Right;

    //fire / action buttons
    public bool Fire1;

    //weapon slots
    public bool Sloat1;
    public bool Sloat2;
    public bool Sloat3;
    public bool Sloat4;
    public bool Sloat5;
    public bool Sloat6;
    public bool Sloat7;
    public bool Sloat8;
    public bool Sloat9;

    public float vert;
    public float horz;
    public bool shouldRespawn;

    public Vector3 TEMPVec3;
    private Vector3 zeroVector = new Vector3(0, 0, 0);

    public virtual void CheckInput()
    {
        //override with your own code to deal with input
        horz = Input.GetAxis("Horizontal");
        vert = Input.GetAxis("Vertical");
    }
}
