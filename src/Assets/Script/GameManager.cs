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

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }


    private void Start()
    {
        //var jsonString = File.ReadAllText(Application.streamingAssetsPath + jsonPath);

        // JsonSerializerSettings settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All };
        // settings.Converters.Add(new CueConverter("cueType"));
        /*GameObject g = new GameObject();
        g.transform.position = new Vector3(1, 2, 3);
        g.transform.rotation = Quaternion.Euler(1, 2, 3);
        g.transform.localScale = new Vector3(1, 2, 3);*/
        string x = JsonUtility.ToJson(gameObject);
        // RootCue rootCue = JsonConvert.DeserializeObject<RootCue>(jsonString, new CueConverter("cueType"));
        Debug.Log("x");
        /*foreach(var cue in rootCue.cues)
        {
           cue.generate(generateCueInScene);
        }*/
    }

}
