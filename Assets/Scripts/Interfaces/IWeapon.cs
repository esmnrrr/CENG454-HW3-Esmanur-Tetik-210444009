using UnityEngine;

// Her silahýn (veya silah eklentisinin) uymasý gereken kurallar
public interface IWeapon
{
    void Fire(Transform cameraTransform);
}