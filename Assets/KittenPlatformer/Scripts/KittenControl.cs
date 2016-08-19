using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class KittenControl : MonoBehaviour 
{

	public float forwardSpeed;
	public float jumpForce;
	public Collider2D groundDetection;
	public AudioSource jumpSound;
	public float startRunningAt;
    public float maxSpeed;
    public float moveForce;
    public float idleSpeed;
    public int livesRemaining;
    public int score;
    public Renderer[] lifeIcons;
    public float lowestPosition;

    public IntEvent OnScoreIncrease;
    public IntEvent OnLifeRemoved;

    [System.Serializable]
    public class IntEvent : UnityEvent<int> {
        
    }

	private bool jump = false;
	private Rigidbody2D m_rigidBody;
	
	private Collider2D[] overlappingColliders;
	private bool isTouchingGround;
	private Bounds groundDetectionBox;
	private Animator animator;
    private bool facingRight = true;
    private SpriteRenderer sprite;
    private bool invincible;

    bool gameOver = false;

	void Awake () 
	{
		m_rigidBody = GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator>();
		overlappingColliders = new Collider2D[8];
        sprite = GetComponent<SpriteRenderer>();

        livesRemaining = lifeIcons.Length;
	}

    
    
	IEnumerator Start()
	{
        Run();
        yield break;
	}

    public void MakeNinja(){
        animator.SetBool("Ninja",true);
    }

    public void StopNinja(){
        animator.SetBool("Ninja",false);
    }
	
	void Run()
	{
		animator.SetBool("Running",true);
	}
	
	void Stop()
	{
		animator.SetBool("Running",false);

		//m_rigidBody.velocity = Vector2.zero;
	}
	
	// Update is called once per frame
	void Update () 
	{
        if( invincible ){
            sprite.enabled = ( Mathf.FloorToInt( Time.time * 8f ) & 1 )!=0;
        }
        if( gameOver ){
            return;
        }
		if( Input.GetButtonDown("Jump") ){
			jump = true;
		}
        if( transform.position.y < lowestPosition ){
            GameOver();
        }
	}
	
	void FixedUpdate()
	{
        if( gameOver ){
            return;
        }
		GroundCheck();
		Vector2 velocity = m_rigidBody.velocity;
		
        float h = Input.GetAxisRaw("Horizontal");
        m_rigidBody.AddForce(Vector2.right * h * moveForce);

        if( velocity.x > maxSpeed ){
            velocity.x = maxSpeed;
        }
        else if( velocity.x < -maxSpeed ){
            velocity.x = -maxSpeed;
        }
        if( h!=0 && ( facingRight ^ h > 0 ) ){
            Flip();
        }

        if( velocity.magnitude < idleSpeed ){
            Stop();
        }else {
            Run();
        }

		m_rigidBody.velocity = velocity;
		
		if( jump ){
			if( isTouchingGround ){
				m_rigidBody.AddForce( Vector2.up * jumpForce, ForceMode2D.Impulse );
				jumpSound.Play();
			}
			jump = false;
		}
		
	}

    
	
	void GroundCheck()
	{
		Bounds box = groundDetection.bounds;
        int numColliders = Physics2D.OverlapAreaNonAlloc( box.min, box.max, overlappingColliders );
		isTouchingGround = false;
		
		for( int i=0; i<numColliders; i++ ){
			if( overlappingColliders[i].gameObject != gameObject ){
				isTouchingGround = true;
				break;
			}
		} 
		
	}

    void Flip ()
    {
        // Switch the way the player is labelled as facing.
        facingRight = !facingRight;

        // Multiply the player's x local scale by -1.
        //Vector3 theScale = transform.localScale;
        //theScale.x *= -1;
        //transform.localScale = theScale;
        sprite.flipX = !facingRight;
    }

    public void IncreaseScore(int value){
        score += value;
        if( score < 0 ){
            score=0;
        }
        OnScoreIncrease.Invoke(score);
    }

    public void AddLife(){
        livesRemaining++;
        UpdateLives();
    }

    public void RemoveLife(){
        if( invincible ) {
            return;
        }
        livesRemaining--;
        Debug.Log(livesRemaining);
        if( livesRemaining > 0 ){
            MakeInvincible(2f);
        }
        UpdateLives();
    }

    public void GameOver(){
        StartCoroutine(_GameOver());
    }

    IEnumerator _GameOver()
	{
        if( gameOver ) yield break;
    
        gameOver = true;
        GetComponent<Animator>().SetTrigger("GameOver");
        GetComponent<Rigidbody2D>().velocity = Vector2.up * 6f;
        Collider2D[] colliders = GetComponents<Collider2D>();
        foreach( var collider in colliders ){
            Destroy( collider );
        }

        yield return new WaitForSeconds(2f);

        Destroy(this);
        SceneManager.LoadScene( SceneManager.GetActiveScene().buildIndex );
	}

    public void MakeInvincible(float seconds){
        if( invincible ) {
            return;
        }
        StartCoroutine( _MakeInvincible(seconds) );
    }

    IEnumerator _MakeInvincible(float seconds){
        invincible = true;
        yield return new WaitForSeconds( seconds );
        sprite.enabled = true;
        invincible = false;
    }


    void UpdateLives(){
        if( livesRemaining > lifeIcons.Length ){
            livesRemaining = lifeIcons.Length;
        }
        if( livesRemaining <= 0 ){
            livesRemaining = 0;
            GameOver();
        }
        for( int i=0; i<lifeIcons.Length; i++ ){
            var life = lifeIcons[i];
            life.enabled = ( livesRemaining > i );

        }
    }




}
