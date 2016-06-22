﻿using UnityEngine;
using System.Collections;

public class skelleton : MonoBehaviour {
	public float maxHealth;
    public float curHealth;
    private float curHealthBar;
    public float count;
    public int controle;
    public float atk;
    private GameObject healthbar;
    Animator animator;
    player player;
	// Use this for initialization
	void Start () {
        count = 0;
        controle = 0;
        atk = 100;
        animator = GetComponent<Animator>();
		maxHealth = 100f;
		curHealth = 100f;
        curHealthBar = 100f;
        healthbar = GameObject.Find("life");
        player = FindObjectOfType<player>();
	}
	
	// Update is called once per frame
	void Update () {
       
       
        if (curHealth != curHealthBar) {
            float x = ((curHealthBar - curHealth) * 0.01f);
            curHealthBar = curHealth;
            healthbar.transform.localScale -= new Vector3(x,0,0);
        }
        AnimatorStateInfo asi = animator.GetCurrentAnimatorStateInfo(0);
        count = Mathf.Round(asi.normalizedTime * 100f)/100f;
        if (asi.IsName("Attack") && controle==1&&count>=0.8) {
            attack(atk);
            controle--;
        }

        else if (asi.IsName("iddle") && controle == 0) {
            controle++;
        }

        else if ((asi.IsName("death") && !animator.IsInTransition(0) && count>=1))
        {
            Destroy(this.gameObject);
        }

        else if (asi.IsName("walkFront"))
        {
            if (transform.position.x == 0)
            {
                if (Mathf.Abs(transform.position.z) <= 15)
                {
                    animator.Play("iddle");
                }
                else
                {
                    walkFront();
                }
            }
            else
            {
                if (Mathf.Abs(transform.position.x) <= 15)
                {
                    animator.Play("iddle");
                }
                else
                {
                    walkFront();
                }
            }
        }

    }

    public void walkFront()
    {
       Vector3 a = transform.position;
        if (transform.position.x == 0)
        {
            if (transform.position.z>0)
            {
                Vector3 b = new Vector3(a.x, a.y, a.z - 1f);
                transform.position = b;
            }
            else
            {
                Vector3 b = new Vector3(a.x, a.y, a.z + 1f);
                transform.position = b;
            }
        }
        else
        {
            if (transform.position.x >0)
            {
                Vector3 b = new Vector3(a.x - 1f, a.y, a.z );
                transform.position = b;
            }
            else
            {
                Vector3 b = new Vector3(a.x + 1f, a.y, a.z);
                transform.position = b;
            }
        }



        
       
    }

    public void attack(float atk) {

        player.damage(atk);

    }

    public void decreasingHealth() {
         if (curHealth > 0)
        {
           curHealth -= 20f;

			if (curHealth < 0)
				curHealth = 0;
        }
        if (curHealth <= 0)
        {
            animator.Play("death", 0);

        }
        else
        {
            animator.Play("hit", 0);
        }

    }

    void OnCollisionEnter(Collision col) {

        Destroy(col.gameObject);
        decreasingHealth();
    }


    void OnGUI() {
       

        /*GameObject player = GameObject.Find("Player_life");
        GUIStyle a = new GUIStyle();
        a.fontSize = 10;
        a.normal.textColor = Color.white;
        GUILayout.Label("playerHealth: "+playerHealth,a);
        GUILayout.Label("playerHealthBar: " + player.transform.localScale, a);

        */
    }

}
