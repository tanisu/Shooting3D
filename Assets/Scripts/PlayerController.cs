using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private ObjectPool bulletPool;
    private float shootInterval = 0;
    public bool isDead;

    private Vector3 restartPos;
    private Vector3 restartRot;

    private void Awake()
    {
        restartPos = transform.localPosition;
        restartRot = transform.localEulerAngles;
    }

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

    private void OnTriggerEnter(Collider _other)
    {
        if (_other.CompareTag("EnemyBullet"))
        {
            _other.GetComponent<PoolContent>().HideFromStage();
            isDead = true;
        }
    }

    public void SetupForPlay()
    {
        shootInterval = 0;
        isDead = false;
        transform.localPosition = new Vector3(0, 0, -3.5f);
        transform.localEulerAngles = new Vector3(0, 0, 0);
    }

    public void SetUpForTitle()
    {
        transform.localPosition = restartPos;
        transform.localEulerAngles = restartRot;
    }
}
