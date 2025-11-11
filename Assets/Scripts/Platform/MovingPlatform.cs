using System.Collections;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    // z축(파란 화살표)을 따라 앞뒤로 움직이는 플랫폼
    [Header("Move Settings")]
    public float moveDistance = 5f;      // 앞뒤로 이동할 거리
    public float moveSpeed = 2f;         // 이동 속도
    public float waitTime = 0.5f;        // 끝점에서 잠시 멈추는 시간

    private Vector3 startPos;
    private Vector3 endPos;
    private bool playerOnPlatform = false;
    private bool isMoving = false;
    private Transform player;

    void Start()
    {
        startPos = transform.position;
        endPos = startPos + transform.forward * moveDistance; // 플랫폼의 앞 방향 기준
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerOnPlatform = true;
            player = collision.transform;
            if (!isMoving) player.SetParent(transform);
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && !isMoving)
        {
            playerOnPlatform = false;
            player.SetParent(null);
            player = null;
        }
    }

    void Update()
    {
        // E키 입력 처리
        if (playerOnPlatform && !isMoving && Input.GetKeyDown(KeyCode.E))
        {
            StartCoroutine(MoveRoutine());
        }
    }

    IEnumerator MoveRoutine()
    {
        isMoving = true;

        //  이동 중엔 항상 플레이어를 자식으로 고정
        if (player != null) player.SetParent(transform);

        // 앞으로 이동
        yield return MoveToPosition(endPos);
        yield return new WaitForSeconds(waitTime);

        // 뒤로 복귀
        yield return MoveToPosition(startPos);
        yield return new WaitForSeconds(waitTime);

        //  이동 끝나면 플레이어 내릴 수 있게 해제
        if (player != null)
            player.SetParent(null);

        isMoving = false;
    }

    IEnumerator MoveToPosition(Vector3 target)
    {
        while (Vector3.Distance(transform.position, target) > 0.05f)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, moveSpeed * Time.deltaTime);
            yield return null;
        }
    }
}
