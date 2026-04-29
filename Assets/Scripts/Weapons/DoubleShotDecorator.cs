using UnityEngine;

public class DoubleShotDecorator : WeaponDecorator
{
    // Üst sýnýfýn kýlýf mantýðýný aynen alýyoruz
    public DoubleShotDecorator(IWeapon weapon) : base(weapon) { }

    public override void Fire(Transform cameraTransform)
    {
        // 1. Önce içindeki orijinal silah ateþ etsin (Normal mermi)
        base.Fire(cameraTransform);

        Debug.Log("+++ DOUBLE SHOT AKTÝF: Ekstra Mermi Gitti! +++");

        // 2. Sonra biz ekstra bir mermi daha atalým (Hafif çapraz gitsin ki çift mermi olduðu anlaþýlsýn)
        Vector3 spreadDirection = cameraTransform.forward + (cameraTransform.right * 0.05f);
        Ray extraRay = new Ray(cameraTransform.position, spreadDirection);

        if (Physics.Raycast(extraRay, out RaycastHit hit))
        {
            IDamageable target = hit.collider.GetComponent<IDamageable>();
            if (target != null) target.TakeDamage(20);
        }
    }
}