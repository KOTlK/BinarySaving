using System.IO;

namespace BinarySaving.Runtime
{
    public class SaveFileDeletePrevious : ISaveFile
    {
        private readonly ISaveFile _save;

        public SaveFileDeletePrevious(ISaveFile save)
        {
            _save = save;
        }

        public void SaveAll(SaveFileDescription fileDescription)
        {
            if (File.Exists(fileDescription.FullPath))
            {
                File.Delete(fileDescription.FullPath);
            }
            
            _save.SaveAll(fileDescription);
        }

        public void LoadAll(SaveFileDescription fileDescription)
        {
            _save.LoadAll(fileDescription);
        }

        public void AddObject(ISave obj)
        {
            _save.AddObject(obj);
        }

        public void RemoveObject(ISave obj)
        {
            _save.RemoveObject(obj);
        }
    }
}