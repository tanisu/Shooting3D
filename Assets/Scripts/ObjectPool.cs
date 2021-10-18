using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] PoolContent content = default;  //bullet
    Queue<PoolContent> objQueue;
    [SerializeField] int maxObjs = 20;
    
    
    void Start()
    {
        objQueue = new Queue<PoolContent>(maxObjs);
        for(int i = 0; i < maxObjs; i++)
        {
            PoolContent tmpObj = Instantiate(content);
            tmpObj.transform.parent = transform;
            tmpObj.transform.localPosition = new Vector3(100, 0, -100);
            objQueue.Enqueue(tmpObj);
        }
    }

    
    void Update()
    {
        
    }

    public PoolContent Launch(Vector3 _position,float _angle)
    {
        if (objQueue.Count <= 0) return null;
        PoolContent tmpObj = objQueue.Dequeue();
        tmpObj.gameObject.SetActive(true);
        tmpObj.ShowInStage(_position, _angle);
        return tmpObj;
    }

    public void Collect(PoolContent _obj)
    {
        _obj.gameObject.SetActive(false);
        objQueue.Enqueue(_obj);
    }

    public void ResetAll()
    {
        BroadcastMessage("HideFromStage", SendMessageOptions.DontRequireReceiver);
    }

}
