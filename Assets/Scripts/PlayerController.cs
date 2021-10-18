using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private ObjectPool bulletPool;
    private float shootInterval = 0;

    
    void Start()
    {
        bulletPool = StageController.I.playerBulletPool;
    }

    
    void Update()
    {
        shootInterval -= Time.deltaTime;
    }

    public void Move(Vector3 _moveVec)
    {
        transform.Translate(_moveVec * 3 * Time.deltaTime);
        Vector3 nowPos = transform.localPosition;
        nowPos.x = Mathf.Clamp(nowPos.x, -2.5f, 2.5f);
        nowPos.z = Mathf.Clamp(nowPos.z, -4f, 4f);
        transform.localPosition = nowPos;
    }

    public void Shot()
    {
        if(shootInterval <= 0)
        {
            PoolContent obj = bulletPool.Launch(transform.position + Vector3.up * 0.2f, 0);
            if (obj != null) obj.GetComponent <BulletMoving>().speed = 10 ;
            shootInterval = 0.1f;
        }
    }
}
