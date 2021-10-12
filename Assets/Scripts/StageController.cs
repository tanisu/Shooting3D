using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageController : MonoBehaviour
{
    [SerializeField] public PlayerController playerCtrl = default;
    private float stageSpeed = 1;
    private static StageController i;
    public static StageController I { get => i; }

    private void Awake()
    {
        i = GetComponent<StageController>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * stageSpeed);
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");
        playerCtrl.Move(new Vector3(x, 0, z));



    }
}
