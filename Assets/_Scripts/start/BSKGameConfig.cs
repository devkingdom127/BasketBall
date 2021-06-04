using UnityEngine;
using System.Collections;
using SimpleJSON;

public class StageConfig
{
    public int id;
    public int target;
    public int time;
    public int passReward;
}

public class SpinConfig
{
    
}

public class BallConfig
{
    public int id;
    public string image;
    public int price;
}

public class BSKGameConfig : MonoBehaviour
{

    static public BSKGameConfig s_Instance;

    public float gameTimeScale = 1.4f;

    public Material[] ballMaterials;

    private TextAsset stageFileData;
    private ArrayList stageConfigs;

    private TextAsset ballFileData;
    private ArrayList ballsConfig;

    void Awake()
    {
        s_Instance = this;

        stageFileData = (TextAsset)Resources.Load("Config/StageData");
        stageConfigs = new ArrayList();
        ReadStageConfig();

        ballFileData = (TextAsset)Resources.Load("Config/BallsData");
        ballsConfig = new ArrayList();
        ReadBallsConfig();

        AddBall(1);
        /*if(ballFileData != null)
        {
            Debug.Log(ballFileData.text);
            Debug.Log(ballsConfig.ToString());
        }*/

        /*for(int i = 1; i <= 20; i++)
        {
            var v = GetBallDataWithId(i);
            Debug.Log("id:" + v.id.ToString()+", image:" + v.image + ", price:" + v.price.ToString());
        }*/
    }

    void ReadStageConfig()
    {
        stageConfigs.Clear();
        var root = JSONNode.Parse(stageFileData.text);
        var data = root["StageData"].AsArray;

        for(int i = 0; i < data.Count; i++)
        {
            StageConfig con = new StageConfig();
            con.id = root["StageData"][i][0].AsInt;
            con.target = root["StageData"][i][1].AsInt;
            con.time = root["StageData"][i][2].AsInt;
            con.passReward = root["StageData"][i][3].AsInt;

            stageConfigs.Add(con);
        }
    }

    public StageConfig GetStageConfigById(int id)
    {
        StageConfig con = null;

        if(id > 5)
        {
            StageConfig tempCon = GetStageConfigById(5);
            con = new StageConfig();
            con.target = tempCon.target + (id - 5) * 50;
            con.time = tempCon.time;
            con.passReward = tempCon.passReward;
        }
        else
        {
            foreach(var val in stageConfigs)
            {
                StageConfig data = (StageConfig)val;
                if(id == data.id)
                {
                    con = data;
                    break;
                }
            }
        }

        return con;
    }

    void ReadBallsConfig()
    {
        ballsConfig.Clear();
        var root = JSONNode.Parse(ballFileData.text);
        var data = root["BallsData"].AsArray;

        for(int i = 0; i < data.Count; i++)
        {
            BallConfig con = new BallConfig();
            con.id = root["BallsData"][i][0].AsInt;
            con.image = root["BallsData"][i][1];
            con.price = root["BallsData"][i][2].AsInt;

            ballsConfig.Add(con);
        }
    }

    public BallConfig GetBallDataWithId(int id)
    {
        BallConfig con = null;
        foreach(var val in ballsConfig)
        {
            BallConfig data = (BallConfig)val;
            if(id == data.id)
            {
                con = data;
                break;
            }
        }
        return con;
    }

    public bool IsBMGOn()
    {
        if (!PlayerPrefs.HasKey("Music"))
        {
            PlayerPrefs.SetInt("Music", 1);
        }

        if (PlayerPrefs.GetInt("Music") == 1)
        {
            return true;
        }

        return false;
    }

    public bool IsAudioOn()
    {
        if (!PlayerPrefs.HasKey("Audio"))
        {
            PlayerPrefs.SetInt("Audio", 1);
        }

        if (PlayerPrefs.GetInt("Audio") == 1)
        {
            return true;
        }

        return false;
    }

    public void SaveBMGPrefs(int val)
    {
        PlayerPrefs.SetInt("Music", val);
    }


    public void SaveAudioPrefs(int val)
    {
        PlayerPrefs.SetInt("Audio", val);
    }

    public int GetCoin()
    {
        return PlayerPrefs.GetInt("Coin", 0);
    }

    public void AddCoin(int num)
    {
        int count = GetCoin() + num;
        if(count < 0)
        {
            count = 0;
        }
        PlayerPrefs.SetInt("Coin", count);
    }

    public bool HasBall(int id)
    {
        int val = PlayerPrefs.GetInt("Ball_" + id.ToString(), 0);

        return val == 1 ? true : false;
    }

    public void AddBall(int id)
    {
        if (HasBall(id))
            return;
        PlayerPrefs.SetInt("Ball_" + id.ToString(), 1);
    }

    public int GetInUsingBall()
    {
        return PlayerPrefs.GetInt("Ball_Using", 1);
    }

    public void UsingBall(int id)
    {
        PlayerPrefs.SetInt("Ball_Using", id);
    }

    public Material GetBallMaterial()
    {
        int id = GetInUsingBall();
        return ballMaterials[id - 1];
    }

    public int GetBestScore()
    {
        return PlayerPrefs.GetInt("Best_Score", 0);
    }

    public void SetBestScore(int score)
    {
        PlayerPrefs.SetInt("Best_Score", score);
    }

}
      
