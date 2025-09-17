public abstract class Card
{
    protected RouletteController _roulette;
    protected ChipController _chip;

    public virtual void Enter() { }
    public virtual void Exit() { }
    public virtual void Update() { }

    public void SetRoulette(RouletteController roulette) => _roulette = roulette;
    public void SetChip(ChipController chip) => _chip = chip;
}
