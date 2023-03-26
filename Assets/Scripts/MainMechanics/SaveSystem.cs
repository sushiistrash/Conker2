using UnityEngine;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

public class SaveSystem : MonoBehaviour
{
    public static SaveSystem Instance;

    public SaveData ActiveSave;
    private string _dataPath;
    private XmlSerializer _serializer;
    private FileStream _stream;

    [HideInInspector] public bool HasLoaded;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogWarning("More than one instance of save system found!");
            return;
        }
        Instance = this;
        
        Load();
        _dataPath = Application.persistentDataPath;
        _serializer = new XmlSerializer(typeof(SaveData));
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            Save();
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            Load();
        }

        if (Input.GetKeyDown(KeyCode.J))
        {
            DeleteSaveData();
        }
    }

    public void Save()
    {
        _stream = new FileStream(_dataPath + "/" + ActiveSave.SaveName + ".save", FileMode.Create);
        _serializer.Serialize(_stream, ActiveSave);
        _stream.Close();

        Debug.Log("Saved");
    }

    public void Load()
    {
        if (File.Exists(_dataPath + "/" + ActiveSave.SaveName + ".save"))
        {
            _stream = new FileStream(_dataPath + "/" + ActiveSave.SaveName + ".save", FileMode.Open);
            ActiveSave = _serializer.Deserialize(_stream) as SaveData;
            _stream.Close();

            Debug.Log("Save loaded");

            HasLoaded = true;
        }
        else
        {
            HasLoaded = false;
        }
    }

    public void DeleteSaveData()
    {
        if (File.Exists(_dataPath + "/" + ActiveSave.SaveName + ".save"))
        {
            File.Delete(_dataPath + "/" + ActiveSave.SaveName + ".save");

            Debug.Log("Save deleted");
        }
    }
}
