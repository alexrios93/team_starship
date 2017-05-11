using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MothershipHealth : MonoBehaviour {
    //public float healthPoint = 100f;
    //public float currentHealthPoint;

    public RectTransform mothershipHealth;
    private float cachedY;
    private float minXValue;
    private float maxXValue;
    public float currentHealth;

    public static bool MothershipBelowHalf = false;

    private float CurrentHealth
    {
        get { return currentHealth; }
        set
        {
            if (value >= 0)
            {
                currentHealth = value;
            }
            else
            {
                currentHealth = 0;
            }

            HandleHealth();
        }
    }

    public int maxHealth;
    public Image visualHealth;
    public float coolDown;
    private bool onCD;

    public GameObject Explosion;
    private GameObject ExplosionFX;
    //private GameObject[] _enemy;

    public GameObject ScorePoints;
    public int _scoreValue = 50;
    private GameObject Camera;

    public GameObject SpawnLocation;
    public GameObject Portal;
    private float LastPortalOpen = -25.0f;
    private float LastPortalClose = -15.0f;
    public GameObject Defender;
    private GameObject _player;
    private VictoryOrDeath _enemyList;
    
    // Use this for initialization
    void Start()
    {
        //_enemy = GameObject.FindGameObjectsWithTag("Enemy");
        cachedY = mothershipHealth.position.y;
        minXValue = mothershipHealth.position.x;
        maxXValue = mothershipHealth.position.x - (mothershipHealth.rect.width * 2);
        currentHealth = maxHealth;
        onCD = false;

        currentHealth = maxHealth;

        Camera = GameObject.FindGameObjectWithTag("MainCamera");

        Portal.GetComponent<ParticleSystem>().Stop();   // Needed for DefendMothership
        _player = GameObject.FindGameObjectWithTag("Player");
        _enemyList = Camera.GetComponent<VictoryOrDeath>();
    }

    public void TakeDamage(float damage)
    {
        if (!onCD && currentHealth > 0)
        {
            StartCoroutine(CoolDownDmg());
            CurrentHealth -= damage;
            //MotherShipDefense();
        }

        //currentHealth -= damage;
       // if (currentHealth <= 0)
       // {
            //print(currentHealthPoint);
       //     currentHealth = 0;
      //      Explode();
       // }
    }

    private void HandleHealth()
    {
        //healthTransform.text = "Health: " + currentHealth;
        float currentXValue = MapValues(currentHealth, 0, maxHealth, minXValue, maxXValue);
        mothershipHealth.position = new Vector3(currentXValue, cachedY);

        if (currentHealth <= 0) // If health reaches 0%
        {
            Points.ScoreOperator = "+";
            Points.ScoreValue = _scoreValue;
            GameObject SPoints = Instantiate(ScorePoints, transform.position, Quaternion.LookRotation(Camera.transform.position));
            ScoreManager.playerScore += _scoreValue;
            Explode();
        }

        if (currentHealth > maxHealth / 2)  // Greater than 50%
        {
            visualHealth.GetComponent<Image>().color = new Color32((byte)MapValues(currentHealth, maxHealth / 2, maxHealth, 109, 109), 109, 109, 255);
        }
        else //Less than 50%
        {
            visualHealth.GetComponent<Image>().color = new Color32(109, (byte)MapValues(currentHealth, 0, maxHealth / 2, 109, 109), 109, 255);
            MothershipBelowHalf = true;
        }
    }

    IEnumerator CoolDownDmg()
    {
        onCD = true;
        yield return new WaitForSeconds(coolDown);
        onCD = false;

    }

    private float MapValues(float x, float inMin, float inMax, float outMin, float outMax)
    {
        return (x - inMin) * (outMax - outMin) / (inMax - inMin) + outMin;
    }

    // Update is called once per frame
    void Explode()
    {
        ExplosionFX = Instantiate(Explosion, this.transform.position, this.transform.rotation);
        //ExplosionFX.transform.localScale = gameObject.transform.localScale;
        Destroy(gameObject);
    }
    
    /*
    void OpenPortal()
    {
        LastPortalOpen = Time.time;
        Portal.GetComponent<ParticleSystem>().Play();
    }

    void ClosePortal()
    {
        LastPortalClose = Time.time;
        Portal.GetComponent<ParticleSystem>().Stop();
    }
    
    void SpawnDefenders()
    {
        Vector3 RelativePos = _player.transform.position - transform.position;
        GameObject EnemyShip = Instantiate(Defender, SpawnLocation.transform.position, Quaternion.LookRotation(RelativePos));
        _enemyList.enemyList.Add(1); // Adds New Enemy to the List      



    }
    
    void MotherShipDefense()
    {
        if (30 < (Time.time - LastPortalOpen))
        {
            OpenPortal();
        }

        SpawnDefenders();

        if (30 < (Time.time - LastPortalClose))
        {
            ClosePortal();
        }
    }
    */
}
