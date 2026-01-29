using System;
using UnityEngine;
using System.Collections;


public class PlayerStatus : MonoBehaviour
{
    public event Action OnStatusChanged;

    [Header("Reference")]
    [SerializeField] private PlayerEquipment equipment;
    [SerializeField] private EquipmentDataSO equipmentDataSO;

    [Header("Level / EXP")]
    [SerializeField] private int level = 1;
    [SerializeField] private int currentExp = 0;
    [SerializeField] private int expToNextLevel = 100;

    [Header("Base Status")]
    [SerializeField] private int baseHP = 100;
    [SerializeField] private int baseMP = 30;
    [SerializeField] private int baseAttack = 10;
    [SerializeField] private int baseDefense = 5;
    [SerializeField] private float baseCritical = 0.05f;
    [SerializeField] private float baseEvasion = 0.03f;

    private int currentHP;
    private int currentMP;
    private int attackBuff;
    private Coroutine attackBuffCoroutine;


    // ===== Getter =====
    public int Level => level;
    public int CurrentExp => currentExp;
    public int ExpToNextLevel => expToNextLevel;

    public int MaxHP => baseHP + EquipHP;
    public int MaxMP => baseMP + EquipMP;
    public int Attack => baseAttack + EquipAttack + attackBuff;

    public int Defense => baseDefense + EquipDefense;

    public float CriticalRate => baseCritical + EquipCritical;
    public float EvasionRate => baseEvasion + EquipEvasion;

    public int CurrentHP => currentHP;
    public int CurrentMP => currentMP;

    // ===== 装備ボーナス（UI用）=====
    public int EquipHP => SumInt(e => e.TotalHP(equipmentDataSO));
    public int EquipMP => SumInt(e => e.TotalMP(equipmentDataSO));
    public int EquipAttack => SumInt(e => e.TotalAttack(equipmentDataSO));
    public int EquipDefense => SumInt(e => e.TotalDefense(equipmentDataSO));
    public float EquipCritical => SumFloat(e => e.TotalCritical(equipmentDataSO));
    public float EquipEvasion => SumFloat(e => e.TotalEvasion(equipmentDataSO));

    private void Start()
    {
        currentHP = MaxHP;
        currentMP = MaxMP;
        OnStatusChanged?.Invoke();
    }

    private void OnEnable()
    {
        if (equipment != null)
            equipment.OnEquipmentChanged += OnEquipmentChanged;
    }

    private void OnDisable()
    {
        if (equipment != null)
            equipment.OnEquipmentChanged -= OnEquipmentChanged;
    }

    private void OnEquipmentChanged()
    {
        // 装備変更時は最大値まで回復（見た目も即反映）
        currentHP = MaxHP;
        currentMP = MaxMP;
        OnStatusChanged?.Invoke();
    }

    // ===== 装備合算 =====
    private int SumInt(Func<EquipmentInstance, int> selector)
    {
        if (equipment == null || equipmentDataSO == null) return 0;

        int total = 0;
        foreach (var e in equipment.GetAll())
        {
            if (e == null) continue;
            total += selector(e);
        }
        return total;
    }

    private float SumFloat(Func<EquipmentInstance, float> selector)
    {
        if (equipment == null || equipmentDataSO == null) return 0f;

        float total = 0f;
        foreach (var e in equipment.GetAll())
        {
            if (e == null) continue;
            total += selector(e);
        }
        return total;
    }
    public void HealHP(int amount)
    {
        if (amount <= 0) return;
        currentHP = Mathf.Min(currentHP + amount, MaxHP);
        OnStatusChanged?.Invoke();
    }

    public void HealMP(int amount)
    {
        if (amount <= 0) return;
        currentMP = Mathf.Min(currentMP + amount, MaxMP);
        OnStatusChanged?.Invoke();
    }
    public void AddAttackBuff(int amount, float duration)
    {
        if (amount <= 0 || duration <= 0f) return;

        if (attackBuffCoroutine != null)
            StopCoroutine(attackBuffCoroutine);

        attackBuffCoroutine = StartCoroutine(AttackBuffRoutine(amount, duration));
    }

    private IEnumerator AttackBuffRoutine(int amount, float duration)
    {
        attackBuff += amount;
        OnStatusChanged?.Invoke();

        yield return new WaitForSeconds(duration);

        attackBuff -= amount;
        attackBuff = Mathf.Max(0, attackBuff);
        OnStatusChanged?.Invoke();

        attackBuffCoroutine = null;
    }



}
