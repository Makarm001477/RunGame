using System.Collections;
using System.Collections.Generic;
using TMPro;// ต้องใช้สำหรับ TextMeshPro
using UnityEngine;
using UnityEngine.SceneManagement; // สำหรับโหลด Scene

public class CountdownTimer : MonoBehaviour
{
    public float startTime = 60f;
    private float currentTime;

    public TMP_Text countdownText;
    public string sceneToLoad = "GameOverScene"; // ชื่อ Scene ที่จะโหลดเมื่อหมดเวลา

    private bool isRunning = true;

    void Start()
    {
        currentTime = startTime;
    }

    void Update()
    {
        if (isRunning)
        {
            currentTime -= Time.deltaTime;

            if (currentTime <= 0)
            {
                currentTime = 0;
                isRunning = false;
                OnTimerEnd();
            }

            UpdateTimerDisplay();
        }
    }

    void UpdateTimerDisplay()
    {
        int minutes = Mathf.FloorToInt(currentTime / 60);
        int seconds = Mathf.FloorToInt(currentTime % 60);
        countdownText.text = string.Format("Time: {0:00}:{1:00}", minutes, seconds);
    }

    void OnTimerEnd()
    {
        Debug.Log("⏰ Time's up!");
        SceneManager.LoadScene(sceneToLoad); // โหลด Scene ที่กำหนด
    }
}
