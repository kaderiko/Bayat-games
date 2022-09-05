using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Player : MonoBehaviour
{
    //Ba??na public ya da  [SerializeField] denir.
    [SerializeField] //Ba?ka dosyalardan ula??lmaz sadece unity arayüzünden ula??l?r.

    float speed = 10f;

  

    //Bir componente kod ile ula?mak 

    [SerializeField]
    Rigidbody2D rb;



    public Animator anim;
    bool kosuyorMu = false;

    int score = 0;

    [SerializeField]
    Text scoreText;


    [SerializeField]
    GameObject YenidenOynaPanel;


    [SerializeField]
    Text panelScoreText;

   public static bool oyunBasladiMi = false;

    [SerializeField]
    GameObject baslangicPanel;

    [SerializeField]
    AudioSource coinses;


    private void Awake()
    {
        //rb = GetComponent<Rigidbody2D>();
        //anim = GetComponent<Animator>();
    }



    private void FixedUpdate()
    {
        if (oyunBasladiMi == false)
        {
            return;
        }
        float horizontal = Input.GetAxis("Horizontal");
        //print(horizontal);


      //yürüme 
      move(horizontal);


        //Animasyon
       animasyon(horizontal);


        //Yön verme
       turnMove(horizontal);
    }


    //Yürüme
    void move (float horizontal)
    {
        
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
    }

    //Animasyon
    void animasyon(float horizontal)
    {
        if (horizontal != 0)
        {
            kosuyorMu = true;
        }
        else
        {
            kosuyorMu = false;
        }
        anim.SetBool("kosuyorMu", kosuyorMu);
    }


    //Yön verme
    void turnMove(float horizontal)
    {
        if (horizontal > 0)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
        else if(horizontal < 0)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
    }






    // Start is called before the first frame update
    void Start()
    {
        if (GameManager.isRestart == true)
        {
            baslangicPanel.SetActive(false);
        }
        scoreText.text = score.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    //?çinden geçmeden
    //onCollisionEnter2D: Çarp??man?n ilk an?n? verir bize.
    //onCollisionExit2D: Çarp??man?n bitti?i an? verir bize.
    //onCollisionStay2D: Çarp??ma devam etti?i sürece


    //?çinden geçerken
    //onTriggerEnter2D: Çarp??man?n ilk an?n? verir bize.
    //onTriggerExit2D: Çarp??man?n bitti?i an? verir bize.
    //onTriggerStay2D: Çarp??ma devam etti?i sürece


    private void OnTriggerEnter2D(Collider2D collision)
    {

       
        //Alt?na çarpan yer
        if (collision.CompareTag("Coin"))
        {
            coinses.Play();
            scoreCounter(collision , 1);

        }
        //Dü?mana çarpan yer
        else if (collision.CompareTag("Enemy"))
        {
            death();
        }

        else if (collision.CompareTag("SuperStar"))
        {
            coinses.Play();
            scoreCounter(collision, 5);
        }


        
        else if (collision.CompareTag("Door"))
        {
            string level = SceneManager.GetActiveScene().name;
            if (level == "Level2")
            {
                SceneManager.LoadScene("Level1");
            }
            else
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
            
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent("Death"))
        {
            death();
        }
    }

    void scoreCounter(Collider2D collision, int puan)
    {
        score += puan;
        Destroy(collision.gameObject);
        //Bir obejeyi yok etmek için Destroy fonksiyonu kullan?lmal?.
        scoreText.text = score.ToString();
    }

    void death()
    {
        //transform.position = new Vector3(transform.position.x - 0.8f, transform.position.y, transform.position.z);
        transform.Rotate(new Vector3(0, 0, 90));
        Destroy(this.gameObject);
        YenidenOynaPanel.SetActive(true);
        panelScoreText.text="Score " + score.ToString();
    }
   
        public void oyunaBasla()
        {
            oyunBasladiMi = true;
            baslangicPanel.SetActive(false);
        }



}
