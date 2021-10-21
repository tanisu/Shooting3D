using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionParticle : MonoBehaviour
{
    private ParticleSystem p;
    private PoolContent pool;


    private void Awake()
    {
        p = GetComponent<ParticleSystem>();
        pool = GetComponent<PoolContent>();
    }

    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    public void PlayParticle()
    {
        p.Play();
        StartCoroutine(TimeWait());
    }

    IEnumerator TimeWait()
    {
        yield return new WaitForSeconds(0.3f);
        p.Stop();
        pool.HideFromStage();
    }
}
