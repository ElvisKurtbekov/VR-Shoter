using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dummyTrigger : MonoBehaviour
{
    [SerializeField] private GameObject[] TargetDummies;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            foreach(GameObject dummies in TargetDummies)
            {
                dummies.GetComponent<trainingDummy>().ActivateDummy();
            }
        }
    }
}
