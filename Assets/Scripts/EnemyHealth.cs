using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour {
    public float healthPoint = 100f;
    public float currentHealthPoint;
    public GameObject Explosion;
    private GameObject ExplosionFX;
    //private GameObject[] _enemy;

    // Use this for initialization
    void Start () {
        //_enemy = GameObject.FindGameObjectsWithTag("Enemy");
        currentHealthPoint = healthPoint;
	}

    public void TakeDamage(float damage)
    {
        currentHealthPoint -= damage;
        if(currentHealthPoint <= 0)
        {
            currentHealthPoint = 0;
            Explode();
        }
    }
	
	// Update is called once per frame
	void Explode () {
        ExplosionFX = Instantiate(Explosion, this.transform.position, this.transform.rotation);
        ExplosionFX.transform.localScale = gameObject.transform.localScale;
        Destroy(gameObject);
	}
}
