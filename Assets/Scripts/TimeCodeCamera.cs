
using UnityEngine;
using TMPro;
public class TimeCodeCamera : MonoBehaviour
{
    public int startHour = 0;
    public int startMinute = 0;
    public int startSecond = 0;

    private float elapsedTime = 0f;
    private int currentHour = 0;
    private int currentMinute = 0;
    private int currentSecond = 0;
    TextMeshProUGUI textTimer;
    private void Start()
    {
        textTimer = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        elapsedTime += Time.deltaTime;

        // Calcula o tempo atual com base no tempo decorrido desde o início
        currentHour = startHour + Mathf.FloorToInt(elapsedTime / 3600);
        currentMinute = startMinute + Mathf.FloorToInt(elapsedTime / 60) % 60;
        currentSecond = startSecond + Mathf.FloorToInt(elapsedTime % 60);
        
        // Exibe o time code no console
        textTimer.SetText("{0:00}:{1:00}:{2:00}", currentHour, currentMinute, currentSecond);
    }

}
