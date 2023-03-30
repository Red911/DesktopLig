using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBehaviour_Test : MonoBehaviour
{
    public int team;
    
    public GameObject projectilePrefab;
    private CharacterController _controller;
    private PlayerControls _controls;

    private bool canShoot = true;
    public float fireRate = 1f;
    private Coroutine fireCoroutine;
    private int compteurShuriken;
    
    public float timebetweeDeath = 3f;
    public bool isDead;

    [HideInInspector]public int _playerID;
    public Transform SpawnPoint;
    
    public float speed = 6;
    public float turnSmoothTime = 0.1f;
    private float _turnSmoothVelocity;
    private Vector2 _inputVector;

    private void Awake()
    {
        _controls = new PlayerControls();

        _controls.Player.Fire.started += _ => ShootShuriken();
        _controls.Player.Fire.canceled += _ => StopShoot();
    }

    private void OnEnable()
    {
        _controls.Enable();
    }

    private void OnDisable()
    {
        _controls.Disable();
    }

    void Start()
    {
        _controller = GetComponent<CharacterController>();
        
    }
    public void OnMove(InputAction.CallbackContext ctx)
    {
        _inputVector = ctx.ReadValue<Vector2>();
    }

    private void Update()
    {
        if (isDead)
        {
            isDead = false;
            StartCoroutine(KillAndResurect());
        }
    }

    void FixedUpdate()
    {
        
        Vector3 direction = new Vector3(_inputVector.x, 0f,_inputVector.y).normalized;
        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref _turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0, angle, 0);

            _controller.Move(direction * speed * Time.deltaTime);
        }
    }

    public void ShootShuriken()
    {
        if (canShoot)
        {
            fireCoroutine = StartCoroutine(LaunchProjectile());
        }
        
    }
    
    public void StopShoot()
    {
        if (fireCoroutine != null)
        {
            StopCoroutine(LaunchProjectile());
        }
    }
    
    public IEnumerator LaunchProjectile()
    {
        canShoot = false;
        compteurShuriken++;
        GameObject shuriken = Instantiate(projectilePrefab, transform.position + transform.forward, transform.rotation);
        if (compteurShuriken == 3)
        {
            shuriken.GetComponent<Boomerang>().activeBounce = true;
            compteurShuriken = 0;
        }
        shuriken.GetComponent<Boomerang>().playerOwner = gameObject;
       
        yield return new WaitForSeconds(fireRate);
        canShoot = true;

    }
    
    private IEnumerator KillAndResurect()
    {
        KillObject();
        yield return new WaitForSeconds(timebetweeDeath);
        ResurectPlayer();
    }
    private void KillObject()
    {
        // Destroy(target); //détruit l'objet du jeu
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<CharacterController>().enabled = false;
        canShoot = false;
    
    }
    
    private void ResurectPlayer()
    {
        GetComponent<MeshRenderer>().enabled = true;
        GetComponent<CharacterController>().enabled = true;
        transform.position = SpawnPoint.position;
        canShoot = true;
    }
}
