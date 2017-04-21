using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//using static System.Net.Mime.MediaTypeNames;

public class PlayerBoost : MonoBehaviour {
    public RectTransform boostTransform;
    private float cachedY;
    private float minXValue;
    private float maxXValue;
    public int currentBoost;

    private int CurrentBoost
    {
        get { return currentBoost; }
        set
        {
            if (value >= 0)
            {
                currentBoost = value;
            }
            else
            {
                currentBoost = 0;
            }

            handleBoost();
        }
    }

    public int maxBoost;
    public Image visualBoost;
    public float coolDown;
    private bool onCD;

    public GameObject Explosion;
    private GameObject ExplosionFX;

    // Use this for initialization
    void Start () {
        cachedY = boostTransform.position.y;
        maxXValue = boostTransform.position.x;
        minXValue = boostTransform.position.x - (boostTransform.rect.width * 2);
        currentBoost = maxBoost;
        onCD = false;
    }
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButton("Left Thumb"))
        {
            if(!onCD && currentBoost < maxBoost)
            {
                StartCoroutine(CoolDownDmg());
                CurrentBoost += 1;
            }            
            //Debug.Log("Healing");
        }
        if (Input.GetButton("Right Thumb"))
        {
            if (!onCD && currentBoost > 0)
            {
                StartCoroutine(CoolDownDmg());
                CurrentBoost -= 1;
            }
            //Debug.Log("Hurting");
        }
	}


    private void handleBoost()
    {
        //boostTransform.text = "Health: " + currentBoost;
        float currentXValue = MapValues(currentBoost, 0, maxBoost, minXValue, maxXValue);
        boostTransform.position = new Vector3(currentXValue, cachedY);

        if (currentBoost > maxBoost  / 2)  // Greater than 50%
        {
            //visualBoost.GetComponent<Image>().color = new Color32(255, 125, (byte)MapValues(currentBoost, maxBoost / 2, maxBoost, 255, 0), 255);
            visualBoost.GetComponent<Image>().color = new Color32((byte)MapValues(currentBoost, maxBoost / 2, maxBoost, 0, 255), 0, 0, 255);
        }
        if (currentBoost <= 0) // If health reaches 0%
        {
            Explode();
            transform.Translate(Vector3.forward * Time.deltaTime * 0);
        }
        else //Less than 50%
        {
            //visualBoost.GetComponent<Image>().color = new Color32((byte)MapValues(currentBoost, 0, maxBoost / 2, 0, 125), 0, 255, 255);
            visualBoost.GetComponent<Image>().color = new Color32(0,0, (byte)MapValues(currentBoost, maxBoost / 2, maxBoost, 0, 255), 255);
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

    void Explode()
    {
        ExplosionFX = Instantiate(Explosion, this.transform.position, this.transform.rotation);
        ExplosionFX.transform.localScale = gameObject.transform.localScale;
        //Destroy(gameObject);
    }
}
