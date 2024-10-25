using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections.Generic;

public class GameSceneManager : MonoBehaviour
{
    [SerializeField] Slider m_progress_bar;
    [SerializeField] TMP_Text m_progerss_text;

    [SerializeField] GameObject m_loading;

    [SerializeField] List<GameObject> m_disable_while_loading;

    private void Awake()
    {
        //DontDestroyOnLoad(gameObject);
    }

    public void LoadSync(string target_scene)
    {
        foreach(GameObject panel in m_disable_while_loading)
        {
            panel.SetActive(false);
        }

        SceneManager.LoadScene(target_scene);
    }

    public void LoadAsync(string target_scene)
    {
        foreach (GameObject panel in m_disable_while_loading)
        {
            panel.SetActive(false);
        }

        StartCoroutine(LoadSceneAsync(target_scene));
    }

    IEnumerator LoadSceneAsync(string target_scene)
    {
        m_loading.SetActive(true);
        AsyncOperation async_load = SceneManager.LoadSceneAsync(target_scene);

        async_load.allowSceneActivation = false;

        float fake_progress = 0;
        while (!async_load.isDone)
        {
            fake_progress += Time.deltaTime * 0.3f;
            float progress = Mathf.Clamp01(async_load.progress / 0.9f);
            m_progerss_text.text = fake_progress.ToString("p2");

            m_progress_bar.value = fake_progress;
            if (fake_progress >= 1f && progress >= 1f)
            {
                async_load.allowSceneActivation = true;
            }

            yield return null;
        }
    }
}
