using System.Collections.Generic;
using System.IO;

namespace BinarySaving.Runtime
{
    public class SaveFile : ISaveFile
    {
        private readonly ISerializer _serializer;
        private readonly IDeserializer _deserializer;

        private readonly List<ISave> _objectsToSave;

        public SaveFile(ISerializer serializer, IDeserializer deserializer , IEnumerable<ISave> objectsToSave) 
            : this(serializer, deserializer)
        {
            _objectsToSave = new List<ISave>(objectsToSave);
        }

        public SaveFile(ISerializer serializer, IDeserializer deserializer)
        {
            _serializer = serializer;
            _deserializer = deserializer;
        }

        public void SaveAll(SaveFileDescription fileDescription)
        {
            if (Directory.Exists(fileDescription.PathToDirectory) == false)
            {
                Directory.CreateDirectory(fileDescription.PathToDirectory);
            }

            _serializer.BeginSerialization();
            
            foreach (var obj in _objectsToSave)
            {
                obj.Save(_serializer);
            }

            File.WriteAllBytes(fileDescription.FullPath, _serializer.ToByteArray());
        }

        public void LoadAll(SaveFileDescription fileDescription)
        {
            if (File.Exists(fileDescription.FullPath) == false)
            {
                throw new FileLoadException("File does not exist");
            }

            var data = File.ReadAllBytes(fileDescription.FullPath);
            
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