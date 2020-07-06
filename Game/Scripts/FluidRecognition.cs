using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Obi;

public class FluidRecognition : MonoBehaviour
{
	[SerializeField] ObiSolver solver;

	Obi.ObiSolver.ObiCollisionEventArgs collisionEvent;

	void OnEnable()
	{
		solver.OnCollision += Solver_OnCollision;
	}

	void OnDisable()
	{
		solver.OnCollision -= Solver_OnCollision;
	}

	void Solver_OnCollision(object sender, Obi.ObiSolver.ObiCollisionEventArgs e)
	{
		foreach (Oni.Contact contact in e.contacts)
		{
			Debug.Log("WTF");

			if (contact.distance < 0.01)
			{
				Component collider;
				if (ObiCollider.idToCollider.TryGetValue(contact.other, out collider))
				{
					Debug.Log("WTF2");
				}
			}
		}
	}
}
