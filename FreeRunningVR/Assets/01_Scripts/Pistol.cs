using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;
using static Unity.VisualScripting.Member;

public class Pistol : MonoBehaviour
{
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
            Debug.DrawRay(bulletEffectsTrans.position, bulletEffectsTrans.forward * hit.distance, Color.green);
            Debug.Log("Hit");
            hit.collider.gameObject.SetActive(false);
        }
        else
        {
            Debug.DrawRay(bulletEffectsTrans.position, bulletEffectsTrans.forward * 1000, Color.red);
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
