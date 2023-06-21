using UnityEngine;

namespace BinarySaving.Runtime
{
    public interface ISerializer
    {
        void BeginSerialization();
        byte[] ToByteArray();
        void SaveInt(int value);
        void SaveBool(bool value);
        void SaveFloat(float value);
        void SaveByte(byte value);
        void SaveString(string value);
        void SaveVector2(Vector2 value);
        void SaveVector3(Vector3 value);
        void SaveQuaternion(Quaternion quaternion);
        void SaveTransform(Transform transform);
    }
}