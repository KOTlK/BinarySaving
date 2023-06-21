using System;
using UnityEngine;

namespace BinarySaving.Runtime
{
    [Serializable]
    public class SaveFileDescription
    {
        public string Name;
        public string Path;
        public string DirectoryName;

        public string FullPath => @$"{Convert(Path)}\{DirectoryName}\{Name}";
        public string PathToDirectory => @$"{Convert(Path)}\{DirectoryName}";

        private string Convert(string path)
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
    }
}