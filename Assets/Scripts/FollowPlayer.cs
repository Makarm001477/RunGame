using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform target;          // ตัว player ที่จะตาม
    public float followSpeed = 5f;    // ความเร็วในการตาม
    public float minDistance = 1f;    // ระยะห่างต่ำสุด ไม่ให้ชนกัน

    void Update()
    {
        if (target != null)
        {
            float distance = Vector2.Distance(transform.position, target.position);

            if (distance > minDistance)
            {
                Vector2 newPos = Vector2.MoveTowards(transform.position, target.position, followSpeed * Time.deltaTime);
                transform.position = newPos;

                // หันตัวตามทิศทาง
                if (newPos.x > transform.position.x)
                    GetComponent<SpriteRenderer>().flipX = false;
                else if (newPos.x < transform.position.x)
                    GetComponent<SpriteRenderer>().flipX = true;
            }
        }
    }
}

