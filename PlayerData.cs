using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


[System.Serializable]
public class PlayerData 
{
    public int Coins;
    public PlayerData(CarController player)
    {
        Coins = player.highestCoins;
    }
}
