using NSJ_MVVM;
using UnityEngine;
using UnityEngine.UI;

public class RoundStartView : BaseView
{
    private Button _startButton;

    protected override void InitAwake()
    {
        
    }

    protected override void InitGetUI()
    {
       _startButton = GetUI<Button>("StartButton");
    }

    protected override void InitStart()
    {
       
    }

    protected override void SubscribeEvents()
    {
        _startButton.onClick.AddListener(OnStartButtonClicked);
    }
    private void OnStartButtonClicked()
    {
        Manager.Event.StartTurnInvoke();
        Canvas.ChangePanel(InGameCanvas.Panel.HUD);
    }
}
