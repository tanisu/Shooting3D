using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.IO;


[CreateAssetMenu(menuName ="StageSequncer")]
public class StageSequencer : ScriptableObject
{
    [SerializeField] private string filename = "";
    public enum CommandType
    {
        SETSPEED,
        PUTENEMY
    }

    static readonly Dictionary<string, CommandType> commandlist = 
        new Dictionary<string, CommandType>()
        {
            { "SETSPEED",CommandType.SETSPEED},
            { "PUTENEMY",CommandType.PUTENEMY},
        };

    public struct StageData
    {
        public readonly float eventPos;
        public readonly CommandType command;
        public readonly float arg1, arg2;
        public readonly uint arg3;
        //コンストラクタ
        public StageData(float _eventpos,string _command,float _x,float _y,uint _type)
        {
            eventPos = _eventpos;
            command = commandlist[_command];
            arg1 = _x;
            arg2 = _y;
            arg3 = _type;
        }
    }
    StageData[] stageDatas;
    private int stagedataidx = 0;
    [SerializeField] Enemy[] enemyPrefabs = default;

    public void Load()
    {

        Dictionary<string, uint> revarr = new Dictionary<string, uint>();
        for (uint i = 0; i < enemyPrefabs.Length; i++)
        {
            revarr.Add(enemyPrefabs[i].name, i);
        }

        List<StageData> stageCsvData = new List<StageData>();
        string csvdata = Resources.Load<TextAsset>(filename).text;
        StringReader sr = new StringReader(csvdata);//一行ずつに分割する
        while(sr.Peek() != -1)//行がまだあるならtrue ないなら-1が返る
        {
            string line = sr.ReadLine();
            string[] cols = line.Split(',');
            if (cols.Length != 5) continue;
            stageCsvData.Add(
                new StageData(
                    float.Parse(cols[0]),
                    cols[1],
                    float.Parse(cols[2]),
                    float.Parse(cols[3]),
                    revarr.ContainsKey(cols[4])?revarr[cols[4]]:0
                    )
            );
            stageDatas = stageCsvData.OrderBy(values => values.eventPos).ToArray();
        }
    }
    public void Reset()
    {
        stagedataidx = 0;
    }

    public void Step(float _stageProgressTime)
    {
        while(stagedataidx < stageDatas.Length && stageDatas[stagedataidx].eventPos <= _stageProgressTime)
        {
            switch (stageDatas[stagedataidx].command)
            {
                case CommandType.SETSPEED:
                    StageController.I.stageSpeed = stageDatas[stagedataidx].arg1;
                    break;
                case CommandType.PUTENEMY:
                    Enemy enemyTmp = Instantiate(enemyPrefabs[stageDatas[stagedataidx].arg3]);
                    enemyTmp.transform.parent = StageController.I.enemyPool;
                    enemyTmp.transform.localPosition = new Vector3(stageDatas[stagedataidx].arg1, 0, stageDatas[stagedataidx].arg2);
                    break;
            }
            ++stagedataidx;
        }
    }
}
