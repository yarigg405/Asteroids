
using Newtonsoft.Json.Bson;

public interface IWeapon
{
    public void TryShoot();
    public void SetOwnerShipTransform(TransformInfo info, Team team);
    public void Dispose();
}
