using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//using static System.Net.Mime.MediaTypeNames;

public class PlayerHealth : MonoBehaviour {
    public RectTransform healthTransform;
    private float cachedY;
    private float minXValue;
    private float maxXValue;
    public int currentHealth;

    public bool status = false;

    private int CurrentHealth
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
    private bool recover;
    private bool safe;

    public GameObject Explosion;
    private GameObject ExplosionFX;

    public AudioClip dangerSound;
    private AudioSource source;
    private float volLowRange = .5f;
    private float volHighRange = 1.0f;

    public GameObject Camera;
    private FadeScreen _fadeScreen;

    public GameObject ScorePoints;
    public int _scoreValue = 50;

    private GameObject enemyLaser;

    // Use this for initialization
    void Start () {
        cachedY = healthTransform.position.y;
        maxXValue = healthTransform.position.x;
        minXValue = healthTransform.position.x - (healthTransform.rect.width * 2);
        currentHealth = maxHealth;
        onCD = false;
        recover = false;
        safe = true;

        _fadeScreen = Camera.GetComponent<FadeScreen>();

        enemyLaser = GameObject.FindGameObjectWithTag("EnemyLaser");
    }
	
	// Update is called once per frame
	void Update () {
        //StartCoroutine(RecoverInterval());
        RecoverHealth();

    }

    void Awake()
    {
        source = gameObject.AddComponent<AudioSource>();
    }

    private void HandleHealth()
    {
        //healthTransform.text = "Health: " + currentHealth;
        float currentXValue = MapValues(currentHealth, 0, maxHealth, minXValue, maxXValue);
        healthTransform.position = new Vector3(currentXValue, cachedY);

        if (currentHealth > maxHealth  / 2)  // Greater than 50%
        {
            visualHealth.GetComponent<Image>().color = new Color32((byte)MapValues(currentHealth, maxHealth / 2, maxHealth, 255, 0), 255, 0, 255);
        }
        if (currentHealth <= 0) // If health reaches 0%
        {
            Points.ScoreOperator = "-";
            Points.ScoreValue = _scoreValue;
            GameObject SPoints = Instantiate(ScorePoints, transform.position + transform.forward * 35, Quaternion.LookRotation(Camera.transform.position));
            ScoreManager.playerScore -= _scoreValue;   // Deduct Value from ScoreManager

            Explode();
            _fadeScreen.fadeOut = true;
            StartCoroutine(DeathSceneCountDown());
        }
        else //Less than 50%
        {
            visualHealth.GetComponent<Image>().color = new Color32(255, (byte)MapValues(currentHealth, 0, maxHealth / 2, 0, 255), 0, 255);
        }        
    }

    IEnumerator CoolDownDmg()
    {
        onCD = true;
        yield return new WaitForSeconds(coolDown);
        onCD = false;
    }

    IEnumerator CoolDownHealth()
    {
        onCD = true;
        //yield return new WaitForSeconds(0.5f);
        yield return new WaitForSeconds(2.5f);
        onCD = false;
    }

    /*
    IEnumerator RecoverInterval()
    {
        recover = true;
        //yield return new WaitForSeconds(5.0f);
        if (safe == true && (currentHealth < maxHealth) && currentHealth > 0)
        {
            //StartCoroutine(CoolDownHealth()); //Depricated
            yield return new WaitForSeconds(2.5f);
            CurrentHealth += 1;
        };
        //RecoverHealth();
        recover = false;
    }
    */
  
    void RecoverHealth()
    {
        if (status)
        {
            {
                StartCoroutine(CoolDownHealth());
                //yield return new WaitForSeconds(2.5f);
                CurrentHealth += 1;
            }
            if (currentHealth == maxHealth)
            {
                currentHealth += 0;
            }
        }
    }
    

    IEnumerator DeathSceneCountDown()
    {
        yield return new WaitForSeconds(2.5f);
        SceneManager.LoadScene("DeathScene");
    }

    void OnTriggerEnter(Collider other)
    {
        if (status)
        {
            if (other.name == "Nebula")
            {
                safe = false;
            }

            if (other.tag == "EnemyLaser")
            {
                safe = false;
                if (!onCD && currentHealth > 0)
                {
                    StartCoroutine(CoolDownDmg());
                    CurrentHealth -= 5;

                    Points.ScoreOperator = "-";
                    Points.ScoreValue = 5;
                    GameObject SPoints = Instantiate(ScorePoints, transform.position + transform.forward * 35, Quaternion.LookRotation(Camera.transform.position));
                    ScoreManager.playerScore -= 5;  //Taking Damage Deducts Points
                }
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (status)
        {
            if (other.name == "Nebula")
            {
                safe = true;
            }
            if (other.tag == "EnemyLaser")
            {
                safe = true;
            }
        }
    }


    void OnTriggerStay(Collider other)
    {
        if (status)
        {
            if (other.name == "HealthTest")
            {
                if (!onCD && currentHealth < maxHealth)
                {
                    StartCoroutine(CoolDownDmg());
                    CurrentHealth += 1;
                }
                //Debug.Log("Healing");
            }
            //if (other.name == "DamageTest")
            if (other.name == "Nebula")
            {                
                if (!onCD && currentHealth > 0)
                {                         
                    StartCoroutine(CoolDownDmg());
                    CurrentHealth -= 1;
                    ScoreManager.playerScore -= 1;  //Taking Damage Deducts Points
                }        
            }
        }
    }     

    private float MapValues(float x, float inMin, float inMax, float outMin, float outMax)
    {
        return (x - inMin) * (outMax - outMin) / (inMax - inMin) + outMin;
    }

    void Explode()
    {
        ExplosionFX = Instantiate(Explosion, this.transform.position, this.transform.rotation);
        ExplosionFX.transform.localScale = gameObject.transform.localScale;
        //Destroy(gameObject);
        gameObject.transform.localScale = new Vector3(0, 0, 0);
    }
}
