using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    
    public GameObject player;
    private Animator ani;
    Vector3 direction;
    Rigidbody p_Rigidbody;
    float triTime = 0;
    bool alive = true;
    [Header("Attack")]
    [SerializeField] int Horizontal;
    [SerializeField] int Up;
    [SerializeField] float freAtLeast2P5;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        ani = GetComponent<Animator>();
        p_Rigidbody = player.GetComponent<Rigidbody>();
    }

    void Update()
    {
        direction = new Vector3((player.transform.position.x - transform.position.x), 0, (player.transform.position.z - transform.position.z));
        direction = direction.normalized;
        direction.x = direction.x * (0.7f);
        direction.z = direction.z * (0.7f);
    }

    void OnTriggerStay(Collider other)
    {
        triTime -= Time.fixedDeltaTime;
        if (other.gameObject == player)
        {
            if (triTime <= 0 && alive==true)
            {
                ani.SetBool("Attack", true);
                p_Rigidbody.AddForce(direction* Horizontal, ForceMode.Impulse);
                p_Rigidbody.AddForce((Vector3.up) * Up, ForceMode.Impulse);
                triTime = freAtLeast2P5;
            }
        }
    }

    public void stopAttack()
    {
        alive = false;
    }

}
