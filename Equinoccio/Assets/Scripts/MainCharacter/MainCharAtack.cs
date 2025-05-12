using UnityEngine;
using UnityEngine.Rendering;

public class MainCharAtack : MonoBehaviour
{
    public bool canAtackMelee;
    public bool canAtackRngd;
    public float lastAtackTimeMelee;
    public float lastAtackTimeRngd;
    public float cooldownMeleeAtk;
    public float cooldownRngdAtk;
    public Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        canAtackMelee = true;
        canAtackRngd = true;
        cooldownMeleeAtk = 0.5f;
        cooldownRngdAtk = 0.5f;
        lastAtackTimeMelee = Time.time;
        lastAtackTimeRngd = Time.time;
    }

    private void Update()
    {
        if (Time.time > lastAtackTimeMelee + cooldownMeleeAtk)
        {
            canAtackMelee = true;
        }
        if (Time.time > lastAtackTimeRngd + cooldownRngdAtk)
        {
            canAtackRngd = true;
        }
    }

    public void Attack(string atack)
    {
        switch (atack)
        {
            case "AtkM":
                if (canAtackRngd)
                {
                    if (canAtackMelee)
                    {
                        animator.SetTrigger("meleeAtack");
                        lastAtackTimeMelee = Time.time;
                        canAtackMelee = false;
                    }
                }
                break;
            case "AtkSp":
                if (canAtackRngd)
                {
                    if (canAtackMelee)
                    {
                        animator.SetTrigger("spearAtack");
                        lastAtackTimeMelee = Time.time;
                        canAtackMelee = false;
                    }
                }
                break;
            case "AtkRg":
                if (canAtackRngd)
                {
                   
                        animator.SetTrigger("rangedAtack");
                        lastAtackTimeRngd = Time.time;
                        canAtackRngd = false;
                    
                }
                break;
        }
    }
}
