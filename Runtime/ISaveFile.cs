namespace BinarySaving.Runtime
{
    public interface ISaveFile
    {
        void SaveAll(SaveFileDescription fileDescription);
        void LoadAll(SaveFileDescription fileDescription);
        void AddObject(ISave obj);
        void RemoveObject(ISave obj);
    }
}