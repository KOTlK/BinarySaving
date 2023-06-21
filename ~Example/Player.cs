using System;
using BinarySaving.Runtime;
using UnityEngine;

namespace BinarySaving.Example
{
    public class Player : Entity
    {
        [SerializeField] private float _health;
        [SerializeField] private float _speed;
        [SerializeField] private string _name;

        private Rigidbody _rigidbody;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        public void Move(Vector3 direction)
        {
            _rigidbody.velocity = direction * _speed;
        }

        public void ApplyDamage(float damage)
        {
            _health -= damage;
        }

        public override void Save(ISerializer serializer)
        {
            base.Save(serializer);
            serializer.SaveFloat(_health);
            serializer.SaveFloat(_speed);
            serializer.SaveString(_name);
        }

        public override void Load(IDeserializer deserializer)
        {
            base.Load(deserializer);
            _health = deserializer.LoadFloat();
            _speed = deserializer.LoadFloat();
            _name = deserializer.LoadString();
        }

        private void FixedUpdate()
        {
            var horizontal = Input.GetAxis("Horizontal");
            var vertical = Input.GetAxis("Vertical");

            var direction = new Vector3(horizontal, 0, vertical);

            Move(direction);
        }
    }
}