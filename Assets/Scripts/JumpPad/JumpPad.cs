
using UnityEngine;

public class JumpPad : MonoBehaviour
{
    [Header("Jump Settings")]
    public float jumpPower = 400f; // Impulse에 쓰일 힘
    public Vector3 direction = new Vector3(0, 1, 0.5f); // 위 + 살짝 앞으로
    public bool usePadRotation = true; // 점프대 방향 그대로 쓸지

    [Header("Target Settings")]
    public string targetTag = "Player"; // 플레이어만 반응

    private void OnTriggerEnter(Collider other)
    {
        // 플레이어만 반응하도록 필터링
        if (!other.CompareTag(targetTag)) return;

        Rigidbody rb = other.attachedRigidbody;
        if (rb == null) return;

        // 방향 계산
        Vector3 launchDir = usePadRotation ? transform.up : direction.normalized;

        // Impulse로 한 번에 튕기기
        rb.AddForce(launchDir * jumpPower, ForceMode.Impulse);

        Debug.Log($" JumpPad activated! Force: {jumpPower}, Dir: {launchDir}");
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Vector3 dir = usePadRotation ? transform.up : direction.normalized;
        Gizmos.DrawRay(transform.position + Vector3.up * 0.5f, dir * 2f);
    }
}
