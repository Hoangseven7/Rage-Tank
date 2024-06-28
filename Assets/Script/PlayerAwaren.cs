using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAwaren : MonoBehaviour
{
    public bool AwareOfPlayer {  get; private set; }
    public Vector2 DirectionToPlayer {  get; private set; }
    [SerializeField] private float _playerAwarenessDistance;
    private Transform _player;

    private void Awake()
    {
        Player player = FindAnyObjectByType<Player>();
        if (player != null)
        {
            _player = player.transform;
        }
        else
        {
            Debug.LogError("Không tìm thấy đối tượng Player trong cảnh.");
        }
    }
    void Update()
    {
        if (_player == null)
        {
            return; // Thoát khỏi hàm nếu _player là null
        }
        Vector2 enemyToPlayerVector = _player.position - transform.position;
        DirectionToPlayer = enemyToPlayerVector.normalized;

     
        if (enemyToPlayerVector.magnitude <= _playerAwarenessDistance )
        {
            AwareOfPlayer = true;
        }
        else
        {
            AwareOfPlayer= false;
        }
    }
}
