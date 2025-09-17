using NSJ_MVVM;
using UnityEngine;
using UnityEngine.UI;

public class SpinView : BaseView
{
    private Button _spinButton;
    protected override void InitAwake()
    {
       
    }

    protected override void InitGetUI()
    {
      _spinButton =GetUI<Button>("SpinButton");
    }

    protected override void InitStart()
    {

    }

    protected override void SubscribeEvents()
    {
        _spinButton.onClick.AddListener(OnClickSpinButton);
    }

    private void OnClickSpinButton()
    {
       Manager.Turn.CommandSpinInvoke();
    }
}
