using Cues;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;


class CueConverter : JsonConverter
{
    public CueConverter(string Selector)
    {
        selector = Selector;
    }
    protected string selector { get; private set; }

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
        switch ((string)jObject[selector])
        {
            case "RootCue": target = new RootCue(); break;
            case "Questionnaire": target = new Questionnaire(jObject["triggers"]); break;
            //case "Media": target = new Media(jObject["triggers"]); break;
            case "Highlight": target = new Highlight(jObject["triggers"]); break;
            case "InfoBox": target = new InfoBox(jObject["triggers"]); break;
            case "Haptic": target = new Haptic(jObject["triggers"]); break;
            default: throw new ArgumentException("Invalid source type");
        }
        serializer.Populate(jObject.CreateReader(), target);
        return target;
    }

}

