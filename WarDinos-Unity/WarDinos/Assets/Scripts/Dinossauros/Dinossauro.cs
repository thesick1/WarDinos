﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class Dinossauro : MonoBehaviour {

	public enum DinoTypes{APATOSSAURO=0,ESTEGOSSAURO=1,PTERODACTILO=2,RAPTOR=3,TREX=4,TRICERATOPO=5}

    public AudioSource attackSound;

	protected int vida;
	protected int ataque;
	protected double velocidadeAtaque;
	protected int velocidade_deslocamento;
	protected int alcance_ataque;

	protected int custoAttrVida;
	protected int custoAttrAtaque;
	protected int custoAttrVelocidadeAtaque;

	protected DinoTypes dinoType;
	protected int playerID;
	protected int lane; // necessary only for ptero ability.

	protected int MAX_VIDA;
	protected int MAX_ATAQUE;
	protected double MAX_VELOCIDADE_ATAQUE;
	protected int MAX_VELOCIDADE_DESLOCAMENTO;
	protected int MAX_ALCANCE_ATAQUE;

	protected int nSlot=1;

	void Start(){
		Velocidade_deslocamento =1;
	}

	public int Alcance_ataque {
		get {
			return alcance_ataque;
		}
		set{
			
			if(value<0){
				alcance_ataque=0;
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

	public int NSlot {
		get {
			return nSlot;
		}
	}

	public abstract void Habilidade();

    // Return true if it successfully attacked and false when there are no targets
    public bool Atacar(GroupController gp) {
        // Select target with the shortest life
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

    private void Die() {
        Destroy(gameObject);
    }


	public void CopyAttr(Dinossauro dino){
		this.vida= dino.vida;
		this.ataque= dino.ataque;
		this.velocidadeAtaque= dino.velocidadeAtaque;
		this.velocidade_deslocamento= dino.velocidade_deslocamento;
		this.alcance_ataque= dino.alcance_ataque;

		this.custoAttrVida= dino.custoAttrVida;
		this.custoAttrAtaque= dino.custoAttrAtaque;
		this.custoAttrVelocidadeAtaque= dino.custoAttrVelocidadeAtaque;

		this.dinoType = dino.dinoType;

		this.MAX_VIDA = dino.MAX_VIDA;
		this.MAX_ATAQUE= dino.MAX_ATAQUE;
		this.MAX_VELOCIDADE_ATAQUE= dino.MAX_VELOCIDADE_ATAQUE;
		this.MAX_VELOCIDADE_DESLOCAMENTO= dino.MAX_VELOCIDADE_DESLOCAMENTO;
		this.MAX_ALCANCE_ATAQUE= dino.MAX_ALCANCE_ATAQUE;
	}



}
