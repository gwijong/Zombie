using UnityEngine;

public interface IDamageble 
{
    void onDamageble(float damage, Vector3 hitPoint, Vector3 hitNormal);
}
