using UnityEngine;

public class HitHeroe : MonoBehaviour
{   

    int damage = 1;
    Transform transform;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        transform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Heroe"))
        {
            float x = transform.position.x;
            collision.gameObject.GetComponent<HeroeController>().ApplyDamage(x, damage);
        }
    }

    public HitHeroe()
    {

    }
}
