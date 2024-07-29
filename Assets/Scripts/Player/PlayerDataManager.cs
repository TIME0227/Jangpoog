using UnityEngine;
using TMPro;

public class PlayerDataManager : MonoBehaviour
{
    // 장풍 데이터 설정
    [SerializeField]
    public GameObject[] jangPoongPrefabs;
    [SerializeField]
    public GameObject jangPoongPrefab;
    [SerializeField]
    public float jangPoongSpeed = 10.0f;
    [SerializeField]
    public float jangPoongDistance = 5.0f;
    [SerializeField]
    public float jangPoongLevel = 1.0f;
    [SerializeField]
    public float jangPoongDamage = 0.5f;
    [SerializeField]
    public float levelUpToken = 0;
    private float[] LevelArr = { 0.5f, 0.7f, 1.1f, 1.6f, 2.2f, 2.9f, 3.5f, 4.2f, 5.0f };

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
    public float hp = 10.0f;
    [SerializeField]
    public float maxHp = 10.0f;

    private void Awake()
    {
        InvokeRepeating("RegenerateMana", 1f, 1f);  // 1초마다 RegenerateMana 메서드 호출
    }

    private void Update()
    {
        UpdateManaText();
        UpdateHpText();

        // 레벨업 테스트용
        if (Input.GetKeyDown(KeyCode.L))
        {
            LevelUp();
        }
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

    // 레벨업
    public void LevelUp()
    {
        levelUpToken += 1;
        jangPoongLevel = Mathf.Clamp(1 + levelUpToken, 1, jangPoongPrefabs.Length);
        jangPoongDamage = LevelArr[(int)jangPoongLevel - 1];
        UpdateJangPoongPrefab();
    }

    // 장풍 프리팹 업데이트
    private void UpdateJangPoongPrefab()
    {
        jangPoongPrefab = jangPoongPrefabs[(int)jangPoongLevel - 1];
    }

    // 체력 텍스트 업데이트
    private void UpdateHpText()
    {
        hpText.text = $"Hp {hp}/{maxHp}";
    }
}
