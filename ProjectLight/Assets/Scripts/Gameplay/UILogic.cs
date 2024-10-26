using System.Collections.Generic;
using UnityEngine;

public class UILogic : MonoBehaviour
{
    [SerializeField] private List<GameObject> m_enable_panel;

    [SerializeField] private List<GameObject> m_disable_panel;

    public void ShowPanel(bool enable)
    {
        foreach (GameObject panel in m_disable_panel)
        {
            panel.SetActive(!enable);
        }

        foreach (GameObject panel in m_enable_panel)
        {
            panel.SetActive(enable);
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
    }

    public void PlayOnClickAudio()
    {
        WwiseAudioManager.GetInstance().PostEvent("ui_button_click");
    }

    public void StopBGM()
    {
        WwiseAudioManager.GetInstance().StopEvent("bgm");
    }
}
