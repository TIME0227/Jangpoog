using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_PlayerMana : MonoBehaviour
{
    private Slider manaSlider;
    private TMP_Text manaText;
    private int maxMana;
    private int currentMana;
    
    
    // Start is called before the first frame update
    void Awake()
    {
       Init();
        
    }
    private void Init()
    {
        manaSlider = GetComponent<Slider>();
        Managers.PlayerData.UpdateManaAction += SetUIMana;

        manaText = GetComponentInChildren<TMP_Text>(true);

    }

    public void SetUIMana(int val)
    {
        maxMana = Managers.PlayerData.MaxMana;
        currentMana = Managers.PlayerData.Mana;
        manaSlider.maxValue = maxMana;
        manaSlider.value = Managers.PlayerData.Mana;

        manaText.text = $"{currentMana}/{maxMana}";
    }

    

    // Update is called once per frame
    void Update()
    {
        
    }
}
