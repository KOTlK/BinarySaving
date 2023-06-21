using BinarySaving.Runtime;
using UnityEngine;

namespace BinarySaving.Example
{
    public abstract class Entity : MonoBehaviour, ISave
    {
        public virtual void Save(ISerializer serializer)
        {
            serializer.SaveTransform(transform);
        }

        public virtual void Load(IDeserializer deserializer)
        {
            deserializer.LoadTransform(transform);
        }
    }
}