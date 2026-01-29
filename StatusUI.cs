using TMPro;
using UnityEngine;

public class StatusUI : MonoBehaviour
{
    [SerializeField] private PlayerStatus status;

    [Header("Texts")]
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private TextMeshProUGUI expText;
    [SerializeField] private TextMeshProUGUI hpText;
    [SerializeField] private TextMeshProUGUI mpText;
    [SerializeField] private TextMeshProUGUI attackText;
    [SerializeField] private TextMeshProUGUI defenseText;
    [SerializeField] private TextMeshProUGUI criticalText;

    private void OnEnable()
    {
        status.OnStatusChanged += Refresh;
        Refresh();
    }

    private void OnDisable()
    {
        status.OnStatusChanged -= Refresh;
    }

    private void Refresh()
    {
        levelText.text = $"Lv {status.Level}";
        expText.text = $"EXP {status.CurrentExp} / {status.ExpToNextLevel}";

        hpText.text = $"HP {status.CurrentHP} / {status.MaxHP} (+{status.EquipHP})";
        mpText.text = $"MP {status.CurrentMP} / {status.MaxMP} (+{status.EquipMP})";

        attackText.text = $"ATK {status.Attack} (+{status.EquipAttack})";
        defenseText.text = $"DEF {status.Defense} (+{status.EquipDefense})";

        criticalText.text =
            $"CRIT {status.CriticalRate:P1} (+{status.EquipCritical:P1})";
    }
}
