using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class Pistol : MonoBehaviour
{
    public event Action<Target, Vector3> OnObjectHit;
    private enum GunHand { left, right };

    [SerializeField] private Vector3 bulletEffectsOffset;
    [SerializeField] private GameObject bulletEffectsPrefabs;
    [SerializeField] private Transform bulletEffectsTrans;

    [SerializeField] private GunHand gunhand;

    private void OnEnable()
    {
        if (gunhand == GunHand.left)
        {
            InputManager.Instance.playerInputActions.Shooting.ShootLeft.performed += StartShoot;
        }
        else
        {
            InputManager.Instance.playerInputActions.Shooting.ShootRight.performed += StartShoot;
        }
    }

    private void OnDisable()
    {
        if (gunhand == GunHand.left)
        {
            InputManager.Instance.playerInputActions.Shooting.ShootLeft.performed -= StartShoot;

        }
        else
        {
            InputManager.Instance.playerInputActions.Shooting.ShootRight.performed -= StartShoot;
        }

    }
    private void StartShoot(InputAction.CallbackContext context)
    {
        GameObject bulletEffects = Instantiate(bulletEffectsPrefabs, bulletEffectsTrans);
        bulletEffects.transform.Rotate(bulletEffectsOffset, Space.Self);
        bulletEffects.transform.parent = null;


        RaycastHit hit;

        if (Physics.Raycast(bulletEffectsTrans.position, bulletEffectsTrans.forward, out hit, Mathf.Infinity))
        {
            if (hit.collider.gameObject.TryGetComponent<Target>(out Target hitObect))
            {
                OnObjectHit?.Invoke(hitObect, hit.point);
            }

        }
    }

    private void StopShoot()
    {
        //particles.Stop();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
