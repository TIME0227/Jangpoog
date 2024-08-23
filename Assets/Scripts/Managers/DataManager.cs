using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager
{
    public void Init()
    {
        GetData();
    }

    public void GetData()
    {
        if(Managers.PlayerData == null) return;
        //데이터 가져오기
        if (PlayerPrefs.HasKey(Define.SaveKey.playerHp.ToString()))
        {
            float value = PlayerPrefs.GetFloat(Define.SaveKey.playerHp.ToString());
            Debug.Log($"PlayerPrefs.GetKey(playerHP) = {value}");
            Managers.PlayerData.Hp = value;
        }
        else
        {
            Debug.Log("playerHp playerprefs 키 없음");
        }
        if (PlayerPrefs.HasKey(Define.SaveKey.playerMana.ToString()))
        {
            Managers.PlayerData.Mana = PlayerPrefs.GetInt(Define.SaveKey.playerMana.ToString());
        }
        if (PlayerPrefs.HasKey(Define.SaveKey.levelToken.ToString()))
        {
            Managers.PlayerData.LevelUpToken = PlayerPrefs.GetInt(Define.SaveKey.levelToken.ToString());
        }
    }

    public void SaveData()
    {
        if (Managers.PlayerData == null)
        {
            return;
        }
        PlayerPrefs.SetFloat(Define.SaveKey.playerHp.ToString(),Managers.PlayerData.Hp);
        PlayerPrefs.SetInt(Define.SaveKey.playerMana.ToString(),Managers.PlayerData.Mana);
        PlayerPrefs.SetInt(Define.SaveKey.levelToken.ToString(),Managers.PlayerData.LevelUpToken);
    }
    
}
