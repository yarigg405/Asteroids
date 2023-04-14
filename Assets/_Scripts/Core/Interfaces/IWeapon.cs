
public interface IWeapon
{
    public void TryShoot();
    public void SetOwnerShipTransform(TransformInfo trInfo, Team team);
    public void Dispose();
}
