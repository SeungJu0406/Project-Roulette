using NSJ_MVVM;

public class InGameCanvas : BaseCanvas
{
    public override int DefaultPanelIndex => (int)Panel.RoundStart;
    public enum Panel
    {
        HUD,
        RoundStart,
        RoundEnd,
        Shop
    }
}
