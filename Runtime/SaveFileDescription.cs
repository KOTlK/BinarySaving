using System;
using UnityEngine;

namespace BinarySaving.Runtime
{
    [Serializable]
    public class SaveFileDescription : ISave
    {
        public string Name;
        public string Path;
        public string DirectoryName;

        public SaveFileDescription() : this("", "", "")
        {
        }
        
        public SaveFileDescription(string name, string path, string directoryName)
        {
            Name = name;
            Path = path;
            DirectoryName = directoryName;
        }

        public string FullPath => @$"{ConvertPath(Path)}\{DirectoryName}\{Name}";
        public string PathToDirectory => @$"{ConvertPath(Path)}\{DirectoryName}";

        public void Save(ISerializer serializer)
        {
            serializer.SaveString(Name);
            serializer.SaveString(Path);
            serializer.SaveString(DirectoryName);
        }

        public void Load(IDeserializer deserializer)
        {
            Name = deserializer.LoadString();
            Path = deserializer.LoadString();
            DirectoryName = deserializer.LoadString();
        }

        private static string ConvertPath(string path)
        {
            if (path.StartsWith('!'))
            {
                return path switch
                {
                    "!persistent" => Application.persistentDataPath,
                    "!data" => Application.dataPath,
                    "!streamingAssets" => Application.streamingAssetsPath,
                    "!tempCache" => Application.temporaryCachePath,
                    _ => path
                };
            }

            return path;
        }
    }
}