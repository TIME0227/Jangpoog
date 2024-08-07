using System;
using System.Collections;
using UnityEngine;
using TMPro;
using Unity.Mathematics;

public class PlayerDataManager : MonoBehaviour
{
    [Header("JangPoong")]
    // ��ǳ ������ ����
    [SerializeField]
    public GameObject[] jangPoongPrefabs;

    [SerializeField] public GameObject jangPoongPrefab;
    [SerializeField] public float jangPoongSpeed = 10.0f;
    [SerializeField] public float jangPoongDistance = 5.0f;
    [SerializeField] public float jangPoongLevel = 1.0f;
    [SerializeField] public float jangPoongDamage = 0.5f;
    [SerializeField] public float levelUpToken = 0;
    private float[] LevelArr = { 0.5f, 0.7f, 1.1f, 1.6f, 2.2f, 2.9f, 3.5f, 4.2f, 5.0f };

    [Header("Mana")]
    // ���� ������ ����
    [SerializeField]
    private TextMeshProUGUI manaText;

    [SerializeField] public float mana = 100f;
    [SerializeField] public float maxMana = 100f;
    [SerializeField] public float manaRegenerationRate = 3f;
    [SerializeField] public float manaConsumption = 5f;

    // ü�� ������ ����
    [Header("Hp")] [SerializeField] private TextMeshProUGUI hpText;

    [SerializeField]
    //HP private로 변경, 프로퍼티 생성
    private float hp = 10.0f;

    [SerializeField] public float maxHp = 10.0f;

    public float Hp
    {
        get { return hp; }
        private set
        {
            if (value != hp)
            {
                hp = value;
                UpdateHpText();

            }
        }
    }

    //Action
    public Action DieAction = null;

    //Invincibility
    [Header("Invincibility")] 
    [SerializeField][Tooltip("피격 시 추가되는 무적 지속 시간")] private float invincibilityDuration = 1;//피격시 추가되는 무적 시간
    private float invincibilityTime = 0; //무적 지속 시간
    private bool isInvincibility = false; //무적 여부
    [SerializeField] private SpriteRenderer spriteRenderer;
    private Color originColor;
    

    private void Awake()
    {
        InvokeRepeating("RegenerateMana", 1f, 1f); // 1�ʸ��� RegenerateMana �޼��� ȣ��
        if(spriteRenderer==null) spriteRenderer = transform.parent.GetComponentInChildren<SpriteRenderer>();
        originColor = spriteRenderer.color;
    }

    private void Update()
    {
        UpdateManaText();
        UpdateHpText();

        // ������ �׽�Ʈ��
        if (Input.GetKeyDown(KeyCode.L))
        {
            LevelUp();
        }
    }

    // ���� ���
    private void RegenerateMana()
    {
        mana = Mathf.Min(mana + manaRegenerationRate, maxMana);
    }

    // ���� �ؽ�Ʈ ������Ʈ
    private void UpdateManaText()
    {
        manaText.text = $"Mana {mana}/{maxMana}";
    }

    // ������
    public void LevelUp()
    {
        levelUpToken += 1;
        jangPoongLevel = Mathf.Clamp(1 + levelUpToken, 1, jangPoongPrefabs.Length);
        jangPoongDamage = LevelArr[(int)jangPoongLevel - 1];
        UpdateJangPoongPrefab();
    }

    // ��ǳ ������ ������Ʈ
    private void UpdateJangPoongPrefab()
    {
        jangPoongPrefab = jangPoongPrefabs[(int)jangPoongLevel - 1];
    }


    #region HP

    // ü�� �ؽ�Ʈ ������Ʈ
    private void UpdateHpText()
    {
        hpText.text = $"Hp {hp:F2}/{maxHp:F2}"; //Format the HP text with two decimal places (240807)
    }

    public void OnAttacked(float damage)
    {
        if (damage <= 0)
        {
            Debug.Log("오류 : 몬스터 공격 데미지가 0 또는 음수입니다!!");
            return;
        }
        //1. 무적 상태 처리
        if (isInvincibility) return; //무적 상태에서는 HP 감소 x

        OnInvincibility(invincibilityDuration); //공격 받으면 invincibilityDuration초 동안 무적 상태

        //2. 체력 감소 처리
        Hp = Mathf.Clamp(Hp - damage, 0, Hp);
        if (Hp == 0)
        {
            DieAction?.Invoke();
        }
    }

    #endregion



    #region invincibility

    private void OnInvincibility(float time)
    {
        if (isInvincibility)
        {
            invincibilityTime += time;
        }
        else
        {
            invincibilityTime = time;
            StartCoroutine(nameof(Invincibility));

        }

    }

    IEnumerator Invincibility()
    {
        //1. flag 설정
        isInvincibility = true;
        //2. invincibilityTime 동안 레이어 변경, 깜박이기 효과
        transform.parent.gameObject.layer = (int)Define.Layer.PlayerDamaged; //무적 상태 레이어로 변경
        
        //3. blink speed
        float blinkSpeed = 10;
        while (invincibilityTime>0)
        {
            invincibilityTime -= Time.deltaTime;
            Color color = spriteRenderer.color;
            color.a = Mathf.SmoothStep(0, 1, Mathf.PingPong(Time.time * blinkSpeed, 1));
            //PingPong : 0~1 사이를 왕복
            //SmoothStep : 두 값 사이의 부드러운 전환(보간) 효과
            spriteRenderer.color = color;
            yield return null;
        }

        spriteRenderer.color = originColor; //alpha 복구
        transform.parent.gameObject.layer = (int)Define.Layer.Player; //원래 레이어로 복구
        isInvincibility = false;

    }

#endregion
}
