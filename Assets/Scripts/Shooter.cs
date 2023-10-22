using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField] GameObject bullet;
    [SerializeField] List<GameObject> projectilePool = new List<GameObject>();
    [SerializeField] int poolSize = 20;

    [SerializeField] Transform trashLocation;
    [SerializeField] Transform bulletSpawn;



    void Start()
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject bulletProjectile = Instantiate(bullet,transform.position, Quaternion.identity, trashLocation);
            projectilePool.Add(bulletProjectile);
            bulletProjectile.SetActive(false);
        }
    }

    void Update()
    {
        Fire();
    }

    void Fire()
    {
        if(Input.GetMouseButtonDown(0)) 
        {
            GameObject bullet = GetPoolProjectile();
            bullet.transform.position = bulletSpawn.position;
            bullet.transform.rotation = transform.rotation;
            bullet.SetActive(true);
        }
    }

    GameObject GetPoolProjectile()
    {
        for(int i = 0; i < projectilePool.Count; i++)
        {
            if (!projectilePool[i].activeInHierarchy)
            {
                return projectilePool[i];
            }
        }
        return null;
    }
}
