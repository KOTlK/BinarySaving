# BinarySaving
Simple and fast game saves system.

# Installing

Make sure you have standalone git installed.

- Unity -> Package Manager -> Add package from git URL
- Paste: `https://github.com/KOTlK/BinarySaving.git`

# Saving data

- Implement interface `ISave` in object you want to save.
- Inside `Save` method save all data your object need.
- Inside `Load` method load all data.
``` C#
public class CharactersFactory : MonoBehaviour, ISave
{
    [SerializeField] private Character _prefab;

    private List<Character> _spawnedCharacters = new();

    public void Save(ISerializer serializer)
    {
        serializer.SaveInt(_spawnedCharacters.Count);

        foreach(var character in _spawnedCharacters)
        {
            character.Save(serializer);
        }
    }

    public void Load(IDeserializer deserializer)
    {
        var spawnCount = deserializer.LoadInt();

        for(var i = 0; i < spawnCount; i++>)
        {
            var character = CreateCharacter();
            character.Load(deserializer);
        }
    }

    public Character CreateCharacter()
    {
        var character = Instantiate(_prefab);
        _spawnedCharacters.Add(character);

        return character;
    }
}
```

# Saving into file

- Create object that implements `ISaveFile` interface.
- Add objects you need to save/load.
- Call `SaveAll`/`LoadAll` method.

``` C#
public class Startup : MonoBehaviour
{
    [SerializeField] private CharactersFactory _charactersFactory;
    [SerializeField] private SaveFileDescription _saveDescription;

    private ISaveFile _save;

    private void Start()
    {
        _save = new SaveFileDeletePrevious(
                new SaveFile(
                    new Serializer(
                        new List<byte>()),
                    new Deserializer(),
                    new List<ISave>()));

        _save.AddObject(_charactersFactory);
    }

    private void Update()
    {
        if(//something)
        {
            _save.SaveAll(_saveDescription);
        }

        if(//something else)
        {
            _save.LoadAll(_saveDescription);
        }
    }
}
```

You can use Unity's data path's in `Path` field inside `SaveFileDescription`.
To do this, set one of prefixes displayed below into `Path` field. It can be helpful when using unity inspector to set file description.

- !persistent => `Application.persistentDataPath`
- !data => `Application.dataPath`
- !streamingAssets => `Application.streamingAssetsPath`
- !tempCache => `Application.temporaryCachePath`