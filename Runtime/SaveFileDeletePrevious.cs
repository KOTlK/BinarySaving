using System.IO;

namespace BinarySaving.Runtime
{
    public class SaveFileDeletePrevious : ISaveFile
    {
        private readonly ISaveFile _save;
        private readonly SaveFileDescription _fileDescription;

        public SaveFileDeletePrevious(ISaveFile save, SaveFileDescription fileDescription)
        {
            _save = save;
            _fileDescription = fileDescription;
        }

        public void SaveAll()
        {
            if (File.Exists(_fileDescription.FullPath))
            {
                File.Delete(_fileDescription.FullPath);
            }
            
            _save.SaveAll();
        }

        public void LoadAll()
        {
            _save.LoadAll();
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