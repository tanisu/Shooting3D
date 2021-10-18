using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int hitPoint = 1;
    private Material flashMaterial = null;
    private ObjectPool bulletPool;

    void Start()
    {
        flashMaterial = transform.GetComponentsInChildren<Renderer>()[0].material;
        flashMaterial.EnableKeyword("_EMISSION");
        bulletPool = StageController.I.enemyBulletPool;
    }

    public void HideFromStage()
    {
        Destroy(gameObject);
    }

    
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider _other)
    {
        if (_other.CompareTag("PlayerBullet"))
        {
            PoolContent poolObj = _other.GetComponent<PoolContent>();
            poolObj.HideFromStage();
            hitPoint -= 1;
            if(hitPoint <= 0)
            {
                HideFromStage();
            }
            else
            {
                StartCoroutine(FlashTimeWait());
            }
        }
    }

    private IEnumerator FlashTimeWait()
    {
        flashMaterial.SetColor("_EmissionColor", Color.red);
        yield return new WaitForSeconds(0.05f);
        flashMaterial.SetColor("_EmissionColor",Color.black);
    }

    public void Shot(EnemyBulletPattern _o)
    {
        float angleOffset = (_o.Count - 1) / 2.0f;
        float baseDirection = 0;
        if (_o.IsAimPlayer)
        {
            baseDirection = Vector3.SignedAngle(
                Vector3.forward, 
                StageController.I.playerCtrl.transform.localPosition - transform.localPosition, 
                Vector3.up
            );
        }
        else
        {
            baseDirection = _o.Direction;
        }

        for (int i = 0; i < _o.Count; i++)
        {
            float d = ((i - angleOffset) * _o.OpenAngle);
            PoolContent obj = bulletPool.Launch(transform.position + Vector3.up * 0.2f, d + baseDirection);
            if (obj != null) obj.GetComponent<BulletMoving>().speed = _o.Speed;
        }
        
    }
}
