using System.Collections.Generic;
using BinarySaving.Runtime;
using NUnit.Framework;
using UnityEngine;

namespace BinarySaving.Tests
{
    public class SerializationDeserializationTests
    {
        [Test]
        public void SerializeEverything()
        {
            var data = new List<byte>();
            var serializer = new Serializer(data);
            var deserializer = new Deserializer();
            var gameObject = new GameObject();
            var quaternionToSave = new Quaternion(1, 1, 0.5f, 0);
            var vector2ToSave = new Vector2(145, 41);
            var vector3ToSave = new Vector3(34, 33, 22);
            var position = new Vector3(10, 10, 9);
            var rotation = new Quaternion(1, 1, 1, 1);
            var scale = new Vector3(2, 2, 2);
            var floatToSave = 10f;
            var stringToSave = "123";
            var boolToSave = false;
            byte byteToSave = 255;
            var intToSave = 10000;
            gameObject.transform.position = position;
            gameObject.transform.rotation = rotation;
            gameObject.transform.localScale = scale;

            serializer.SaveFloat(floatToSave);
            serializer.SaveString(stringToSave);
            serializer.SaveBool(boolToSave);
            serializer.SaveByte(byteToSave);
            serializer.SaveInt(intToSave);
            serializer.SaveQuaternion(quaternionToSave);
            serializer.SaveVector2(vector2ToSave);
            serializer.SaveVector3(vector3ToSave);
            serializer.SaveTransform(gameObject.transform);


            deserializer.BeginDeserialization(data.ToArray());
            var savedFloat = deserializer.LoadFloat();
            var savedString = deserializer.LoadString();
            var savedBool = deserializer.LoadBool();
            var savedByte = deserializer.LoadByte();
            var savedInt = deserializer.LoadInt();
            var savedQuaternion = deserializer.LoadQuaternion();
            var savedVector2 = deserializer.LoadVector2();
            var savedVector3 = deserializer.LoadVector3();
            deserializer.LoadTransform(gameObject.transform);

            Assert.True(savedFloat == floatToSave);
            Assert.True(savedString == stringToSave);
            Assert.True(savedBool == boolToSave);
            Assert.True(savedByte == byteToSave);
            Assert.True(savedInt == intToSave);
            Assert.True(savedQuaternion == quaternionToSave);
            Assert.True(savedVector2 == vector2ToSave);
            Assert.True(savedVector3 == vector3ToSave);
            Assert.True(gameObject.transform.position == position);
            Assert.True(gameObject.transform.rotation == rotation);
            Assert.True(gameObject.transform.localScale == scale);
        }
    }
}
