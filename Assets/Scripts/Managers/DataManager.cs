using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager
{
    
    public void Init()
    {
        
    }

    public void SaveData()
    {
        PlayerPrefs.SetFloat(Define.SaveKey.playerHp.ToString(),Managers.PlayerData.Hp);
        PlayerPrefs.SetFloat(Define.SaveKey.playerMana.ToString(),Managers.PlayerData.Mana);
        PlayerPrefs.SetInt(Define.SaveKey.levelToken.ToString(),Managers.PlayerData.LevelUpToken);
    }
    
}
