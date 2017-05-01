using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLaserDamage : MonoBehaviour {

    public float damage = 25.0f;

    // Update is called once per frame
    void OnCollisionEnter(Collision collision)
    {
        //print("i hit a " + collision.gameObject.tag);
        if (collision.gameObject.tag == "Satellite")
        {            
            collision.gameObject.SendMessage("TakeDamage", damage, SendMessageOptions.DontRequireReceiver);
            Destroy(gameObject);
        }
    }
}
