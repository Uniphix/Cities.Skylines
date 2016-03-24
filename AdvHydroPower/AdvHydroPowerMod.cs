using ICities;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace AdvHydroPower {
	public class AdvHydroPowerMod : LoadingExtensionBase, IUserMod {
		public const string NAME = "Advance Hydro Power";
		public const string VERSION = "1.0.*";
		public const string RELEASE_VERSION = "1.0.1";

		public string Description {
			get {
				return "Brings realistic power, and makes the water flow better and more accurate, with flood gates";
			}
		}

		public string Name {
			get {
				return NAME;
			}
		}
		private List<RedirectCallState> _redirectionStates = new List<RedirectCallState>();

		public override void OnLevelLoaded( LoadMode mode ) {
			base.OnLevelLoaded( mode );

			if ( mode != LoadMode.LoadGame && mode != LoadMode.NewGame ) {
				return;
			}

			Debug.LogWarning( string.Format( "[{0}] {1}: {2} initializing", DateTime.Now, AdvHydroPowerMod.NAME, AdvHydroPowerMod.RELEASE_VERSION ) );
			AdvDamPowerHouseAI.Deploy();
		}

		public override void OnLevelUnloading() {
			base.OnLevelUnloading();

			AdvDamPowerHouseAI.Revert();
        }
	}
}
