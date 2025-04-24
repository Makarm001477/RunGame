using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemCollector : MonoBehaviour
{
    public TMP_Text itemCountText; // การแสดงผลจำนวนไอเท็ม
    public GameObject[] itemsToCollect; // ไอเท็มทั้งหมดที่ต้องเก็บ
    public GameObject obstacle; // สิ่งกีดขวางที่จะหายไป

    private int collectedItems = 0;

    void Start()
    {
        UpdateItemUI(); // อัปเดต UI ตอนเริ่มเกม
    }

    // ฟังก์ชันที่เรียกใช้เมื่อเก็บไอเท็ม
    public void CollectItem(GameObject item)
    {
        for (int i = 0; i < itemsToCollect.Length; i++)
        {
            if (itemsToCollect[i] == item)
            {
                collectedItems++;
                Debug.Log("Item Collected: " + collectedItems); // ตรวจสอบใน Console
                UpdateItemUI();
                break;
            }
        }

        // ถ้าเก็บครบ 10 ไอเท็ม ให้ลบสิ่งกีดขวาง
        if (collectedItems >= 10)
        {
            RemoveObstacle(); // ลบสิ่งกีดขวางเมื่อเก็บครบ 10
        }
    }

    // อัปเดต UI เมื่อเก็บไอเท็ม
    void UpdateItemUI()
    {
        itemCountText.text = $"{collectedItems}/{itemsToCollect.Length}";
        Debug.Log("Item Count Updated: " + itemCountText.text); // ตรวจสอบใน Console
    }

    // ฟังก์ชันที่จะลบสิ่งกีดขวาง
    void RemoveObstacle()
    {
        if (obstacle != null)
        {
            obstacle.SetActive(false); // ทำให้สิ่งกีดขวางหายไป
            Debug.Log("Obstacle Removed!");
        }
    }
}
