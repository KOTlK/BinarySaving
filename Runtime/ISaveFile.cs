namespace BinarySaving.Runtime
{
    public interface ISaveFile
    {
        void SaveAll();
        void LoadAll();
        void AddObject(ISave obj);
        void RemoveObject(ISave obj);
    }
}