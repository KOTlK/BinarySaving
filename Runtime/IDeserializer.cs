using UnityEngine;

namespace BinarySaving.Runtime
{
    public interface IDeserializer
    {
        void BeginDeserialization(byte[] data);
        int LoadInt();
        bool LoadBool();
        float LoadFloat();
        byte LoadByte();
        string LoadString();
        Vector2 LoadVector2();
        Vector3 LoadVector3();
        Quaternion LoadQuaternion();
        void LoadTransform(Transform origin);
    }
}