using UnityEngine;

public class EnemyReset : MonoBehaviour
{
    // กำหนดจุดเริ่มต้น
    public Vector3 startPosition = new Vector3(0, 0, 0); // หรือสามารถใช้ transform.position ของ GameObject ที่เริ่มต้น

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy")) // ตรวจสอบว่า GameObject ที่ชนมี tag เป็น "Enemy"
        {
            // ย้าย Enemy ไปที่จุดเริ่มต้น
            other.transform.position = startPosition;

            // ตั้งค่า Rigidbody2D ของ Enemy ใหม่เพื่อให้มันหยุดเคลื่อนไหว
            Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                // ตั้งให้มันไม่มีความเร็วตอนเริ่ม
                rb.velocity = Vector2.zero;

                // ทำให้ Rigidbody2D "หลับ" และปลุกมัน
                rb.Sleep();
                rb.WakeUp();
            }
        }
    }
}
