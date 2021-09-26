using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FutureWeatherItem : MonoBehaviour
{
    public Text m_TimerText;
    public Text m_MaxTemperatureText;
    public Text m_MinTemperatureText;
    public Text m_DayWeatherText;
    public Text m_DayWindText;
    public Text m_NightWeatherText;
    public Text m_NightWindText;
    public void Refresh(weather weather)
    {
        m_TimerText.text = weather.date;
        m_MaxTemperatureText.text = "最" + weather.high.Replace(" " , "度 ");
        m_MinTemperatureText.text = "最" + weather.low.Replace(" " , "度 ");
        m_DayWeatherText.text = "天气:" + weather.day.type;
        m_DayWindText.text = weather.day.fengxiang + ":" + weather.day.fengli;
        m_NightWeatherText.text = "天气:" + weather.night.type;
        m_NightWindText.text = weather.night.fengxiang + ":" + weather.night.fengli;
    }
}
