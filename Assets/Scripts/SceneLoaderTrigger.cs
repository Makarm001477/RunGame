using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoaderTrigger : MonoBehaviour
{
    public string sceneName = "Newscreen"; // ชื่อซีนที่ต้องการโหลด
    public float delayBeforeLoad = 5f;

    private bool isLoading = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!isLoading && other.CompareTag("Player"))
        {
            isLoading = true;
            StartCoroutine(LoadSceneAfterDelay());
        }
    }

    IEnumerator LoadSceneAfterDelay()
    {
        // แสดงเอฟเฟกต์หรือ UI รอโหลดได้ที่นี่
        yield return new WaitForSeconds(delayBeforeLoad);
        SceneManager.LoadScene(sceneName);
    }
}
