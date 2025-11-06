using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    [Header("HP / MP Fill Images")]
    [SerializeField] private Image hpFill;
    [SerializeField] private Image mpFill;

    [Header("HP / MP Text")]
    [SerializeField] private Text hpText;
    [SerializeField] private Text mpText;

    private void Update()
    {
        float hp = GameManager.Instance.playerHP;
        float mp = GameManager.Instance.playerMP;
        float maxHP = GameManager.Instance.maxHP;
        float maxMP = GameManager.Instance.maxMP;

        hpFill.fillAmount = hp / maxHP;
        mpFill.fillAmount = mp / maxMP;

        hpText.text = Mathf.RoundToInt((hp / maxHP) * 100f) + "%";
        mpText.text = Mathf.RoundToInt((mp / maxMP) * 100f) + "%";
    }
}
