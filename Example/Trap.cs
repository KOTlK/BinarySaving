using BinarySaving.Runtime;
using UnityEngine;

namespace BinarySaving.Example
{
    public class Trap : Entity
    {
        [SerializeField] private GameObject _disarmedView;
        [SerializeField] private GameObject _armedView;
        [SerializeField] private float _damage = 10f;
        [SerializeField] private bool _disarmed = false;
        
        public override void Save(ISerializer serializer)
        {
            base.Save(serializer);
            serializer.SaveBool(_disarmed);
            serializer.SaveFloat(_damage);
        }

        public override void Load(IDeserializer deserializer)
        {
            base.Load(deserializer);
            _disarmed = deserializer.LoadBool();
            ChangeState(_disarmed);
            _damage = deserializer.LoadFloat();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (_disarmed)
                return;
            
            if (other.TryGetComponent(out Player player))
            {
                player.ApplyDamage(_damage);
                _disarmed = true;
                ChangeState(_disarmed);
            }
        }

        private void ChangeState(bool disarmed)
        {
            if (disarmed)
            {
                _disarmedView.gameObject.SetActive(true);
                _armedView.gameObject.SetActive(false);
            }
            else
            {
                _disarmedView.gameObject.SetActive(false);
                _armedView.gameObject.SetActive(true);
            }
        }
    }
}