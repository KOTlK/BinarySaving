using System;
using System.Text;
using UnityEngine;

namespace BinarySaving.Runtime
{
    public class Deserializer : IDeserializer
    {
        private readonly StringBuilder _stringBuilder = new();
        
        private int _index;
        private byte[] _data;

        public Deserializer()
        {
            _index = 0;
        }

        public void BeginDeserialization(byte[] data)
        {
            _data = data;
            _index = 0;
        }

        public int LoadInt()
        {
            var result = 0;

            for (var i = 0; i < sizeof(int); i++)
            {
                result |= _data[_index] << (i * 8);
                _index++;
            }

            return result;
        }

        public bool LoadBool()
        {
            var result = _data[_index] == 1;

            _index++;

            return result;
        }

        public float LoadFloat()
        {
            var result = BitConverter.ToSingle(_data, _index);
            
            _index += 4;
            
            return result;
        }

        public byte LoadByte()
        {
            var result = _data[_index];

            _index++;

            return result;
        }

        public string LoadString()
        {
            var charactersCount = LoadInt();
            _stringBuilder.Clear();

            for (var i = 0; i < charactersCount; i++)
            {
                var character = BitConverter.ToChar(_data, _index);

                _stringBuilder.Append(character);
                _index += sizeof(char);
            }
            
            return _stringBuilder.ToString();
        }

        public Vector2 LoadVector2()
        {
            var result = new Vector2()
            {
                x = LoadFloat(),
                y = LoadFloat()
            };

            return result;
        }

        public Vector3 LoadVector3()
        {
            var result = new Vector3
            {
                x = LoadFloat(),
                y = LoadFloat(),
                z = LoadFloat()
            };

            return result;
        }

        public Quaternion LoadQuaternion()
        {
            var x = LoadFloat();
            var y = LoadFloat();
            var z = LoadFloat();
            var w = LoadFloat();

            return new Quaternion(x, y, z, w);
        }

        public void LoadTransform(Transform origin)
        {
            var position = LoadVector3();
            var rotation = LoadQuaternion();
            var scale = LoadVector3();

            origin.position = position;
            origin.rotation = rotation;
            origin.localScale = scale;
        }

        public void LoadRigidbody(Rigidbody origin)
        {
            origin.position = LoadVector3();
            origin.rotation = LoadQuaternion();
            origin.velocity = LoadVector3();
            origin.mass = LoadFloat();
            origin.isKinematic = LoadBool();
            origin.drag = LoadFloat();
            origin.angularDrag = LoadFloat();
            origin.angularVelocity = LoadVector3();
            origin.detectCollisions = LoadBool();
            origin.freezeRotation = LoadBool();
            origin.useGravity = LoadBool();
            origin.sleepThreshold = LoadFloat();
            origin.centerOfMass = LoadVector3();
            origin.interpolation = (RigidbodyInterpolation)LoadInt();
            origin.constraints = (RigidbodyConstraints)LoadInt();
            origin.collisionDetectionMode = (CollisionDetectionMode)LoadInt();
            origin.inertiaTensorRotation = LoadQuaternion();
            origin.maxAngularVelocity = LoadFloat();
            origin.maxDepenetrationVelocity = LoadFloat();
            origin.solverVelocityIterations = LoadInt();
            origin.inertiaTensor = LoadVector3();
            origin.solverIterations = LoadInt();
        }
    }
}