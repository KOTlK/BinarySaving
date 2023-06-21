using System;
using System.Collections.Generic;
using UnityEngine;

namespace BinarySaving.Runtime
{
    public class Serializer : ISerializer
    {
        private readonly List<byte> _rawData;

        public Serializer(List<byte> rawData)
        {
            _rawData = rawData;
        }

        public void BeginSerialization()
        {
            _rawData.Clear();
        }

        public byte[] ToByteArray()
        {
            return _rawData.ToArray();
        }

        public void SaveInt(int value)
        {
            for (var i = 0; i < sizeof(int); i++)
            {
                _rawData.Add((byte)(value >> (i * 8)));
            }
        }

        public void SaveBool(bool value)
        {
            _rawData.Add((byte)(value ? 1 : 0));
        }

        public void SaveFloat(float value)
        {
            var bytes = BitConverter.GetBytes(value);

            foreach (var bt in bytes)
            {
                _rawData.Add(bt);
            }
        }

        public void SaveByte(byte value)
        {
            _rawData.Add(value);
        }

        public void SaveString(string value)
        {
            SaveInt(value.Length);

            foreach (var character in value)
            {
                var bytes = BitConverter.GetBytes(character);

                foreach (var bt in bytes)
                {
                    _rawData.Add(bt);
                }
            }
        }

        public void SaveVector2(Vector2 value)
        {
            SaveFloat(value.x);
            SaveFloat(value.y);
        }

        public void SaveVector3(Vector3 value)
        {
            SaveFloat(value.x);
            SaveFloat(value.y);
            SaveFloat(value.z);
        }

        public void SaveQuaternion(Quaternion quaternion)
        {
            SaveFloat(quaternion.x);
            SaveFloat(quaternion.y);
            SaveFloat(quaternion.z);
            SaveFloat(quaternion.w);
        }

        public void SaveTransform(Transform transform)
        {
            SaveVector3(transform.position);
            SaveQuaternion(transform.rotation);
            SaveVector3(transform.localScale);
        }
    }
}