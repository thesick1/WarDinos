﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using MongoDB.Bson.Serialization.Attributes;

public abstract class Dinossauro : MonoBehaviour {
	[BsonIgnoreAttribute] 
	private GroupController gc;
	[BsonIgnoreAttribute] 
	private Player playerSelf;
	[BsonIgnoreAttribute] 
	private Player playerEnemy;


//	public enum DinoTypes{APATOSSAURO=0,ESTEGOSSAURO=1,PTERODACTILO=2,RAPTOR=3,TREX=4,TRICERATOPO=5}

    protected int custo;
    protected int abilityCost;

	protected int vida;
	protected int ataque;
	protected double velocidadeAtaque;
	protected int velocidade_deslocamento;
	protected int alcance_ataque;
    
     
	protected int custoAttrVida;
	protected int custoAttrAtaque;
	protected int custoAttrVelocidadeAtaque;
	protected int custoAttrVelocidadeDeslocamento;

    protected int vida_upg;
    protected int ataque_upg;
    protected double velocidadeAtaque_upg;
    protected int velocidade_deslocamento_upg;

    //SOME FLAGS
	[BsonIgnoreAttribute] 
	protected GroupController.DinoType dinoType;
	protected int playerID;
	protected bool habilidadeOn = false;

	//MAX VALUE OF ATTRIBUTES
	protected int MAX_VIDA;
	protected int MAX_ATAQUE;
	protected double MAX_VELOCIDADE_ATAQUE;
	protected int MAX_VELOCIDADE_DESLOCAMENTO;
	protected int MAX_ALCANCE_ATAQUE;
	protected int MAX_ATTR_VIDA;
	protected int MAX_ATTR_ATAQUE;
	protected int MAX_ATTR_VEL_ATQ;
	protected int MAX_ATTR_VEL_DES;

	protected int nSlot=1;

	protected LoggerMongo logg;

	void Start(){
	//	Velocidade_deslocamento =1;
        
    }
	public GroupController.DinoType DinoType{
		get{ 
			return dinoType;
		}
	}

    public bool HabilidadeOn {
        get
        {
            return habilidadeOn;
        }
        set {
            habilidadeOn = value;
        }
    }

    public int Custo { get { return custo; } }

	public int CustoAttrVida{
		get{
			return custoAttrVida;
		}
		set{
			if (value < 1) {
				custoAttrVida = 1;
			} else if (value > MAX_ATTR_VIDA) {
				custoAttrVida = MAX_ATTR_VIDA;
			} else {
				custoAttrVida = value;
			}
		}
	}

	public int CustoAttrAtaque{
		get{
			return custoAttrAtaque;
		}
		set{
			if (value < 1) {
				custoAttrAtaque = 1;
			} else if (value > MAX_ATTR_ATAQUE) {
				custoAttrAtaque = MAX_ATTR_ATAQUE;
			} else {
				custoAttrAtaque = value;
			}
		}
	}
	public int CustoAttrVelocidadeAtaque{
		get{
			return custoAttrVelocidadeAtaque;
		}
		set{
			if (value < 1) {
				custoAttrVelocidadeAtaque = 1;
			} else if (value > MAX_ATTR_VEL_ATQ) {
				custoAttrVelocidadeAtaque = MAX_ATTR_VEL_ATQ;
			} else {
				custoAttrVelocidadeAtaque = value;
			}
		}
	}
	public int CustoAttrVelocidadeDeslocamento{
		get{
			return custoAttrVelocidadeDeslocamento;
		}
		set{
			if (value < 1) {
				custoAttrVelocidadeDeslocamento = 1;
			} else if (value > MAX_ATTR_VEL_DES) {
				custoAttrVelocidadeDeslocamento = MAX_ATTR_VEL_DES;
			} else {
				custoAttrVelocidadeDeslocamento = value;
			}
		}
	}

	public int Alcance_ataque {
		get {
			return alcance_ataque;
		}
		set{
			
			if(value<1){
				alcance_ataque=1;
			}
			else if(value>MAX_ALCANCE_ATAQUE){
				alcance_ataque = MAX_ALCANCE_ATAQUE;
			}else
			alcance_ataque=value;

		}
	}

	public int PlayerID {
		get {
			return playerID;
		}
		set{
			playerID=value;
		}
	}

	public int Velocidade_deslocamento {
		get {
			return velocidade_deslocamento;
		}
		set{
			
			if(value<0){
				velocidade_deslocamento = 1;
			}
			else if(value>MAX_VELOCIDADE_DESLOCAMENTO){
				velocidade_deslocamento = MAX_VELOCIDADE_DESLOCAMENTO;
			}else
				velocidade_deslocamento=value;
			
		}
	}

	public double VelocidadeAtaque {
		get {
			return velocidadeAtaque;
		}
		set{
			if(value<1){
				velocidadeAtaque= 1;
			}
			else if(value>MAX_VELOCIDADE_ATAQUE ){
				velocidadeAtaque = MAX_VELOCIDADE_ATAQUE;
			}else{
				velocidadeAtaque =value;
			}
		}
	}

	public int Ataque {
		get {
			return ataque;
		}
		set{
			if(value<1){
				ataque= 1;
			}
			else if(value>MAX_ATAQUE ){
				ataque = MAX_ATAQUE;
			}else{
				ataque =value;
			}
		}
	}

	public int Vida {
		get {
			return vida;
		}
		set{
			if(value<=0){
				Die();
			}
			else if(value>MAX_VIDA ){
				vida = MAX_VIDA;
			}else{
				vida =value;
			}
		}
	}

