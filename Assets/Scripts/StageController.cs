using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageController : MonoBehaviour
{
    [SerializeField] public ObjectPool playerBulletPool = default;
    [SerializeField] public PlayerController playerCtrl = default;
    [SerializeField] public ObjectPool enemyBulletPool = default;
    [SerializeField] public Transform enemyPool = default;
    [SerializeField] private StageSequencer sequencer = default;

    public float stageSpeed = 1;
    private float stageProggressTime = 0;

    private static StageController i;
    public static StageController I { get => i; }

    private void Awake()
    {
        i = GetComponent<StageController>();
    }

    // Start is called before the first frame update
    void Start()
    {
        sequencer.Load();
        sequencer.Reset();
        stageProggressTime = 0;
    }

    void Update()
    {
        sequencer.Step(stageProggressTime);
        stageProggressTime += Time.deltaTime;
        transform.Translate(Vector3.forward * Time.deltaTime * stageSpeed);
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");
        playerCtrl.Move(new Vector3(x, 0, z));
        if (Input.GetButton("Fire1"))
        {
            playerCtrl.Shot();
        }


    }
}
