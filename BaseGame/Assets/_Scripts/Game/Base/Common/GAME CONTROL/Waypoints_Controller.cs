using System.Collections;
using UnityEngine;
public class Waypoints_Controller : MonoBehaviour
{
    [ExecuteInEditMode]

    // this script simle gives us a visual path to make it easier to edit
    // our waypoints
    private ArrayList transforms;   // arraylist for easy access to transforms
    private Vector3 firstPoint;     // store our first waypoint so we can loop the path
    private float distance;         // used to calculate distance between points

    private Transform TEMPtrans;    // a temporary holder for a transform
    private int TEMPindex;          // a temporary holder for an index number
    private int totalTransforms;

    private Vector3 diff;
    private float curDistance;
    private Transform closest;

    private Vector3 currentPos;
    private Vector3 lastPos;
    private Transform pointT;

    public bool closed = true;
    public bool shouldReverse;

    void Start()
    {
        // make sure that when this script starts that 
        // we have grabbed the transforms for each waypoint 
        GetTransforms();
    }

    void OnDrawGizmos()
    {
        // we only want to draw the waypoints when we're editing,
        // not when we are playing the game
        if (Application.isPlaying)
            return;

        GetTransforms();
        // make sure that we have more than one transform in the list,
        // otherwise we can't draw lines between them
        if (totalTransforms < 2)
        {
            return;
        }

        // draw our path first, we grab the position of the very
        // first waypoint so that our line has a start point
        TEMPtrans = (Transform)transforms[0];
        lastPos = TEMPtrans.position;

        // we point each waypoint at the next, so that we can use 
        // this rotation data to find out when the player is going
        // the wrong way or to position the player after a reset
        // facing the correct direction. So first we need to hold a 
        // reference to transform we are goning to point
        pointT = (Transform)transforms[0];

        // also, as this is the first point we store it to use for
        // closing the path later
        firstPoint = lastPos;

        // now we loop through all of the waypoints drawing lines between them
        for (int i = 0; i < transforms.Count; i++)
        {
            TEMPtrans = (Transform)transforms[i];
            if (TEMPtrans == null)
            {
                GetTransforms();
                return;
            }

            // grab the current waypoint position
            currentPos = TEMPtrans.position;

            Gizmos.color = Color.green;
            Gizmos.DrawSphere(currentPos, 2);

            // draw the line between the last point an this one
            Gizmos.color = Color.red;
            Gizmos.DrawLine(lastPos, currentPos);

            // point our last transform at the least position
            pointT.LookAt(currentPos);

            // update our 'lase' waypoint to become this one as we move on to find the next..
            lastPos = currentPos;

            // update the pointing transform
            pointT = (Transform)transforms[i];
        }

        //  close the path
        if (closed)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(currentPos, firstPoint);
        }

    }

    // reverse 反向
    public void SetReverseMode(bool rev)
    {
        shouldReverse = rev;
    }

    public void GetTransforms()
    {
        // we store all of the waypoints transforms in an Arraylist,
        // which is initailised here(we always need to do this
        // before we can use Arraylists)
        transforms = new ArrayList();

        // now we go through an transforms 'under' this transform,
        // so all of the child objects that act as our waypoints get
        // put into our arraylist
        foreach (Transform t in transform)
        {
            // add this transform to our arraylist 
            transforms.Add(t);
        }

        totalTransforms = (int)transforms.Count;    

    }

    //public int FindNearstWaypoint(Vector3 fromPos, float maxRange)
    //{
 
    //}
}
