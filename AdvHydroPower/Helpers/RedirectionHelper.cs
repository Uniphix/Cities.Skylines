using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace AdvHydroPower {
	internal struct RedirectCallsState {
		public IntPtr fptr1;
		public byte a;
		public byte b;
		public byte c;
		public byte d;
		public byte e;
		public ulong f;
	}

	internal static class RedirectionHelper {
		public static RedirectCallsState RedirectCalls( MethodInfo from, MethodInfo to ) {
			return RedirectionHelper.PatchJumpTo( from.MethodHandle.GetFunctionPointer(), to.MethodHandle.GetFunctionPointer() );
		}

		public static void RevertRedirect( RedirectCallsState state ) {
			RedirectionHelper.RevertJumpTo( state );
		}

		private static unsafe RedirectCallsState PatchJumpTo( IntPtr site, IntPtr target ) {
			RedirectCallsState redirectCallState = new RedirectCallsState();
			redirectCallState.fptr1 = site;
			byte* numPtr = (byte*) site.ToPointer();
			redirectCallState.a = *numPtr;
			redirectCallState.b = numPtr[1];
			redirectCallState.c = numPtr[10];
			redirectCallState.d = numPtr[11];
			redirectCallState.e = numPtr[12];
			redirectCallState.f = (ulong)*(long*)( numPtr + 2 );
			*numPtr = (byte)73;
			numPtr[1] = (byte)187;
			*(long*)( numPtr + 2 ) = target.ToInt64();
			numPtr[10] = (byte)65;
			numPtr[11] = byte.MaxValue;
			numPtr[12] = (byte)227;
			return redirectCallState;
		}

		private static unsafe void RevertJumpTo( RedirectCallsState state ) {
			byte* numPtr = (byte*) state.fptr1.ToPointer();
			*numPtr = state.a;
			numPtr[1] = state.b;
			*(long*)( numPtr + 2 ) = (long)state.f;
			numPtr[10] = state.c;
			numPtr[11] = state.d;
			numPtr[12] = state.e;
		}
	}
}
