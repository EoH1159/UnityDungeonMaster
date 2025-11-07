using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public float playerHP = 100f;
    public float playerMP = 50f;
    public float maxHP = 100f;
    public float maxMP = 50f;

    private void Awake()
    {
        // 싱글톤 패턴: 중복 생성 방지
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void ChangeHP(float amount)
    {
        playerHP = Mathf.Clamp(playerHP + amount, 0, maxHP);
         Debug.Log($" HP: {playerHP}/{maxHP}");
    }

    public void ChangeMP(float amount)
    {
        playerMP = Mathf.Clamp(playerMP + amount, 0, maxMP);
        Debug.Log($" MP: {playerMP}/{maxMP}");
    }
}
