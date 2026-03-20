using System.Collections.Generic;

public interface IDataService
{
    void Save(GameData data, bool overwrite = true);
    GameData Load(string name);//name is the name of the save file
    void Delete(string name);
    IEnumerable<string> ListSaves();
}
