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
        Cue cues = JsonConvert.DeserializeObject<Cue>(jsonString, new CueConverter("cueType"));
        Debug.Log(cues);
    }

}
