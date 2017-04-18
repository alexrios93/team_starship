using UnityEngine;
using System.Collections;
 
public class FauxRailCamera : MonoBehaviour
{
    public float PitchSpeed = 5.0f;
    public float YawSpeed = 5.0f;
    public float RollSpeed = 5.0f;
 
    private Transform childFollow = null;
 
    void OnGUI()
    {
    }
 
    void Awake()
    {
        childFollow = transform.Find("Player");
    }
    // Use this for initialization
    void Start ()
    {
   
    }
   
    // Update is called once per frame
    void Update ()
    {
        Quaternion AddRot = Quaternion.identity;
        float roll = 0;
        float pitch = 0;
        float yaw = 0;
 
        roll = Input.GetAxis("Roll") * (Time.deltaTime * RollSpeed);
        pitch = Input.GetAxis("Pitch") * (Time.deltaTime * PitchSpeed);
        yaw = Input.GetAxis("Yaw") * (Time.deltaTime * YawSpeed);
 
        AddRot.eulerAngles = new Vector3(-pitch, yaw, -roll);
        transform.rotation *= AddRot;
    }
}