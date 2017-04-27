using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//using static System.Net.Mime.MediaTypeNames;

public class PlayerHealth : MonoBehaviour {
    public RectTransform healthTransform;
    private float cachedY;
    private float minXValue;
    private float maxXValue;
    public int currentHealth;

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

    public GameObject Explosion;
    private GameObject ExplosionFX;

    public AudioClip dangerSound;
    private AudioSource source;
    private float volLowRange = .5f;
    private float volHighRange = 1.0f;

    // Use this for initialization
    void Start () {
        cachedY = healthTransform.position.y;
        maxXValue = healthTransform.position.x;
        minXValue = healthTransform.position.x - (healthTransform.rect.width * 2);
        currentHealth = maxHealth;
        onCD = false;
    }
	
	// Update is called once per frame
	void Update () {
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
            Explode();
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

    void OnTriggerStay(Collider other)
    {
        if (other.name == "HealthTest")
        {
            if(!onCD && currentHealth < maxHealth)
            {
                StartCoroutine(CoolDownDmg());
                CurrentHealth += 1;
            }            
            //Debug.Log("Healing");
        }
        //if (other.name == "DamageTest")
        if (other.name == "Nebula")
        {
            //float vol = Random.Range(volLowRange, volHighRange);
            //source.PlayOneShot(dangerSound, vol);
            source.Play();

            if (!onCD && currentHealth > 0)
            {
                StartCoroutine(CoolDownDmg());
                CurrentHealth -= 1;
            }
            //Debug.Log("Hurting");
        }
        else
        {
            source.Stop();
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
        Destroy(gameObject);
    }
}
