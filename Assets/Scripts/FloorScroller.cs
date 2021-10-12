using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorScroller : MonoBehaviour
{
    readonly float areaSizeZ = 1.62f * 8;
    Vector3 basePos;
    

    void Start()
    {
        basePos = transform.position;
    }

    
    void Update()
    {
        float posZ = Mathf.Round((StageController.I.transform.position.z - basePos.z) / areaSizeZ);
        Vector3 nowPos = transform.position;
        nowPos.z = areaSizeZ * posZ + basePos.z;
        transform.position = nowPos;

    }
}
