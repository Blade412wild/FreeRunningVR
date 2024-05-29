using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using static Unity.VisualScripting.Member;

public class Pistol : MonoBehaviour
{
    [SerializeField] private Vector3 bulletEffectsOffset;
    [SerializeField] private GameObject bulletEffectsPrefabs;
    [SerializeField] private Transform bulletEffectsTrans;


    // Start is called before the first frame update
    void Start()
    {
        XRGrabInteractable grabInteractable = GetComponent<XRGrabInteractable>();
        grabInteractable.activated.AddListener(x => StartShoot());
        grabInteractable.deactivated.AddListener(x => StopShoot());
    }

    private void StartShoot()
    {
        GameObject bulletEffects = Instantiate(bulletEffectsPrefabs, bulletEffectsTrans.position, transform.localRotation);
        bulletEffects.transform.Rotate(bulletEffectsOffset, Space.Self);
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
