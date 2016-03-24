using ColossalFramework.Plugins;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace AdvHydroPower {
	public class AdvDamPowerHouseAI : DamPowerHouseAI {
		public override void SimulationStep( ushort buildingID, ref Building buildingData, ref Building.Frame frameData ) {
			Debug.LogWarning( "Test" );
		}

		internal static void RedirectCalls( List<RedirectCallsState> callStates ) {
			MethodInfo from = Enumerable.FirstOrDefault<MethodInfo>((IEnumerable<MethodInfo>) typeof (DamPowerHouseAI).GetMethods(), (Func<MethodInfo, bool>) (m => m.Name == "SimulationStep" && m.GetParameters().Length == 6));
			MethodInfo to = Enumerable.FirstOrDefault<MethodInfo>((IEnumerable<MethodInfo>) typeof (AdvDamPowerHouseAI).GetMethods(), (Func<MethodInfo, bool>) (m => m.Name == "SimulationStep" && m.GetParameters().Length == 6));
			if ( from == null || to == null ) {
				return;
			}

			callStates.Add( RedirectionHelper.RedirectCalls( from, to ) );
		}
	}
}
