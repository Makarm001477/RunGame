using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneButton : MonoBehaviour
{
    public string sceneName;

    public void LoadScene()
    {
        Debug.Log("กำลังโหลดซีน: " + sceneName);
        SceneManager.LoadScene(sceneName);
    }
}
