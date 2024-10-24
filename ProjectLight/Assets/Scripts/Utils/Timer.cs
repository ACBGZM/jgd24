using UnityEngine;

public class Timer : MonoBehaviour
{
    private float currentTime = 0;
    public float CurrentTime => currentTime;

    private float targetTime = 0;
    public float TargetTime => targetTime;

    private bool isPaused = false;

    void Update()
    {
        if(!isPaused)
        {
            currentTime += Time.deltaTime;
        }

        if(currentTime >= targetTime)
        {
            Pause();
        }
    }

    public void Resume()
    {
        isPaused = false;
    }


    public void Pause()
    {
        isPaused = true;
    }

    public void Switch()
    {
        isPaused = !isPaused;
    }

    public void SetUp(float time)
    {
        targetTime = time;
        Resume();
    }
    public void Reset()
    {
        currentTime = 0;
        Pause();
    }
}