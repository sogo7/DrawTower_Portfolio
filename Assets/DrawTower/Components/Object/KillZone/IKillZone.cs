using R3;

namespace DrawTower.Object
{
    public interface IKillZone
    {
        Observable<bool> OnBlockOutOfBounds();
    }
}