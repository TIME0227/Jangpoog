using UnityEngine;
using TMPro;

public class PlayerDataManager : MonoBehaviour
{
    // 장풍 데이터 설정
    [SerializeField]
    public GameObject jangPoongPrefab;
    [SerializeField]
    public float jangPoongSpeed = 10.0f;
    [SerializeField]
    public float jangPoongDistance = 5.0f;
    [SerializeField]
    public float jangPoongLevel = 1.0f;

    // 마나 데이터 설정
    [SerializeField]
    private TextMeshProUGUI manaText;
    [SerializeField]
    public float mana = 100f;
    [SerializeField]
    public float maxMana = 100f;
    [SerializeField]
    public float manaRegenerationRate = 3f;
    [SerializeField]
    public float manaConsumption = 5f;

    // 체력 데이터 설정
    [SerializeField]
    private TextMeshProUGUI hpText;
    [SerializeField]
    public float hp = 100f;
    [SerializeField]
    public float maxHp = 100f;

    private void Awake()
    {
        InvokeRepeating("RegenerateMana", 1f, 1f);  // 1초마다 RegenerateMana 메서드 호출
    }

    private void Update()
    {
        UpdateManaText();
    }

    // 마나 재생
    private void RegenerateMana()
    {
        mana = Mathf.Min(mana + manaRegenerationRate, maxMana);
    }

    // 마나 텍스트 업데이트
    private void UpdateManaText()
    {
        manaText.text = $"Mana {mana}/{maxMana}";
    }

}
