using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UI_PlayerHp : MonoBehaviour
{
    private Slider hpSlider;
    private TMP_Text hpText;
    private float maxHp;
    private float currentHp;
    
    void Awake()
    {
       Init();
        
    }
    private void Init()
    {
        hpSlider = GetComponent<Slider>();
        Managers.PlayerData.UpdateHpAction += SetUIHp;

        hpText = GetComponentInChildren<TMP_Text>(true);
    }

    public void SetUIHp(float val)
    {
        maxHp = Managers.PlayerData.maxHp;
        currentHp = Managers.PlayerData.Hp;
        hpSlider.maxValue = maxHp;
        hpSlider.value = currentHp;

        hpText.text = $"{currentHp}/{maxHp}";

    }

    

    // Update is called once per frame
    void Update()
    {
        
    }
}
