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
            case "Questionnaire": target = new Questionnaire(); break;
            case "Image": target = new Image(); break;
            case "RootCue": target = new RootCue(); break;
            default: throw new ArgumentException("Invalid source type");
        }
        serializer.Populate(jObject.CreateReader(), target);
        return target;
    }

}

