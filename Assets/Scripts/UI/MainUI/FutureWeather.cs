using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FutureWeather : MonoBehaviour
{
    [SerializeField]
    private FutureWeatherItem[] items;
    private void Awake()
    {
        HEventDispatcher.GetInstance().AddEventListener(BaseHEventType.GET_WEATHER , SetIndexItem);
    }
    void SetIndexItem(BaseHEvent hEvent)
    {
        for (int i = 0; i < items.Length; i++)
        {
            items[i].Refresh(Main.instance.weather.forecast[i]);
        }
    }
    private void OnDestroy()
    {
        HEventDispatcher.GetInstance().RemoveEventListener(BaseHEventType.GET_WEATHER , SetIndexItem);
    }
}
