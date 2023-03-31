using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    
    public GameObject projectilePrefab;
    private Animator _animator;
    private int isMovingHash;
    private Rigidbody rb;
    private PlayerInputHandler _playerInputHandler;

   public int _playerID;
    
    public float speed = 20;
    public float turnSmoothTime = 0.1f;
    private float _turnSmoothVelocity;
    private Vector2 _inputVector;

    [Header("Shuriken")] 
    public Transform ShurikenSpawn;
    private bool canShoot = true;
    public float fireRate = 1f;
    private Coroutine fireCoroutine;
    private int compteurShuriken;
    
    [Header("Dead")]
    public float timebetweeDeath = 3f;
    public bool isDead;
    private InitialiseLevel _initialiseLevel;

    void Start()
    {
        _animator = GetComponentInChildren<Animator>();
        isMovingHash = Animator.StringToHash("isMoving");

        rb = GetComponent<Rigidbody>();
        _playerInputHandler = GetComponent<PlayerInputHandler>();
        _initialiseLevel = GameObject.Find("LevelInitializer").GetComponent<InitialiseLevel>();
    }
    
    public void SetInputVector(Vector2 direction)
    {
        _inputVector = direction;
    }
    
    // Update is called once per frame
    void Update()
    {
        Vector3 direction = new Vector3(_inputVector.x, 0f,_inputVector.y).normalized;
        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref _turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0, angle, 0);
            
            rb.MovePosition(transform.position + direction * speed * Time.deltaTime);
        }
        
        _animator.SetBool(isMovingHash, direction.magnitude >= 0.1f);

        if (isDead)
        {
            isDead = false;
            StartCoroutine(KillAndResurect());
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
        GameObject shuriken = Instantiate(projectilePrefab, ShurikenSpawn.position, transform.rotation);
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
        GetComponent<CapsuleCollider>().enabled = false;
        canShoot = false;
    
    }
    
    private void ResurectPlayer()
    {
        GetComponent<MeshRenderer>().enabled = true;
        GetComponent<CapsuleCollider>().enabled = true;

        if (_playerInputHandler.whichTeam == 1)
        {
            transform.position = _initialiseLevel.teamOnePlayerSpawns[_playerID].position;
        }
        else if (_playerInputHandler.whichTeam == 2)
        {
            transform.position = _initialiseLevel.teamTwoPlayerSpawns[_playerID].position;
        }
        canShoot = true;
    }
}
