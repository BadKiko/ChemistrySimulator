using UnityEngine;
using BzKovSoft.ObjectSlicer;
using System.Diagnostics;
using System;
using System.Collections;

namespace BzKovSoft.ObjectSlicerSamples
{
	/// <summary>
	/// Test class for demonstration purpose
	/// </summary>
	public class SampleKnifeSlicer : MonoBehaviour
	{
#pragma warning disable 0649
		[SerializeField]
		private GameObject _blade;

        [SerializeField] private GameObject Camera;
#pragma warning restore 0649

		void Update()
		{
			if (Input.GetMouseButtonDown (0))
			{

				var knife = _blade.GetComponentInChildren<BzKnife>();
				knife.BeginNewSlice();

            }

		}

	}
}