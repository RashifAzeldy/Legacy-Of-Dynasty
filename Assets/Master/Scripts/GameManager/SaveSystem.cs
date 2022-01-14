using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class SaveSystem
{
    public static List<int> completedAchievementIndex = new List<int>();
    public static List<int> achievementProgressList = new List<int>();
    public static List<int> unlockedHatList = new List<int>();
    public static int hatIndex;

    public static void SavePlayerData(PlayerData playerDataSave)
    {
        BinaryFormatter formatter = new BinaryFormatter();

        string path = Application.persistentDataPath + "/PlayerData.save";
        FileStream stream = new FileStream(path, FileMode.Create);

        PlayerData data = playerDataSave;

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static PlayerData LoadPlayerData()
    {
        string path = Application.persistentDataPath + "/PlayerData.save";

        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            PlayerData data = formatter.Deserialize(stream) as PlayerData;
            stream.Close();

            return data;
        }
        else
        {
            Debug.LogError("Save file not found in " + path);
            return null;
        }
    }
}
