using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private string jsonPath;
    [SerializeField] private GenerateCueInScene questionGenerator;
    // [SerializeField] private ImageGenerator imageGenerator;

    private void Start()
    {
        var jsonString = File.ReadAllText(Application.streamingAssetsPath + jsonPath);
        
        // JsonSerializerSettings settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All };
        // settings.Converters.Add(new CueConverter("cueType"));
        RootCue rootCue = JsonConvert.DeserializeObject<RootCue>(jsonString, new CueConverter("cueType"));
        foreach(var cue in rootCue.cues)
        {
           cue.generate();
        }
    }

}
