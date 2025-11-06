using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))  // K 키로 테스트
        {
            TakeDamage(10f);
        }
    }

    public void TakeDamage(float damage)
    {
        GameManager.Instance.ChangeHP(-damage);

        Debug.Log($"플레이어가 {damage} 데미지를 입음! 현재 HP: {GameManager.Instance.playerHP}");

        if (GameManager.Instance.playerHP <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("플레이어 사망!");
        // TODO: 사망 애니메이션, 리스폰, UI 등 나중에 연결
    }
}
