using UnityEngine;
using System.Collections;
 
public class LocalFollowController : MonoBehaviour {
 
    private Reticle reticle;
 
    public float MoveSpeed = 10.0f;
    public float RotateSpeed = 10.0f;
 
    void OnGUI()
    {
    }
 
    // Use this for initialization
    void Start ()
    {
        reticle = GameObject.FindGameObjectWithTag("Reticle").GetComponent<Reticle>();
    }
   
    // Update is called once per frame
    void Update ()
    {
        Vector3 mousePos = reticle.TargetPosition;//reticle.transform.position;
 
        Vector3 aimdir = mousePos - transform.position;
        Quaternion Follow = Quaternion.LookRotation(reticle.TargetDirection, reticle.transform.up);
        transform.rotation = Follow;
 
        transform.position = Vector3.Lerp(transform.position, mousePos, Time.deltaTime * MoveSpeed);
    }
}