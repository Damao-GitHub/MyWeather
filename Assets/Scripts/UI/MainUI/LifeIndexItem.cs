using System;
using UnityEngine;
using UnityEngine.UI;
class LifeIndexItem : MonoBehaviour
{
    public Image m_Icon;
    public Text m_ItemName;
    public Text m_ItemAdvice;
    public Text m_ItemDescribe;
    public void Refresh(zhishu zhishu)
    {
        m_ItemName.text = zhishu.name;
        m_ItemAdvice.text = zhishu.value;
        m_ItemDescribe.text = zhishu.detail;
        UISystem.GetInstance().GetResourcesUI("icon/" + zhishu.name , (spr) =>
            {
                m_Icon.sprite = spr;
                m_Icon.GetComponent<RectTransform>().sizeDelta = new Vector2(spr.rect.width , spr.rect.height) * ( spr.rect.width <= 55 && spr.rect.height <= 55 ? 2 : 1 );
            });
    }
}