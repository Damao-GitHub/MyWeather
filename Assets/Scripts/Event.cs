using System;
using System.Collections;
using UnityEngine;

public enum BaseHEventType
{
    GET_WEATHER = 0,
}
public class BaseHEvent
{
    protected Hashtable m_arguments;
    protected BaseHEventType m_type;
    protected object m_Sender;

    public IDictionary Params
    {
        get { return this.m_arguments; }
        set { this.m_arguments = ( value as Hashtable ); }
    }

    public BaseHEventType Type
    {//事件类型 构造函数会给Type 和Sender赋值
        get { return this.m_type; }
        set { this.m_type = value; }
    }

    public object Sender
    {//物体
        get { return this.m_Sender; }
        set { this.m_Sender = value; }
    }

    public override string ToString()
    {
        return this.m_type + "[" + ( ( this.m_Sender == null ) ? "null" : this.m_Sender.ToString() ) + "]";
    }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="type"> 事件类型 </param>
    /// <param name="sender"> 物体 </param>
    public BaseHEvent(BaseHEventType type , object sender)
    {
        this.Type = type;
        Sender = sender;
        if (this.m_arguments == null)
        {
            this.m_arguments = new Hashtable();
            //Debug.LogError("this.m_arguments" + this.m_arguments.Count);
        }
    }

    public BaseHEvent(BaseHEventType type , Hashtable args , object sender)
    {
        this.Type = type;
        this.m_arguments = args;
        Sender = sender;

        if (this.m_arguments == null)
        {
            this.m_arguments = new Hashtable();
        }
    }

    public BaseHEvent Clone()
    {
        return new BaseHEvent(this.m_type , this.m_arguments , Sender);
    }
}

public delegate void HEventListenerDelegate(BaseHEvent hEvent);//定义委托用于传事件基类

public class HEventDispatcher
{
    static HEventDispatcher Instance;
    public static HEventDispatcher GetInstance()//单利
    {
        if (Instance == null)
        {
            Instance = new HEventDispatcher();
        }
        return Instance;
    }

    private Hashtable listeners = new Hashtable(); //掌控所有类型的委托事件

    public void AddEventListener(BaseHEventType type , HEventListenerDelegate listener)
    {
        HEventListenerDelegate hEventListenerDelegate = this.listeners[type] as HEventListenerDelegate;//获得之前这个类型的委托 如果第一次等于Null 
        hEventListenerDelegate = (HEventListenerDelegate)Delegate.Combine(hEventListenerDelegate , listener);//将两个委托的调用列表连接在一起,成为一个新的委托
        this.listeners[type] = hEventListenerDelegate;//赋值给哈希表中的这个类型
    }

    public void RemoveEventListener(BaseHEventType type , HEventListenerDelegate listener)
    {
        HEventListenerDelegate hEventListener = this.listeners[type] as HEventListenerDelegate;//获得之前这个类型的委托 如果第一次等于Null
        if (hEventListener != null)
        {
            hEventListener = (HEventListenerDelegate)Delegate.Remove(hEventListener , listener);//从hEventListener的调用列表中移除listener
            this.listeners[type] = hEventListener;//赋值给哈希表中的这个类型
        }
    }

    public void DispatchEvent(BaseHEvent baseH)
    {
        HEventListenerDelegate hEventListener = this.listeners[baseH.Type] as HEventListenerDelegate;
        if (hEventListener != null)
        {
            try
            {
                hEventListener(baseH);//执行委托
            }
            catch (Exception e)
            {
                throw new System.Exception(string.Concat(new string[] { "Error Dispatch event" , baseH.Type.ToString() , ":" , e.Message , " " , e.StackTrace }) , e);
            }
        }
    }
    public void RemoveAll()
    {
        this.listeners.Clear();
    }
}
// 添加事件
// HEventDispatcher.GetInstance().AddEventListener(BaseHEventType.GAME_DATA , StartHEvent);
// 分发事件
// HEventDispatcher.GetInstance().DispatchEvent(new BaseHEvent(BaseHEventType.GAME_DATA, this));
// 移除事件
// HEventDispatcher.GetInstance().RemoveEventListener(BaseHEventType.GAME_DATA, StartHEvent);
// 事件方法
// void StartDataEvent(BaseHEvent hEvent)
// {
//     Debug.Log("Doule" + hEvent.ToString());
// }