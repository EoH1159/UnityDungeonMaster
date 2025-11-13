# 유니티 개인 프로젝트 던전 만들기 입니다.

처음에 던전에 대한 인식의 차이 때문에 hp, mp 이상의 존재를 크게 가치를 두지 않았습니다...
그래도 필수 기능은 전부 하였습니다.

## 아이템 데이터
```public class ItemData : ScriptableObject
{
    [Header("기본 정보")]
    public string itemName;
    public ItemType itemType;
    public float value;
    public float duration = 5f;  // ▶︎ 버프 지속시간(초)

    [Header("시각 요소")]
    public GameObject prefab;        // 3D 모델
    

    public enum ItemType
    {
        HP_Potion,
        MP_Potion,
        Weapon,
        SpeedPotion
    }
}
```

## 동적 환경 조사
``` Ray ray = new Ray(cam.transform.position, cam.transform.forward);
 RaycastHit hit;

 if (Physics.Raycast(ray, out hit, interactRange, interactMask))
 {
     //  아이템
     if (hit.collider.CompareTag("Item"))
     {
         interactText.text = "Press [E] to pick up";
         interactText.gameObject.SetActive(true);

         if (Input.GetKeyDown(KeyCode.E))
         {
             ItemPickup pickup = hit.collider.GetComponent<ItemPickup>();
             if (pickup != null)
             {
                 pickup.PickUp();  // 아이템 습득
             }
         }
     }
```

## 아이템 사용
``` Coroutine speedCo;

 public void ApplySpeedBoost(float multiplier, float duration)
 {
     if (speedCo != null) StopCoroutine(speedCo);
     speedCo = StartCoroutine(SpeedBoostRoutine(multiplier, duration));
 }

 IEnumerator SpeedBoostRoutine(float multiplier, float duration)
 {
     _speedMultiplier = multiplier;   // 예: 1.5배
     yield return new WaitForSeconds(duration);
     _speedMultiplier = 1f;           // 원상복구
     speedCo = null;
 }
```

현재 프로젝트의 필수 기능을 완수하였으나, 
지금까지 만든 사항에서는 try-catch를 사용할 '예외'사항이 크게 없다고 판단,
넣지 않았습니다.
