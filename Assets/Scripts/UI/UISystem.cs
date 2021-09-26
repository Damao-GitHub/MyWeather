using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class UISystem : MonoBehaviour
{
    private static UISystem instance;
    private Stack<UIPanel> m_BaseUIStack = new Stack<UIPanel>();
    private Stack<UIPanel> m_MiddleUIStack = new Stack<UIPanel>();
    private Stack<UIPanel> m_PopUIStack = new Stack<UIPanel>();
    private Stack<UIPanel> m_TopUIStack = new Stack<UIPanel>();
    private Dictionary<string , UIPanel> uiPanelDic = new Dictionary<string , UIPanel>();

    public static UISystem GetInstance()
    {
        if(instance == null)
        {
            GameObject obj = new GameObject("UISystem");
            instance = obj.AddComponent<UISystem>();
            instance.Init();
        }
        return instance;
    }
    private void Init()
    {

    }
    public void ShowBaseUI(string path , Action<Transform> action = null)
    {
        Transform panel = null;
        //if(uiPanelDic.ContainsKey(path))
        //    panel = Instantiate()
        // LoadResoures<UnityEngine.Sprite>(path , action)
    }
    public void ShowMiddleUI(string path , Action<Transform> action = null)
    {

    }
    public void ShowPopUI(string path , Action<Transform> action = null)
    {

    }
    public void ShowTopUI(string path , Action<Transform> action = null)
    {

    }
    public void CloseUI(string path)
    {

    }

    public void ShowTip(ShowTipType type , string msg , Action onClickCancel = null , Action onClickOk = null)
    {

    }

    public void GetResourcesUI(string path , Action<Sprite> action)
    {
        StartCoroutine(LoadResourcesSpr(path , action));
    }
    public IEnumerator LoadResoures<T>(string path,Action<object> action)
    {
        string HotFixPath = "";
#if UNITY_EDITOR
        HotFixPath = Application.streamingAssetsPath + "/UI/" + path + ".png";
#elif UNITY_ANDROID || UNITY_IOS
        HotFixPath = Application.persistentDataPath + "/UI/" + path + ".png";
#endif
        AssetBundleCreateRequest budleRequest = AssetBundle.LoadFromFileAsync(HotFixPath);
        yield return budleRequest;
    }
    public IEnumerator LoadResourcesSpr(string path , Action<Sprite> action)
    {
        string HotFixPath = "";
#if UNITY_EDITOR
        HotFixPath = Application.streamingAssetsPath + "/UI/" + path + ".png";
#elif UNITY_ANDROID || UNITY_IOS
        HotFixPath = Application.persistentDataPath + "/UI/" + path + ".png";
#endif
        Sprite spr = null;
        if (!File.Exists(HotFixPath))
        {
            spr = Resources.Load<Sprite>("UI/" + path);
            //if (spr == null) Debug.Log("[GetResourcesUI]" + path + "不存在");
        }
        else
        {
            WWW www = new WWW(HotFixPath);
            yield return www;
            Texture2D t2d = www.texture;
            spr = Sprite.Create(t2d , new Rect(0 , 0 , t2d.width , t2d.height) , Vector2.zero);
        }
        action?.Invoke(spr);
    }
}
public enum ShowTipType
{
    OK,
    CANCEL,
    OKANDCANCEL,
}
public class UIPanel
{
    public string path;
    public Transform transform;
}