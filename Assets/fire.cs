using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fire : MonoBehaviour
{
    [SerializeField] private GameObject bullet;

    [SerializeField] private Transform SpawnPoint;

    [SerializeField] private float bulletspeed=10f;

    public void FireBullet()
    {
        GameObject SpawnBullet = Instantiate(bullet, SpawnPoint.position, SpawnPoint.rotation);
        SpawnBullet.GetComponent<Rigidbody>().velocity = SpawnPoint.forward * bulletspeed;
        Destroy(SpawnBullet, 4f);
    }
}
