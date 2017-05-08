using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour {
    public float healthPoint = 100f;
    public float currentHealthPoint;
    public GameObject Explosion;
    public GameObject ScorePoints;
    private GameObject ExplosionFX;
    //private GameObject[] _enemy;

    private GameObject Camera; // Needed to add enemies to the list
    private VictoryOrDeath _enemyList;

    // Use this for initialization
    void Start () {
        //_enemy = GameObject.FindGameObjectsWithTag("Enemy");
        currentHealthPoint = healthPoint;

        Camera = GameObject.FindGameObjectWithTag("MainCamera");
        _enemyList = Camera.GetComponent<VictoryOrDeath>();
    }

    public void TakeDamage(float damage)
    {
        currentHealthPoint -= damage;
        if(currentHealthPoint <= 0)
        {
            currentHealthPoint = 0;
            GameObject SPoints = Instantiate(ScorePoints, transform.position, Quaternion.LookRotation(Camera.transform.position));
            //Instantiate(ScorePoints, transform);
            Explode();

        }
    }
	
	// Update is called once per frame
	void Explode () {
        ExplosionFX = Instantiate(Explosion, this.transform.position, this.transform.rotation);
        //ExplosionFX.transform.localScale = gameObject.transform.localScale;
        _enemyList.enemyList.Remove(1);    // Removes Enemy from the List
        Destroy(gameObject);
	}
}
