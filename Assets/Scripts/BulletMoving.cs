using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMoving : MonoBehaviour
{
    public float speed;
    PoolContent poolContent;
    void Start()
    {
        poolContent = GetComponent<PoolContent>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        if(transform.localPosition.z > 5 || transform.localPosition.z < -5 || transform.localPosition.x > 5 || transform.localPosition.x < -5)
        {
            poolContent.HideFromStage();
        }
    }
}
