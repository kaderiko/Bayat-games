using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YerdeMi : MonoBehaviour
{

    public LayerMask layer;
    public bool yerdeMiyiz;
    public Rigidbody2D rb;
    public float speed = 15f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Player.oyunBasladiMi == false)
        {
            return;
        } 


        RaycastHit2D carpiyorMu = Physics2D.Raycast(transform.position ,Vector2.down , 0.372f , layer ); //zemin ile ayak aras? mesafe 0.372f
        
        if (carpiyorMu.collider != null)
        {
            //Zemine çarp?yor
            yerdeMiyiz = true;
        }
        else
        {
           // Havaday?z
            yerdeMiyiz=false;
        }   

        //Klavyeden tu?a basma i?lemler Input s?n?f? yap?l?r. E?er tu?a bas?lmas? kontrol ediliyorsa GetKeyDown methodu kullan?l?r. Bu fonksiyon parametre KeyCode al?r. Bu parametreden . ile tüm tu?lara ula??labilir.
        if (yerdeMiyiz == true && Input.GetKeyDown(KeyCode.Space))
        {
            rb.velocity= new Vector2(rb.velocity.x , speed);
        }
    
        

    }

   
}
