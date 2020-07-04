using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadGame : MonoBehaviour
{
    [SerializeField] Slider LoadingSlideer;
    public int _sceneIndex = 0;
    private int ModelsQuality;
    private void Start()
    {
        ModelsQuality = PlayerPrefs.GetInt("ModelsQuality");
        _sceneIndex = ModelsQuality + 3;
        StartCoroutine(LoadAsyns());
    }

    private IEnumerator LoadAsyns()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(_sceneIndex);
        while (!operation.isDone)
        {
            float _loadProgress = Mathf.Clamp01(operation.progress / 0.9f);
            LoadingSlideer.value = _loadProgress;
            yield return null;
        }
    }
}
