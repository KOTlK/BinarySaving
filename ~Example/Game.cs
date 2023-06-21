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
                    _saveFileDescription,
                    new Serializer(
                        new List<byte>()),
                    new Deserializer(),
                    _entitiesToSave),
                _saveFileDescription);

            _saveButton.onClick.AddListener(_save.SaveAll);
            _loadButton.onClick.AddListener(_save.LoadAll);
        }

        private void OnDestroy()
        {
            _saveButton.onClick.RemoveListener(_save.SaveAll);
            _loadButton.onClick.RemoveListener(_save.LoadAll);
        }
    }
}