using UnityEngine;
using System.Collections.Generic;
using System.IO;

public class FileDataService : IDataService
{ 
    private ISerializer serializer;
    private string dataPath;
    private string fileExtension;
    public FileDataService(ISerializer serializer)
    { 
        this.serializer = serializer;
        dataPath = Application.persistentDataPath;//located in C:/Users/<username>/AppData/LocalLow/<gameName>
        fileExtension = ".json";
    }
    private string GetPathFile(string fileName)
    { 
        return Path.Combine(dataPath, string.Concat(fileName, fileExtension));
    }
    public void Save(GameData data, bool overwrite = true)
    {
        string fileLocation = GetPathFile(data.fileName);
        if (!overwrite && File.Exists(fileLocation))
        {
            throw new IOException("the file already exist and no overwrite");        
        }
        File.WriteAllText(fileLocation,serializer.Serialize(data));
    }
    public GameData Load(string fileName)
    {
        string fileLocation = GetPathFile(fileName);

        if (!File.Exists(fileLocation))
        {
            throw new System.Exception("no persistant data found at " + fileLocation);
        }

        string json = File.ReadAllText(fileLocation);
        return serializer.Deserialize<GameData>(json);
    }
    public void Delete(string filename)
    {
        string fileLocation = GetPathFile(filename);
        if (File.Exists(fileLocation))
        {
            File.Exists(fileLocation);
        }
    }
    public IEnumerable<string> ListSaves()
    {
        foreach (string path in Directory.EnumerateFiles(dataPath))
        {
            if (Path.GetExtension(path) == fileExtension)
            {
                yield return Path.GetFileNameWithoutExtension(path);

            }
        }
    }
}
