using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormieAnimation : MonoBehaviour {
	
	Animator m_animator;
	
	// Use this for initialization
	void Start () {
		
		m_animator = GetComponent<Animator>();
		
	}
	
	// Update is called once per frame
	void Update () {
		
		//When holding down w character walks. When flase it goes back to idle. 
	
		bool isMovingPressed = Input.GetKey("w");
		m_animator.SetBool("IsMoving", isMovingPressed);
		
		//When key pressed character base attack and goes back to idle 
		bool baseAttackPressed = Input.GetKeyDown("space");
		m_animator.SetBool("BaseAttack", baseAttackPressed);

		bool sliceAttackPressed = Input.GetKeyDown("x");
		m_animator.SetBool("SliceAttack", sliceAttackPressed);

		////When holding down we character is guarding. When flase it goes back to idle. 
		bool isGuardingPressed = Input.GetKey("e");
		m_animator.SetBool("IsGuarding", isGuardingPressed);
		
		//when key pressed character does death animation but doesnt come back to idle state.
		bool isDeadPressed = Input.GetKeyDown("q");
		m_animator.SetBool("IsDead", isDeadPressed);

		


	}
}
