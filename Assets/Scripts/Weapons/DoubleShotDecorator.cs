using UnityEngine;

public class DoubleShotDecorator : WeaponDecorator
{
    // Üst sýnýfý aliyoruz ve sadece ekstra bir mermi atma iþlevi ekliyoruz
    public DoubleShotDecorator(IWeapon weapon) : base(weapon) { }

    public override void Fire(Transform cameraTransform)
    {
        // 1. Önce içindeki orijinal silah ateþ etsin
        base.Fire(cameraTransform);

        Debug.Log("+++ DOUBLE SHOT AKTÝF: Ekstra Mermi Gitti! +++");

        // 2. Sonra ekstra bir mermi daha atiyoruz, biraz saða kayarak
        Vector3 spreadDirection = cameraTransform.forward + (cameraTransform.right * 0.05f);
        Ray extraRay = new Ray(cameraTransform.position, spreadDirection);

        if (Physics.Raycast(extraRay, out RaycastHit hit))
        {
            IDamageable target = hit.collider.GetComponent<IDamageable>();
            if (target != null) target.TakeDamage(20);
        }
    }
}