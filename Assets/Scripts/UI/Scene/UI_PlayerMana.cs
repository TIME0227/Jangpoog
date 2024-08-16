using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_PlayerMana : MonoBehaviour
{
    private Slider manaSlider;
    private int maxMana;
    
    
    // Start is called before the first frame update
    void Start()
    {
       Init();
        
    }
    private void Init()
    {
        manaSlider = GetComponent<Slider>();
        Managers.PlayerData.UpdateManaAction += SetUIMana;
        
    }

    public void SetUIMana(int val)
    {
        maxMana = Managers.PlayerData.MaxMana;
        manaSlider.maxValue = maxMana;
        manaSlider.value = Managers.PlayerData.Mana;

    }

    

    // Update is called once per frame
    void Update()
    {
        
    }
}
