using UnityEngine;
using TMPro;
using System.Collections;

public class PlayerEnemyCollision : MonoBehaviour
{
    public GameObject objectToDisable;  // เลือก GameObject ที่ต้องการ disable ใน Inspector
    public TextMeshProUGUI cooldownText;  // ตัวแปร TextMeshPro สำหรับแสดงข้อความ
    public float disableDuration = 3f;  // เวลาที่จะ disable object (3 วิ)
    public float enemyCooldown = 5f;    // คูลดาวน์ของ enemy (5 วิ)

    private bool isCooldownActive = false;
    private float cooldownTimer = 0f;

    void Update()
    {
        // เช็คคูลดาวน์ของ enemy
        if (isCooldownActive)
        {
            cooldownTimer -= Time.deltaTime;
            cooldownText.text = "Enemy Cooldown: " + Mathf.Max(0, Mathf.Ceil(cooldownTimer)) + "s";
            if (cooldownTimer <= 0)
            {
                isCooldownActive = false;
                cooldownText.text = "";
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // ตรวจสอบการชนกับ Enemy
        if (other.CompareTag("Enemy") && !isCooldownActive)
        {
            StartCoroutine(DisableObjectAndShowMessage());
            isCooldownActive = true;
            cooldownTimer = enemyCooldown;
        }
    }

    private IEnumerator DisableObjectAndShowMessage()
    {
        // Disable object
        objectToDisable.SetActive(false);

        // แสดงข้อความใน TextMeshPro
        cooldownText.text = "Player Disabled for " + disableDuration + "s";

        // รอ 3 วิ
        yield return new WaitForSeconds(disableDuration);

        // เปิดใช้งาน object ใหม่
        objectToDisable.SetActive(true);

        // ลบข้อความ
        cooldownText.text = "";
    }
}
