using UnityEngine;

// Bu sýnýf hem bir silahtýr (IWeapon), hem de içinde baþka bir silah barýndýrýr!
public abstract class WeaponDecorator : IWeapon
{
    protected IWeapon decoratedWeapon;

    // Kýlýfýn içine hangi silahý koyacaðýmýzý constructor (yapýcý) ile belirliyoruz
    public WeaponDecorator(IWeapon weapon)
    {
        this.decoratedWeapon = weapon;
    }

    // Ateþ edildiðinde, içindeki asýl silahýn ateþ kodunu çalýþtýrýr
    public virtual void Fire(Transform cameraTransform)
    {
        decoratedWeapon.Fire(cameraTransform);
    }
}