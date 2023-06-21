using System.Collections.Generic;
using System.IO;

namespace BinarySaving.Runtime
{
    public class SaveFile : ISaveFile
    {
        private readonly SaveFileDescription _fileDescription;
        private readonly ISerializer _serializer;
        private readonly IDeserializer _deserializer;

        private readonly List<ISave> _objectsToSave;

        public SaveFile(SaveFileDescription saveFileDescription, ISerializer serializer, IDeserializer deserializer , IEnumerable<ISave> objectsToSave) 
            : this(saveFileDescription, serializer, deserializer)
        {
            _objectsToSave = new List<ISave>(objectsToSave);
        }

        public SaveFile(SaveFileDescription saveFileDescription, ISerializer serializer, IDeserializer deserializer)
        {
            _fileDescription = saveFileDescription;
            _serializer = serializer;
            _deserializer = deserializer;
        }

        public void SaveAll()
        {
            if (Directory.Exists(_fileDescription.PathToDirectory) == false)
            {
                Directory.CreateDirectory(_fileDescription.PathToDirectory);
            }

            _serializer.BeginSerialization();
            
            foreach (var obj in _objectsToSave)
            {
                obj.Save(_serializer);
            }

            File.WriteAllBytes(_fileDescription.FullPath, _serializer.ToByteArray());
        }

        public void LoadAll()
        {
            if (File.Exists(_fileDescription.FullPath) == false)
            {
                throw new FileLoadException("File does not exist");
            }

            var data = File.ReadAllBytes(_fileDescription.FullPath);
            
            _deserializer.BeginDeserialization(data);

            foreach (var entity in _objectsToSave)
            {
                entity.Load(_deserializer);
            }
        }

        public void AddObject(ISave obj)
        {
            _objectsToSave.Add(obj);
        }

        public void RemoveObject(ISave obj)
        {
            if (_objectsToSave.Contains(obj))
            {
                _objectsToSave.Remove(obj);
            }
        }
    }
}