using Newtonsoft.Json;
public class TriggerPoint
{
    [JsonProperty("_triggerPoint")]
    [JsonConverter(typeof(TriggerPointConverter))]
    public object _triggerPoint;

    public TriggerPoint(float triggerPoint) { _triggerPoint = triggerPoint; }
    public TriggerPoint(CueTransform triggerPoint) { _triggerPoint = triggerPoint; }
}