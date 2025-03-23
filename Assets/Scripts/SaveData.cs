using UnityEngine;


[System.Serializable]
public class SaveData
{
    public float highScore;
    public string name;

    public void Save()
    {
        string saveString = JsonUtility.ToJson(this);
        SaveSystem.Save("save", saveString);
    }

    public static SaveData Load()
    {
        string savedData = SaveSystem.Load("save");
        if (!string.IsNullOrEmpty(savedData))
        {
            return JsonUtility.FromJson<SaveData>(savedData);
        }
        else
        {
            return new SaveData();
        }
    }
}
