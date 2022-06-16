using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantAttack : EnemyCombat
{
    [SerializeField]
    private GameObject _bullet;
    public GameObject _container;
    public Attack currentAttack;

    public bool CanAttack;

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
        CanAttack = true;
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
            CanAttack = false;
            animator.SetBool("Dying", true);
            ParticleInstancer.Instance.StartParticles("PlantaDie_Particle", transform);
            dying = false;
            if (IsInvoking("InkAttack")) CancelInvoke("InkAttack");
            StartCoroutine(DeathDelay(1.9f));
            GetComponent<Rigidbody2D>().isKinematic = true;
            GetComponent<PolygonCollider2D>().isTrigger = true;
            MusicPlayer.Instance.PlayFX("Plant_Die", 1.0f);
        }
    }

    void InkAttack()
    {
        if (!CanAttack) return;
        if (dying == true) return;
        Vector2 init = new Vector2(transform.position.x + (transform.right.x * -0.3f), transform.position.y + 0.8f);
        GameObject bullet = Instantiate(_bullet, init, Quaternion.identity, _container.transform);
        bullet.GetComponent<PlantInk>().Direction(-transform.right.x);
        currentTime = 0;
        attacking = false;
        animator.SetBool("Attack", false);
        animator.SetBool("Idle", true);
        MusicPlayer.Instance.PlayFX("Enemy_PlantaAtk/Enemy_PlantaAtk_" + ((int)Random.Range(1, 3)).ToString(), 0.5f);
    }
}
