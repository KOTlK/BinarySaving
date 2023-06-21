namespace BinarySaving.Runtime
{
    public interface ISave
    {
        void Save(ISerializer serializer);
        void Load(IDeserializer deserializer);
    }
}