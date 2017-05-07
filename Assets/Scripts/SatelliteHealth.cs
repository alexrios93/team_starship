using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SatelliteHealth : MonoBehaviour {
    //public float healthPoint = 100f;
    //public float currentHealthPoint;

    public RectTransform satelliteHealth;
    private float cachedY;
    private float minXValue;
    private float maxXValue;
    public float currentHealth;

    public bool status = false;  // Allows for Pause & Play in ControlLayout

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

    public GameObject Camera;   //Use camera as dummy for SatelliteDown
    private VictoryOrDeath _satelliteDown;

    // Use this for initialization
    void Start()
    {
        //_enemy = GameObject.FindGameObjectsWithTag("Enemy");
        cachedY = satelliteHealth.position.y;
        minXValue = satelliteHealth.position.x;
        maxXValue = satelliteHealth.position.x - (satelliteHealth.rect.width * 2);
        currentHealth = maxHealth;
        onCD = false;

        currentHealth = maxHealth;

        _satelliteDown = Camera.GetComponent<VictoryOrDeath>();
    }

    public void TakeDamage(float damage)
    {
        if (status) // Needed for COntrolLayout
        {
            if (!onCD && currentHealth > 0)
            {
                StartCoroutine(CoolDownDmg());
                CurrentHealth -= damage;
            }

            //currentHealth -= damage;
            // if (currentHealth <= 0)
            // {
            //print(currentHealthPoint);
            //     currentHealth = 0;
            //      Explode();
            // }
            //print("satellite hit!  HP Left: " + currentHealthPoint);
        }
    }

    private void HandleHealth()
    {
        //healthTransform.text = "Health: " + currentHealth;
        float currentXValue = MapValues(currentHealth, 0, maxHealth, minXValue, maxXValue);
        satelliteHealth.position = new Vector3(currentXValue, cachedY);

        if (currentHealth > maxHealth / 2)  // Greater than 50%
        {
            visualHealth.GetComponent<Image>().color = new Color32((byte)MapValues(currentHealth, maxHealth / 2, maxHealth, 109, 109), 109, 109, 255);
        }
        if (currentHealth <= 0) // If health reaches 0%
        {
            //StartCoroutine(DeathCountDown()); // Taken to SatelliteDown script
            _satelliteDown.satelliteDown += 1;
            Explode();   
        }
        else //Less than 50%
        {
            visualHealth.GetComponent<Image>().color = new Color32(109, (byte)MapValues(currentHealth, 0, maxHealth / 2, 109, 109), 109, 255);
        }
    }

    IEnumerator CoolDownDmg()
    {
        onCD = true;
        yield return new WaitForSeconds(coolDown);
        onCD = false;
    }

    IEnumerator DeathSceneCountDown()
    {
        yield return new WaitForSeconds(2.5f);
        SceneManager.LoadScene("DeathScene");
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
        //Destroy(gameObject); 
        gameObject.transform.localScale = new Vector3(0, 0, 0);  //Instead of Destroying the Object, it just "disappears"       
    }
}