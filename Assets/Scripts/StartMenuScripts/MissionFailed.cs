using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class MissionFailed : MonoBehaviour {
    public Button continueText;

    public AudioClip selectSound;
    public AudioClip scrollSound;
    private AudioSource source;
    private float volLowRange = .5f;
    private float volHighRange = 1.0f;
    private bool hasPlayed = false;

    // Use this for initialization
    void Start()
    {
        continueText.interactable = true;
        continueText.Select();
    }

    void Awake()
    {
        source = gameObject.AddComponent<AudioSource>();
    }

    public void ContinuePress()
    {
        float vol = Random.Range(volLowRange, volHighRange);
        source.PlayOneShot(selectSound, vol);

        SceneManager.LoadScene("StartMenu");
    }

    public void Update()
    {
        if (Input.GetAxis("Horizontal") > 0 || Input.GetAxis("Horizontal") < 0 || Input.GetAxis("Vertical") > 0 || Input.GetAxis("Vertical") < 0)
        {
            //print(Input.GetAxis("Horizontal"));
            if (!hasPlayed)
            {
                float vol = Random.Range(volLowRange, volHighRange);
                source.PlayOneShot(scrollSound, vol);
                hasPlayed = true;
            }
        }
        else if (Input.GetAxis("Horizontal") == 0 || Input.GetAxis("Vertical") == 0)
        {
            hasPlayed = false;
        }
    }

}
