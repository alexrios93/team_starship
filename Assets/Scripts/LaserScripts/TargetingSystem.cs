using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[WORK IN PROGRESS 4/23]
public class TargetingSystem : MonoBehaviour
{
    public List<Transform> targets;
    public Transform selectedTarget;

    private Transform myTransform;


    // Use this for initialization
    void Start()
    {
        targets = new List<Transform>();
        selectedTarget = null;
        myTransform = transform;

        AddAllEnemies();

    }


    public void AddAllEnemies()
    {
        GameObject[] go = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in go)
            AddTarget(enemy.transform);
    }

    public void AddTarget(Transform enemy)
    {
        targets.Add(enemy);
    }


    private void SortTargetsByDistance()
    {
        targets.Sort(delegate (Transform t1, Transform t2) {
            return Vector3.Distance(t1.position, myTransform.position).CompareTo(Vector3.Distance(t2.position, myTransform.position));
        });
    }

    //if we do not have an enemy targeted ywt, then find the clostest one and target him
    //if we do have an enemy targeted, then get the next target
    //if we have the last target in the list, then get then first target in the list
    private void TargetEnemy()
    {
        // remove all null transforms:
        while (targets.Remove(null)) ;

        if (selectedTarget == null)
        {
            SortTargetsByDistance();
            selectedTarget = targets[0];
        }
        else
        {
            int index = targets.IndexOf(selectedTarget);

            if (index < targets.Count - 1)
            {
                index++;
            }
            else
            {
                index = 0;
            }
            DeselectTarget();
            selectedTarget = targets[index];
        }
        SelectTarget();

    }

    private void SelectTarget()
    {
        selectedTarget.GetComponent<Renderer>().material.color = Color.red;

        TargetingLaser tl = (TargetingLaser)GetComponent("SeekingLaser");
        tl._enemy = selectedTarget.gameObject;

        //PlayerAttack pa = (PlayerAttack)GetComponent("PlayerAttack");
        //pa.target = selectedTarget.gameObject;
    }

    private void DeselectTarget()
    {
        selectedTarget.GetComponent<Renderer>().material.color = Color.white;
        selectedTarget = null;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
            TargetEnemy();
    }
}