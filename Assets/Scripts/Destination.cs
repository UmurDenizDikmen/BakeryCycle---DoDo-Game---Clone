using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destination : MonoBehaviour
{
    public GameObject Customers;
    public Transform SpawnPoint;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Customers"))
        {
            GameControl.instance.DestinationSettings();
            GameObject obj = Instantiate(Customers, SpawnPoint.transform.position, SpawnPoint.transform.rotation);
            GameControl.instance.Customers.Add(obj);
        }
        
    }
}
