using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankAI : MonoBehaviour
{
    [SerializeField] float _speed;
    [SerializeField] float _rotationSpeed;
    [SerializeField] float _collisionTurnSpeed; 
    [SerializeField] float _collisionCooldownTime; 
    [SerializeField] float _wallDetectionDistance = 5f; 
    [SerializeField] LayerMask _wallLayerMask; 

    private Rigidbody2D _rigidbody;
    private PlayerAwaren _playerAwaren;
    private Vector2 _targetDirection;
    private float _changeDirectionCooldown;
    private float _collisionCooldown;
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _playerAwaren = GetComponent<PlayerAwaren>();
        _targetDirection = transform.up;
        _changeDirectionCooldown = Random.Range(1f, 5f); 
        _collisionCooldown = 0;
    }

    private void FixedUpdate()
    {
        UpdateTargetDirection();
        RotateTowardsTarget();
        SetVelocity();
    }

    private void UpdateTargetDirection()
    {
        HandleRandomDirection();
        HandlePlayerTargeting();
        HandleWallAvoidance();
    }

    private void HandleWallAvoidance()
    {
        if (_collisionCooldown <= 0)
        {
            Vector2 rayDirection = transform.up;
            RaycastHit2D hit = Physics2D.Raycast(transform.position, rayDirection, _wallDetectionDistance, _wallLayerMask);
            Debug.DrawRay(transform.position, rayDirection * _wallDetectionDistance, Color.red);

            if (hit.collider != null)
            {

                if (hit.collider.CompareTag("Tuong"))
                {
                    
                    float randomTurn = Random.Range(0, 2) == 0 ? -90f : 90f;
                    StartCoroutine(TurnOverTime(randomTurn));
                    _collisionCooldown = _collisionCooldownTime; 
                }
               
            }
        }
    }


    private IEnumerator TurnOverTime(float angle)
    {
        float totalRotation = 0;
        while (Mathf.Abs(totalRotation) < Mathf.Abs(angle))
        {
            float rotationStep = Mathf.Sign(angle) * _collisionTurnSpeed * Time.deltaTime;
            transform.Rotate(0, 0, rotationStep);
            totalRotation += rotationStep;
            yield return null;
        }

        _targetDirection = transform.up;
    }

    private void HandleRandomDirection()
    {
        _changeDirectionCooldown -= Time.deltaTime;
        if (_changeDirectionCooldown <= 0)
        {
            float angleChange = Random.Range(-90f, 90f);
            Quaternion rotation = Quaternion.AngleAxis(angleChange, Vector3.forward); 
            _targetDirection = rotation * _targetDirection;

            _changeDirectionCooldown = Random.Range(1f, 5f);
        }
    }

    private void HandlePlayerTargeting()
    {
        if (_playerAwaren.AwareOfPlayer )
        {
            _targetDirection = _playerAwaren.DirectionToPlayer;
        }
    }

    private void RotateTowardsTarget()
    {
        float targetAngle = Mathf.Atan2(_targetDirection.y, _targetDirection.x) * Mathf.Rad2Deg - 90f; 
        float currentAngle = transform.eulerAngles.z;
        float newAngle = Mathf.MoveTowardsAngle(currentAngle, targetAngle, _rotationSpeed * Time.deltaTime);
        _rigidbody.MoveRotation(newAngle);
    }

    private void SetVelocity()
    {
        _rigidbody.velocity = transform.up * _speed;
    }

    private void Update()
    {
        if (_collisionCooldown > 0)
        {
            _collisionCooldown -= Time.deltaTime;
        }
    }
}
