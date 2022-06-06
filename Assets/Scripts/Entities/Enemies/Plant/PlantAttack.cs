using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantAttack : EnemyCombat
{
    [SerializeField]
    private GameObject _bullet;
    public GameObject _container;
    public Attack currentAttack;

    private PlayerCombat _playerCombat;
    private PlantAI plantAI;
    private float currentTime;
    private float maxTime = 1.5f;
    private Animator animator;

    private bool attacking;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        var direction = transform.right;
        Gizmos.DrawRay(transform.position, direction * 5);
    }

    private void Awake()
    {
        attacking = false;
        _container = GameObject.FindObjectOfType<ElementsContainer>().gameObject;
    }

    // Start is called before the first frame update
    void Start()
    {
        plantAI = GetComponent<PlantAI>();
        _playerCombat = GetComponent<PlayerCombat>();
        currentTime = 0;
        _health = 5;
        animator = GetComponentInParent<Animator>();
        animator.SetBool("Attack", false);
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;
        if ((currentTime > maxTime) && (!attacking))
        {   
            if(plantAI.Detected && dying == false)
            {
                attacking = true;
                animator.SetBool("Attack", true);
                Invoke("InkAttack", 1.15f);
            }
        }

        if (dying)
        {
            animator.SetBool("Dying", true);
            if (IsInvoking("InkAttack")) CancelInvoke("InkAttack");
            StartCoroutine(DeathDelay(1.9f));
        }
    }

    void InkAttack()
    {
        Vector2 init = new Vector2(transform.position.x + 0.1f, transform.position.y + 0.5f);
        GameObject bullet = Instantiate(_bullet, init, Quaternion.identity, _container.transform);
        bullet.GetComponent<PlantInk>().Direction(-transform.right.x);
        currentTime = 0;
        attacking = false;
        animator.SetBool("Attack", false);
        animator.SetBool("Idle", true);
    }
}
