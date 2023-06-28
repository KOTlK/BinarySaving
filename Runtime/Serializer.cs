using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

        public void SaveRigidbody(Rigidbody rigidbody)
        {
            SaveVector3(rigidbody.position);
            SaveQuaternion(rigidbody.rotation);
            SaveVector3(rigidbody.velocity);
            SaveFloat(rigidbody.mass);
            SaveBool(rigidbody.isKinematic);
            SaveFloat(rigidbody.drag);
            SaveFloat(rigidbody.angularDrag);
            SaveVector3(rigidbody.angularVelocity);
            SaveBool(rigidbody.detectCollisions);
            SaveBool(rigidbody.freezeRotation);
            SaveBool(rigidbody.useGravity);
            SaveFloat(rigidbody.sleepThreshold);
            SaveVector3(rigidbody.centerOfMass);
            SaveInt((int)rigidbody.interpolation);
            SaveInt((int)rigidbody.constraints);
            SaveInt((int)rigidbody.collisionDetectionMode);
            SaveQuaternion(rigidbody.inertiaTensorRotation);
            SaveFloat(rigidbody.maxAngularVelocity);
            SaveFloat(rigidbody.maxDepenetrationVelocity);
            SaveInt(rigidbody.solverVelocityIterations);
            SaveVector3(rigidbody.inertiaTensor);
            SaveInt(rigidbody.solverIterations);
        }
    }
}