    public int AbilityCost
    {
        get
        {
            return abilityCost;
        }
    }

    public int Vida_upg { get { return vida_upg; } }
    public int Ataque_upg { get { return vida_upg; } }
    public int VelocidadeAtaque_upg { get { return vida_upg; } }
    public int Velocidade_deslocamento_upg { get { return vida_upg; } }

    public bool UpgradeVida() {
        if (custoAttrVida <= MAX_ATTR_VIDA)
        {
            vida = vida + vida_upg;
            custoAttrVida++;
            return true;
        }
        else return false;
    }
    public bool UpgradeAtaque()
    {
        if (custoAttrAtaque <= MAX_ATTR_ATAQUE)
        {
            ataque = ataque + ataque_upg;
            custoAttrAtaque++;
            return true;
        }
        else return false;
    }
    public bool UpgradeVelAtq()
    {
        if (custoAttrVelocidadeAtaque <= MAX_ATTR_VEL_ATQ)
        {
            velocidadeAtaque = velocidadeAtaque + velocidadeAtaque_upg;
            custoAttrVelocidadeAtaque++;
            return true;
        }
        else return false;
    }
    public bool UpgradeVelDes()
    {
        if(custoAttrVelocidadeDeslocamento <= MAX_ATTR_VEL_DES)
        {
            velocidade_deslocamento = velocidade_deslocamento + velocidade_deslocamento_upg;
            custoAttrVelocidadeDeslocamento++;
            return true;
        }
        else return false;
    }
    public bool UpgradeAbility()
    {
        if (habilidadeOn == false)
        {
            habilidadeOn = true;
            return true;
        }
        else return false;
    }
    public int NSlot {
		get {
			return nSlot;
		}
	}

	public abstract void Habilidade();

    // Return true if it successfully attacked OR false when there IS no target.
    public abstract bool Atacar(GroupController gp);

    /*
    public bool Atacar(GroupController gp) {
        Gc = gp;
		Dinossauro dTarget = null;
        int menorVida = -1;


        foreach (Dinossauro d in gp.DinosDinossauro) {
            if (d != null && (d.Vida < menorVida || menorVida == -1)) {
                dTarget = d;
                menorVida = d.Vida;
            }
        }
        if (menorVida != -1) {
            dTarget.Vida = dTarget.Vida - ataque;
            Debug.Log(GetInstanceID() + "Attacked with " + ataque + " dmg. Target was " + dTarget + "which is now with " + dTarget.Vida + "life");
            return true;
        }
        else {
            Debug.Log(GetInstanceID() + "Attacked but there were no target");
            return false;
        }
    }
    */
    private void Die() {
        //gameObject.SetActive(false);
        //transform.position = new Vector2(999.0f, 999.0f);
		if (DinoType == GroupController.DinoType.APATOSSAURO && habilidadeOn) {
            foreach(Dinossauro d in gc.enemyTargetGroup.DinosDinossauro)
            {
                //Reverse Apatasaur ability
                if (d != null)
                    d.VelocidadeAtaque = d.VelocidadeAtaque / 2;

            }
        }
        // When the dinosaur is destroyed, the enemy player is rewarded with Dodo Meth
		this.GetComponent<Animator>().Play("dead");
        playerEnemy.incrementarRecursos(custo);
        Destroy(gameObject);
    }



    public Player PlayerSelf {
        get { return playerSelf; }
        set { playerSelf = value; }
    }

    public Player PlayerEnemy {
        get { return playerEnemy; }
        set { playerEnemy = value; }
    }

    public GroupController Gc {
        get { return gc; }
        set { gc = value; }
    }

    public int GET_MAX_ATTR_VIDA { get { return MAX_ATTR_VIDA; } }
    public int GET_MAX_ATTR_ATAQUE { get { return MAX_ATTR_ATAQUE; } }
    public int GET_MAX_ATTR_VEL_ATQ { get { return MAX_ATTR_VEL_ATQ; } }
    public int GET_MAX_ATTR_VEL_DES { get { return MAX_ATTR_VEL_DES; } }



    public void CopyAttr(Dinossauro dino){
        this.custo = dino.custo;

        this.vida= dino.vida;
		this.ataque= dino.ataque;
		this.velocidadeAtaque= dino.velocidadeAtaque;
		this.velocidade_deslocamento= dino.velocidade_deslocamento;
		this.alcance_ataque= dino.alcance_ataque;

		this.custoAttrVida= dino.custoAttrVida;
		this.custoAttrAtaque= dino.custoAttrAtaque;
		this.custoAttrVelocidadeAtaque= dino.custoAttrVelocidadeAtaque;
        this.custoAttrVelocidadeDeslocamento = dino.custoAttrVelocidadeDeslocamento;
        this.abilityCost = dino.abilityCost;

		this.dinoType = dino.dinoType;

		this.MAX_VIDA = dino.MAX_VIDA;
		this.MAX_ATAQUE= dino.MAX_ATAQUE;
		this.MAX_VELOCIDADE_ATAQUE= dino.MAX_VELOCIDADE_ATAQUE;
		this.MAX_VELOCIDADE_DESLOCAMENTO= dino.MAX_VELOCIDADE_DESLOCAMENTO;
		this.MAX_ALCANCE_ATAQUE= dino.MAX_ALCANCE_ATAQUE;

        this.habilidadeOn = dino.habilidadeOn;
	}



}
