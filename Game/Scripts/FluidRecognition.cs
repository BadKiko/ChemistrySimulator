﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Obi;

public class FluidRecognition : MonoBehaviour
{
	[SerializeField] ObiSolver solver;
	[SerializeField] ObiEmitter emitter;
    [SerializeField] ObiCollider mainCollider;
    [SerializeField] Interaction _interactionScript;
    [SerializeField] LiquidController _liquidControllerScript;


    private void Update()
    {
        solver = _interactionScript.PickUpObiSolver;
        emitter = _interactionScript.PickUpObiEmitter;
    }

    void OnEnable()
	{
        solver.OnCollision += Solver_OnCollision;
	}

	void OnDisable()
	{
        solver.OnCollision -= Solver_OnCollision;
	}

	void Solver_OnCollision(object sender, Obi.ObiSolver.ObiCollisionEventArgs collisionEventArgs)
	{
		foreach (Oni.Contact contact in collisionEventArgs.contacts)
		{   
            
            
			if (contact.distance < 0.01)
			{


                Component collider = mainCollider;
                if (ObiCollider.idToCollider[contact.other].tag == "CanAddLiquid")
                {
                    emitter.life[solver.particleToActor[contact.particle].indexInActor] = 0;
                    _liquidControllerScript.AddFluid();
                }
            }
		}
	}
}
