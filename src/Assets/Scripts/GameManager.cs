using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private string jsonPath;
    public static GameManager instance;
    public GenerateCueInScene generateCueInScene;
    public static string gameStartTimestamp;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }


    private void Start()
    {
        var jsonString = File.ReadAllText(Application.streamingAssetsPath + jsonPath);
        RootCue rootCue = JsonConvert.DeserializeObject<RootCue>(jsonString, new CueConverter("cueType", "triggers"));
        LogHelper.CreateLogTxtFile();
        for (int i = 0; i < rootCue.cues.Count; i++)
        {
            rootCue.cues[i].generate(i);
        }
    }

}
