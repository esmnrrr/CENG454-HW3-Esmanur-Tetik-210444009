using UnityEngine;

public class BasicWeapon : IWeapon
{
    private int damage = 20;

    public void Fire(Transform cameraTransform)
    {
        Ray ray = new Ray(cameraTransform.position, cameraTransform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            IDamageable target = hit.collider.GetComponent<IDamageable>();
            if (target != null)
            {
                target.TakeDamage(damage);
                Debug.Log("Temel Silah ateþlendi! Vurulan: " + hit.collider.name);
            }
        }
    }
}