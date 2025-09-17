using NSJ_MVVM;
using UnityEngine;
using UnityEngine.UI;

public class SpinView : BaseView
{
    private Button _spinButton;

    private bool _isInteractive = true;
    protected override void InitAwake()
    {
       
    }

    protected override void InitGetUI()
    {
      _spinButton =GetUI<Button>("SpinButton");
    }

    protected override void InitStart()
    {
       TurnManager.Instance.OnTurnStartEvent += () => SetInteractiveSpinButton(true);
    }

    protected override void SubscribeEvents()
    {
        _spinButton.onClick.AddListener(OnClickSpinButton);
    }

    private void OnClickSpinButton()
    {
        if(_isInteractive == false) return;

        TurnManager.SpinInvoke();
        SetInteractiveSpinButton(false);
    }

    private void SetInteractiveSpinButton(bool isInteractive)
    {
        _isInteractive = isInteractive;
    }
}
