using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BulletManager : MonoBehaviour
{
    [Header("Bullet Pool Properties")] 
    public int poolSize;
    public GameObject bulletPrefab;
    public Transform bulletParent;

    private Queue<GameObject> bulletPool;

    void Awake()
    {
        bulletPrefab = Resources.Load<GameObject>("Prefabs/Bullet");
        bulletParent = GameObject.Find("[BULLETS]").transform;
        bulletPool = new Queue<GameObject>(); // create an empty container
    }

    void Start()
    {
        BuildBulletPool();
    }

    /// <summary>
    ///  Builds the Bullet Pool
    /// </summary>
    private void BuildBulletPool()
    {
        for (var i = 0; i < poolSize; i++)
        {
            bulletPool.Enqueue(CreateBullet());
        }
    }

    /// <summary>
    ///  Creates a Bullet
    /// </summary>
    /// <returns></returns>
    private GameObject CreateBullet()
    {
        var bullet = Instantiate(bulletPrefab, Vector2.zero, Quaternion.identity, bulletParent);
        bullet.SetActive(false);
        return bullet;
    }

    /// <summary>
    /// Gets a bullet from the pool. Sets to Active
    /// </summary>
    /// <param name="position"></param>
    /// <returns></returns>
    public GameObject GetBullet(Vector2 position)
    {
        if (bulletPool.Count < 1)
        {
            bulletPool.Enqueue(CreateBullet());
        }

        var bullet = bulletPool.Dequeue();
        bullet.SetActive(true);
        bullet.transform.position = position;
        bullet.GetComponent<BulletController>().Activate();
        return bullet;
    }

    /// <summary>
    /// Returns a bullet back to the bullet pool.
    /// Also resets values and sets to inactive.
    /// </summary>
    /// <param name="bullet"></param>
    public void ReturnBullet(GameObject bullet)
    {
        bullet.SetActive(false);
        bullet.transform.position = Vector2.zero;
        bullet.transform.rotation = Quaternion.identity;
        bullet.GetComponent<BulletController>().direction = Vector2.zero;
        bulletPool.Enqueue(bullet);
    }
}
