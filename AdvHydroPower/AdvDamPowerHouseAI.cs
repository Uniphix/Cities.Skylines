using ColossalFramework.Plugins;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace AdvHydroPower {
	public class AdvDamPowerHouseAI : DamPowerHouseAI {
		private static RedirectCallState _revertState;
		private static readonly MethodInfo _originalSimulationStepMethodInfo = typeof (DamPowerHouseAI).GetMethods().FirstOrDefault( m => m.Name == "SimulationStep" && m.GetParameters().Length == 3 );
		private static readonly MethodInfo _newSimulationStepMethodInfo = typeof (AdvDamPowerHouseAI).GetMethods().FirstOrDefault( m => m.Name == "SimulationStep" && m.GetParameters().Length == 3 );

		public override void SimulationStep( ushort buildingID, ref Building buildingData, ref Building.Frame frameData ) {
			Debug.LogWarning( "Test" );
		}

		internal static void Deploy() {
			if ( _originalSimulationStepMethodInfo == null ) {
				throw new NullReferenceException( "Original SimulationStep method not found" );
			}

			if ( _newSimulationStepMethodInfo == null ) {
				throw new NullReferenceException( "New SimulationStep method not found" );
			}

			_revertState = RedirectionHelper.RedirectCalls( _originalSimulationStepMethodInfo, _newSimulationStepMethodInfo );
		}

		internal static void Revert() {
			RedirectionHelper.RevertRedirect( _revertState );
		}
	}
}
