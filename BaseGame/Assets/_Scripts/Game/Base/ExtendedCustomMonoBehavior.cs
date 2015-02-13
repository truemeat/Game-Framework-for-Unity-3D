using UnityEngine;
using System.Collections;

public class ExtendedCustomMonoBehavior : MonoBehaviour {

	//this class is used to add some common variables to
    //MonoBehavior, rather than constantly repeating
    //this same dexlarations in every class

    public Transform myTransform;
    public GameObject myGO;
    public Rigidbody muBody;

    public bool didinit;
    public bool canControl;

    public int id;

    [System.NonSerialized]
    public Vector3 tempVEC;

    [System.NonSerialized]
    public Transform tempTR;

    public virtual void SetID(int anID)
    {
        id = anID;
    }
}
