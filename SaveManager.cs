using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

[System.Serializable]
public class SaveManager
{
    public static void SavePlayer(CarController car)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/playerdata.enn";
        FileStream stream = new FileStream(path, FileMode.Create);

        PlayerData data = new PlayerData(car);
        formatter.Serialize(stream, data);
    }

    public static PlayerData LoadPlayer()
    {
        string path = Application.persistentDataPath + "/playerdata.enn";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            PlayerData data = formatter.Deserialize(stream) as PlayerData;

            return data;
        }
        else
        {
            Debug.LogError("Save file not found in " + path);
            return null;
        }
    }
}
