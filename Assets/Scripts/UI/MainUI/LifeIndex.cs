using System;
using UnityEngine;

class LifeIndex : MonoBehaviour
{
    [SerializeField]
    private LifeIndexItem[] items;
    private void Awake()
    {
        HEventDispatcher.GetInstance().AddEventListener(BaseHEventType.GET_WEATHER , SetIndexItem);
    }
    void SetIndexItem(BaseHEvent hEvent)
    {
        for (int i = 0; i < items.Length; i++)
        {
            items[i].Refresh(Main.instance.weather.zhishus[i]);
        }
    }
    private void OnDestroy()
    {
        HEventDispatcher.GetInstance().RemoveEventListener(BaseHEventType.GET_WEATHER , SetIndexItem);
    }
}