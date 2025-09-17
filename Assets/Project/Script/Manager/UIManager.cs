using NSJ_MVVM;
using System;
using UnityEngine;
using Utility;

public class UIManager : SingleTon<UIManager>
{
    private BaseCanvas _curCanvas;

    protected override void InitAwake()
    {
        
    }

    public static void SetCanvas(BaseCanvas canvas)
    {
        Instance._curCanvas = canvas;
    }   

    public static void ChangePanel<TEnum>(TEnum panel) where TEnum : Enum
    {
        if (Instance._curCanvas == null)
        {
            Debug.LogError("UIManager: Current canvas is not set.");
            return;
        }
        Instance._curCanvas.ChangePanel(panel);
    }
    public static void ChangePanel(string name)
    {
        if (Instance._curCanvas == null)
        {
            Debug.LogError("UIManager: Current canvas is not set.");
            return;
        }
        Instance._curCanvas.ChangePanel(name);
    }
}
