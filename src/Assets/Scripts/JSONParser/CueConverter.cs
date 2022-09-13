using Cues;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;


class CueConverter : JsonConverter
{
    struct CueTypes
    {
        public const string RootCue = "RootCue";
        public const string Questionnaire = "Questionnaire";
        public const string Highlight = "Highlight";
        public const string InfoBox = "InfoBox";
        public const string Media = "Media";
        public const string Haptic = "Haptic";
        public const string Animation = "Animation";
    }
    public CueConverter(string cueTypeSelector, string triggersVariableName)
    {
        this.cueTypeSelector = cueTypeSelector;
        this.triggersVariableName = triggersVariableName;
    }
    protected string cueTypeSelector { get; private set; }
    protected string triggersVariableName { get; private set; }

    public override bool CanConvert(Type objectType)
    {
        return typeof(Cue).IsAssignableFrom(objectType);
    }

    public override bool CanRead => true;

    public override bool CanWrite => false;

    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    {
        throw new NotImplementedException();
    }


    public override object ReadJson(JsonReader reader, Type objectType, object existingValue,
        JsonSerializer serializer)
    {
        JObject jObject = JObject.Load(reader);
        object target;
        switch ((string)jObject[cueTypeSelector])
        {
            case CueTypes.RootCue: target = new RootCue(); break;
            case CueTypes.Questionnaire: target = new Questionnaire(jObject[triggersVariableName]); break;
            case "Media": target = new Media(jObject["triggers"]); break;
            case CueTypes.Highlight: target = new Highlight(jObject[triggersVariableName]); break;
            case CueTypes.InfoBox: target = new InfoBox(jObject[triggersVariableName]); break;
            case CueTypes.Haptic: target = new Haptic(jObject[triggersVariableName]); break;
            default: throw new ArgumentException($"Invalid cue type. {(string)jObject[cueTypeSelector]} does not exist.");
        }
        serializer.Populate(jObject.CreateReader(), target);
        return target;
    }

}

