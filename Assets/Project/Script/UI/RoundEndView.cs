using NSJ_MVVM;
using UnityEngine;
using UnityEngine.UI;

public class RoundEndView : BaseView
{
    private Button _nextRoundButton;
    private Button _titleButton;

    protected override void InitAwake()
    {

    }

    protected override void InitGetUI()
    {
        _nextRoundButton = GetUI<Button>("NextRoundButton");
        _titleButton = GetUI<Button>("TitleButton");
    }

    protected override void InitStart()
    {
        //TODO: юс╫ц
        _titleButton.gameObject.SetActive(false);
    }

    protected override void SubscribeEvents()
    {
        _nextRoundButton.onClick.AddListener(OnClickNextRoundButton);
    }
    private void OnClickNextRoundButton()
    {
        Manager.Event.ShopStartInvoke();
    }
}
