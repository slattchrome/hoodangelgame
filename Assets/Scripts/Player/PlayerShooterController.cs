using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.InputSystem;


public class PlayerShooterController : MonoBehaviour
{
    [SerializeField]
    private CinemachineVirtualCamera aimVirtualCamera;
    [SerializeField]
    private Transform aimCamPos;
    [SerializeField]
    private LayerMask aimColliderLayerMask = new LayerMask();
    [SerializeField]
    private Transform debugTransform;
    [SerializeField]
    private Transform pfBulletProjectile;
    [SerializeField]
    private Transform spawnBulletPosition;
    [SerializeField]
    private AudioClip shootingSound;
    [SerializeField]
    private CinemachineFreeLook freeLookCamera;

    private PlayerMovement playerMovement;
    public Cinemachine.AxisState xAxis, yAxis;
    private Animator animator;


    private void Start()
    {
        xAxis = freeLookCamera.m_XAxis;
    }

    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        animator = GetComponent<Animator>();
    }


    private void Update()
    {
        xAxis.Update(Time.deltaTime);
        yAxis.Update(Time.deltaTime);

        Vector3 mouseWorldPosition = Vector3.zero;
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        Debug.DrawLine(ray.origin, ray.origin + ray.direction * 999f, Color.red);
        if (Physics.Raycast(ray, out RaycastHit raycastHit, 9999f, aimColliderLayerMask))
        {
            debugTransform.position = raycastHit.point;
            mouseWorldPosition = raycastHit.point;          
        }

        if (playerMovement.aim)
        {
            aimVirtualCamera.gameObject.SetActive(true);
            aimCamPos.localEulerAngles = new Vector3(yAxis.Value * 0.5f, aimCamPos.localEulerAngles.y, aimCamPos.localEulerAngles.z);
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, xAxis.Value * 0.5f, transform.eulerAngles.z);
            animator.SetLayerWeight(1, Mathf.Lerp(animator.GetLayerWeight(1), 1f, Time.deltaTime * 10f));

            if (playerMovement.shoot)
            {
                SoundFXManager.instance.PlaySoundFXClip(shootingSound, transform, 1f);
                Vector3 aimDir = (mouseWorldPosition - spawnBulletPosition.position).normalized;
                Instantiate(pfBulletProjectile, spawnBulletPosition.position, Quaternion.LookRotation(aimDir, Vector3.up));
                //Debug.Log(raycastHit.point);
                playerMovement.shoot = false;
            }
        }
        else
        {
            aimVirtualCamera.gameObject.SetActive(false);
            animator.SetLayerWeight(1, Mathf.Lerp(animator.GetLayerWeight(1), 0f, Time.deltaTime * 10f));
        }  
    }
}
