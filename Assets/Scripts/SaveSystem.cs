using UnityEngine;
using System.IO;

public static class SaveSystem
{
    public static readonly string Save_Folder = Application.persistentDataPath + "/saves/";
    public static readonly string File_Ext = ".json";

    public static void Save(string fileName, string dataToSave)
    {
        if (!Directory.Exists(Save_Folder))
        {
            Directory.CreateDirectory(Save_Folder);
        }

        File.WriteAllText(Save_Folder +  fileName + File_Ext, dataToSave);
    }

    public static string Load(string fileName)
    {
        string fileLoc = Save_Folder + fileName + File_Ext;
        if (File.Exists(fileLoc))
        {
            string loadedData = File.ReadAllText(fileLoc);

            return loadedData;
        }
        else
        {
            return null;
        }
    }

}
