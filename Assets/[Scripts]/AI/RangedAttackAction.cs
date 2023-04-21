using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedAttackAction : MonoBehaviour, Action
{
    public bool hasLOS;
    [Range(1, 100)] 
    public int fireDelay = 20;
    public Transform bulletSpawn;
    public BulletManager bulletManager;

    void Start()
    {
        bulletManager = FindObjectOfType<BulletManager>();
    }

    void Update()
    {
        hasLOS = GetComponent<PlayerDetection>().LOS;
    }

    void FixedUpdate()
    {
        if (hasLOS && Time.frameCount % fireDelay == 0)
        {
            Execute();
        }
    }

    public void Execute()
    {
        var bullet = bulletManager.GetBullet(bulletSpawn.position);
    }
}
