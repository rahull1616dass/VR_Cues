using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private string jsonPath;

    private void Start()
    {
        var jsonString = File.ReadAllText(Application.streamingAssetsPath + jsonPath);
        RootCue cues = JsonConvert.DeserializeObject<RootCue>(jsonString, new CueConverter("cueType"));
        Debug.Log(cues);
    }

}
