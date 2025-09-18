using NSJ_MVVM;
using UnityEngine.UI;

public class ShopView : BaseView
{
    private Button _nextRoundButton;
    protected override void InitAwake()
    {

    }

    protected override void InitGetUI()
    {
        _nextRoundButton = GetUI<Button>("NextRoundButton");

    }

    protected override void InitStart()
    {

    }

    protected override void SubscribeEvents()
    {
        _nextRoundButton.onClick.AddListener(OnClickNextRoundButton);
    }

    private void OnClickNextRoundButton()
    {
        Manager.Event.RoundStartInvoke();
    }
}
