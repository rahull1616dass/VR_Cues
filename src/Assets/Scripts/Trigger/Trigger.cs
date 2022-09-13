public class Trigger
{
    /*
     * Given that the TriggerPoints are generic and can be of either cueTransform or float type,
     * we do not want the serializer.Populate(jObject.CreateReader(), target) call of CueConverter.cs
     * to handle this, but rather we parse these fields ourselves in the Cue constructor.
     * As such, the names are prepended with '_' char to make sure that the CueConverter.cs does not match
     * them with the field names of JSON, we will be handling that.
    */
    public TriggerPoint _startPoint;
    public TriggerPoint _endPoint;

    public Trigger(TriggerPoint startPoint, TriggerPoint endPoint)
    {
        this._startPoint = startPoint;
        this._endPoint = endPoint;
    }
}