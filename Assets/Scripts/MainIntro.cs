using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainIntro : MonoBehaviour
{
    public void LaunchScene(string scene) {
        SceneManager.LoadScene(scene, LoadSceneMode.Single);
    }
}
