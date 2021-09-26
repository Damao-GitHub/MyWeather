using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MainUI : MonoBehaviour
{
    public Text m_AddressText;
    public Text m_TimerText;
    public Text m_TemperatureText;
    public Text m_HumidityText;
    public Image m_WeatherIcon;
    private void Awake()
    {
        HEventDispatcher.GetInstance().AddEventListener(BaseHEventType.GET_WEATHER , SetCurrent);
    }
    void SetCurrent(BaseHEvent hEvent)
    {
        if (Main.instance.CityName != Main.instance.RegionName)
            m_AddressText.text = Main.instance.CityName + "市 " + Main.instance.RegionName;
        else
            m_AddressText.text = Main.instance.CityName + "市";
        m_TimerText.text = Convert.ToDateTime(DateTime.Now).ToString("yyyy年MM月dd日" , System.Globalization.DateTimeFormatInfo.InvariantInfo);
        m_TemperatureText.text = Main.instance.weather.wendu + "℃";
        m_HumidityText.text = Main.instance.weather.shidu;
        UISystem.GetInstance().GetResourcesUI("weather/" + Main.instance.weather.forecast[0].day.type , (spr) =>
           {
               m_WeatherIcon.sprite = spr;
               m_WeatherIcon.GetComponent<RectTransform>().sizeDelta = new Vector2(spr.rect.width , spr.rect.height) * 3;
           });
    }
    private void OnDestroy()
    {
        HEventDispatcher.GetInstance().RemoveEventListener(BaseHEventType.GET_WEATHER , SetCurrent);
    }
}
