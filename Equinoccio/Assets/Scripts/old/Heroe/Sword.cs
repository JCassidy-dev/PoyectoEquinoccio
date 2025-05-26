using UnityEngine;

public class Sword : MonoBehaviour
{
   
    public int damage;
    void Start()
    {
        damage = 1;
       
    }

    // Update is called once per frame
    void Update()
    {
;
    }

    public int getAttack()
    {
        return damage;
    }


}
