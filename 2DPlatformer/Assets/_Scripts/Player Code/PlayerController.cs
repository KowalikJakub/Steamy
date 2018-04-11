using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class PlayerController : MonoBehaviour
{
    [Header("Player Information")]
    [SerializeField]
    public PlayerInfo info;

    void Start()
    {
        info.WeaponHolder = info.Arm;
        info.Initialize(transform);
        info.SetupDisconnectButton();
        info.InGameMenu.SetActive(false);
        info.IsControllable = true;
        info.HP = info.MaxHP;
        info.CurrentWeapon.CurrentAmmo = info.CurrentWeapon.MaxAmmo;       
    }

    void Awake()
    {
        info.Body = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(info.MenuKey))
        {
            info.InGameMenu.SetActive(!info.InGameMenu.activeSelf);
            info.IsControllable = !info.IsControllable;
        }
        if (info.IsControllable)
        {
            //Obróć rękę odpowiednio
            info.rotationMotor.RotateMousewise(info.PlayerCam, info.Arm);

            if ((Input.GetAxis("Fire1") != 0) || Input.GetKey(info.ShootKey))
            {
                info.CurrentWeapon.Shoot();
            }
            if (Input.GetKey(info.ReloadKey))
            {
                info.CurrentWeapon.Reload();
            }
        }
    }
    void FixedUpdate()
    {
        if (info.IsControllable)
        {
            info.playerMotor.Move(info);

            if (Input.GetKey(info.JumpKey))
            {
                info.playerMotor.Jump(info);
            }
            if (Input.GetKey(info.SprintKey))
            {
                info.IsSprinting = true;
                info.playerMotor.Sprint(info);
            }
            else
            {
                info.IsSprinting = false;
            }
        }
    }


  
}