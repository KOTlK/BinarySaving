using System;
using System.Collections.Generic;
using BinarySaving.Runtime;
using UnityEngine;
using UnityEngine.UI;

namespace BinarySaving.Example
{
    public class Game : MonoBehaviour
    {
        [SerializeField] private Button _saveButton;
        [SerializeField] private Button _loadButton;
        [SerializeField] private Entity[] _entitiesToSave;
        [SerializeField] private SaveFileDescription _saveFileDescription;

        private ISaveFile _save;
        
        private void Start()
        {
            _save = new SaveFileDeletePrevious(
                new SaveFile(
                    new Serializer(
                        new List<byte>()),
                    new Deserializer(),
                    _entitiesToSave));

            _saveButton.onClick.AddListener(Save);
            _loadButton.onClick.AddListener(Load);
        }

        private void OnDestroy()
        {
            _saveButton.onClick.RemoveListener(Save);
            _loadButton.onClick.RemoveListener(Load);
        }

        private void Save()
        {
            _save.SaveAll(_saveFileDescription);
        }

        private void Load()
        {
            _save.LoadAll(_saveFileDescription);
        }
    }
}