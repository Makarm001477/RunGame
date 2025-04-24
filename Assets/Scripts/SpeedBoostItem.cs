using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Platformer.Mechanics;

public class SpeedBoostItem : MonoBehaviour
{
    public float speedBoostAmount = 3f;
    public float duration = 5f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        var player = other.GetComponent<PlayerController>();
        if (player != null)
        {
            StartCoroutine(ApplySpeedBoost(player));
            CollectItem(); // เรียกฟังก์ชันเก็บไอเท็มใน ItemCollector
            Destroy(gameObject); // ลบไอเท็มหลังจากเก็บ
        }
    }

    IEnumerator ApplySpeedBoost(PlayerController player)
    {
        player.maxSpeed += speedBoostAmount;
        yield return new WaitForSeconds(duration);
        player.maxSpeed -= speedBoostAmount;
    }

    // ฟังก์ชันที่เรียกใช้เพื่อเก็บไอเท็ม
    void CollectItem()
    {
        ItemCollector collector = FindObjectOfType<ItemCollector>();
        if (collector != null)
        {
            collector.CollectItem(gameObject); // เรียกเก็บไอเท็มใน ItemCollector
        }
    }
}
