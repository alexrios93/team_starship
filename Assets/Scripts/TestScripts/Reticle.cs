using UnityEngine;
using System.Collections;
 
public class Reticle : MonoBehaviour
{
    public Vector3 TargetPosition = Vector3.zero;
    public Vector3 TargetDirection = Vector3.forward;
    public float ZPlane = 15.0f;
 
    private Camera mainCamera;
 
    void OnDrawGizmos()
    {
        Gizmos.DrawSphere(TargetPosition, 0.5f);
    }
    void OnGui()
    {
    }
 
    // Use this for initialization
    void Start ()
    {
        mainCamera = Camera.main;
    }
   
    // Update is called once per frame
    void Update ()
    {
        transform.rotation = Quaternion.LookRotation(-mainCamera.transform.forward, mainCamera.transform.up);
        Vector3 mousepos = Input.mousePosition;
        Plane pickplane = new Plane(transform.forward, transform.position);
        Debug.DrawLine(transform.position, transform.forward * 2);
       
        Ray pickray = mainCamera.ScreenPointToRay(mousepos);
        float hitdist = 0.0f;
 
        if(pickplane.Raycast(pickray, out hitdist))
        {
            TargetPosition = pickray.GetPoint(hitdist);
            TargetDirection = pickray.direction;
        }
        else
        {
            TargetPosition = TargetPosition;
        }
 
    }
}