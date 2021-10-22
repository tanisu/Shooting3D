using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageController : MonoBehaviour
{
    [SerializeField] public ObjectPool playerBulletPool = default;
    [SerializeField] public PlayerController playerCtrl = default;
    [SerializeField] public ObjectPool enemyBulletPool = default;
    [SerializeField] public Transform enemyPool = default;
    [SerializeField] public ObjectPool explosionPool = default;

    [SerializeField] private StageSequencer sequencer = default;

    int score = 0;
    [SerializeField] UnityEngine.UI.Text ScoreValue = default;

    public float stageSpeed = 1;
    private float stageProggressTime = 0;

    public bool isPlaying;
    public bool isStageBossDead;

    public enum PlayStopCodeDef
    {
        PlayerDead,
        BossDefeat,
    }
    public PlayStopCodeDef playStopCode;

    private static StageController i;
    public static StageController I { get => i; }

    private void Awake()
    {
        i = GetComponent<StageController>();
    }

    
    void Start()
    {
        sequencer.Load();
        sequencer.Reset();
        stageProggressTime = 0;
        isPlaying = false;
        SetScore(0);
        playerCtrl.SetUpForTitle();
    }

    void Update()
    {
        if (playerCtrl.isDead)
        {
            playStopCode = PlayStopCodeDef.PlayerDead;
            isPlaying = false;
        }

        if (isStageBossDead)
        {
            playStopCode = PlayStopCodeDef.BossDefeat;
            isPlaying = false;
        }

        if (!isPlaying) return;

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

    public void StageStart()
    {
        isPlaying = true;
        stageProggressTime = 0;
        stageSpeed = 0;
        sequencer.Reset();
        isStageBossDead = false;
        playerCtrl.SetupForPlay();
        
        SetScore(0);
    }
    public void ResetStage()
    {
        BroadcastMessage("HideFromStage",SendMessageOptions.DontRequireReceiver);
        transform.position = Vector3.zero;
        playerCtrl.SetUpForTitle();
    }

    public void AddScore(int _val)
    {
        SetScore(score + _val);
    }
    public void SetScore(int _val)
    {
        score = _val;
        ScoreValue.text = $"{score:000000}";
    }
}
