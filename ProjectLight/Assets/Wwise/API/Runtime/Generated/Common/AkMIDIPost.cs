#if ! (UNITY_DASHBOARD_WIDGET || UNITY_WEBPLAYER || UNITY_WII || UNITY_WIIU || UNITY_NACL || UNITY_FLASH || UNITY_BLACKBERRY) // Disable under unsupported platforms.
//------------------------------------------------------------------------------
// <auto-generated />
//
// This file was automatically generated by SWIG (https://www.swig.org).
// Version 4.2.1
//
// Do not make changes to this file unless you know what you are doing - modify
// the SWIG interface file instead.
//------------------------------------------------------------------------------


public class AkMIDIPost : AkMIDIEvent {
  private global::System.IntPtr swigCPtr;

  internal AkMIDIPost(global::System.IntPtr cPtr, bool cMemoryOwn) : base(AkSoundEnginePINVOKE.CSharp_AkMIDIPost_SWIGUpcast(cPtr), cMemoryOwn) {
    swigCPtr = cPtr;
  }

  internal static global::System.IntPtr getCPtr(AkMIDIPost obj) {
    return (obj == null) ? global::System.IntPtr.Zero : obj.swigCPtr;
  }

  internal override void setCPtr(global::System.IntPtr cPtr) {
    base.setCPtr(AkSoundEnginePINVOKE.CSharp_AkMIDIPost_SWIGUpcast(cPtr));
    swigCPtr = cPtr;
  }

  protected override void Dispose(bool disposing) {
    lock(this) {
      if (swigCPtr != global::System.IntPtr.Zero) {
        if (swigCMemOwn) {
          swigCMemOwn = false;
          AkSoundEnginePINVOKE.CSharp_delete_AkMIDIPost(swigCPtr);
        }
        swigCPtr = global::System.IntPtr.Zero;
      }
      global::System.GC.SuppressFinalize(this);
      base.Dispose(disposing);
    }
  }

  public ulong uOffset { set { AkSoundEnginePINVOKE.CSharp_AkMIDIPost_uOffset_set(swigCPtr, value); }  get { return AkSoundEnginePINVOKE.CSharp_AkMIDIPost_uOffset_get(swigCPtr); } 
  }

  public uint PostOnEvent(uint in_eventID, ulong in_gameObjectID, uint in_uNumPosts) {
		uint ret = AkSoundEnginePINVOKE.CSharp_AkMIDIPost_PostOnEvent__SWIG_0(swigCPtr, in_eventID, in_gameObjectID, in_uNumPosts);
		AkCallbackManager.SetLastAddedPlayingID(ret);
		return ret;
	}

  public uint PostOnEvent(uint in_eventID, ulong in_gameObjectID, uint in_uNumPosts, bool in_bAbsoluteOffsets) {
		uint ret = AkSoundEnginePINVOKE.CSharp_AkMIDIPost_PostOnEvent__SWIG_1(swigCPtr, in_eventID, in_gameObjectID, in_uNumPosts, in_bAbsoluteOffsets);
		AkCallbackManager.SetLastAddedPlayingID(ret);
		return ret;
	}

  public uint PostOnEvent(uint in_eventID, ulong in_gameObjectID, uint in_uNumPosts, bool in_bAbsoluteOffsets, uint in_uFlags, AkCallbackManager.EventCallback in_pfnCallback, object in_pCookie) {
	in_pCookie = AkCallbackManager.EventCallbackPackage.Create(in_pfnCallback, in_pCookie, ref in_uFlags);
    {
		uint ret = AkSoundEnginePINVOKE.CSharp_AkMIDIPost_PostOnEvent__SWIG_2(swigCPtr, in_eventID, in_gameObjectID, in_uNumPosts, in_bAbsoluteOffsets, in_uFlags, in_uFlags != 0 ? (global::System.IntPtr)1 : global::System.IntPtr.Zero, in_pCookie != null ? (global::System.IntPtr)in_pCookie.GetHashCode() : global::System.IntPtr.Zero);
		AkCallbackManager.SetLastAddedPlayingID(ret);
		return ret;
	}
  }

  public uint PostOnEvent(uint in_eventID, ulong in_gameObjectID, uint in_uNumPosts, bool in_bAbsoluteOffsets, uint in_uFlags, AkCallbackManager.EventCallback in_pfnCallback, object in_pCookie, uint in_playingID) {
	in_pCookie = AkCallbackManager.EventCallbackPackage.Create(in_pfnCallback, in_pCookie, ref in_uFlags);
    {
		uint ret = AkSoundEnginePINVOKE.CSharp_AkMIDIPost_PostOnEvent__SWIG_3(swigCPtr, in_eventID, in_gameObjectID, in_uNumPosts, in_bAbsoluteOffsets, in_uFlags, in_uFlags != 0 ? (global::System.IntPtr)1 : global::System.IntPtr.Zero, in_pCookie != null ? (global::System.IntPtr)in_pCookie.GetHashCode() : global::System.IntPtr.Zero, in_playingID);
		AkCallbackManager.SetLastAddedPlayingID(ret);
		return ret;
	}
  }

  public uint PostOnEvent(uint in_eventID, UnityEngine.GameObject in_gameObjectID, uint in_uNumPosts)
  {
    var in_gameObjectID_id = AkSoundEngine.GetAkGameObjectID(in_gameObjectID);
    AkSoundEngine.PreGameObjectAPICall(in_gameObjectID, in_gameObjectID_id);

    uint ret = PostOnEvent(in_eventID, in_gameObjectID_id, in_uNumPosts);
	AkCallbackManager.SetLastAddedPlayingID(ret);
	return ret;
  }
  
  public uint PostOnEvent(uint in_eventID, UnityEngine.GameObject in_gameObjectID, uint in_uNumPosts, bool in_bAbsoluteOffsets)
  {
    var in_gameObjectID_id = AkSoundEngine.GetAkGameObjectID(in_gameObjectID);
    AkSoundEngine.PreGameObjectAPICall(in_gameObjectID, in_gameObjectID_id);

    uint ret = PostOnEvent(in_eventID, in_gameObjectID_id, in_uNumPosts, in_bAbsoluteOffsets);
	AkCallbackManager.SetLastAddedPlayingID(ret);
	return ret;
  }
  
  public uint PostOnEvent(uint in_eventID, UnityEngine.GameObject in_gameObjectID, uint in_uNumPosts, bool in_bAbsoluteOffsets, uint in_uFlags, AkCallbackManager.EventCallback in_pfnCallback, object in_pCookie)
  {
    var in_gameObjectID_id = AkSoundEngine.GetAkGameObjectID(in_gameObjectID);
    AkSoundEngine.PreGameObjectAPICall(in_gameObjectID, in_gameObjectID_id);

    uint ret =  PostOnEvent(in_eventID, in_gameObjectID_id, in_uNumPosts, in_bAbsoluteOffsets, in_uFlags, in_pfnCallback, in_pCookie);
	AkCallbackManager.SetLastAddedPlayingID(ret);
	return ret;
  }
  
  public uint PostOnEvent(uint in_eventID, UnityEngine.GameObject in_gameObjectID, uint in_uNumPosts, bool in_bAbsoluteOffsets, uint in_uFlags, AkCallbackManager.EventCallback in_pfnCallback, object in_pCookie, uint in_playingID)
  {
    var in_gameObjectID_id = AkSoundEngine.GetAkGameObjectID(in_gameObjectID);
    AkSoundEngine.PreGameObjectAPICall(in_gameObjectID, in_gameObjectID_id);
	in_pCookie = AkCallbackManager.EventCallbackPackage.Create(in_pfnCallback, in_pCookie, ref in_uFlags);

    uint ret =  PostOnEvent(in_eventID, in_gameObjectID_id, in_uNumPosts, in_bAbsoluteOffsets, in_uFlags, in_pfnCallback, in_pCookie, in_playingID);
	AkCallbackManager.SetLastAddedPlayingID(ret);
	return ret;
  }
  

  public void Clone(AkMIDIPost other) { AkSoundEnginePINVOKE.CSharp_AkMIDIPost_Clone(swigCPtr, AkMIDIPost.getCPtr(other)); }

  public static int GetSizeOf() { return AkSoundEnginePINVOKE.CSharp_AkMIDIPost_GetSizeOf(); }

  public AkMIDIPost() : this(AkSoundEnginePINVOKE.CSharp_new_AkMIDIPost(), true) {
  }

}
#endif // #if ! (UNITY_DASHBOARD_WIDGET || UNITY_WEBPLAYER || UNITY_WII || UNITY_WIIU || UNITY_NACL || UNITY_FLASH || UNITY_BLACKBERRY) // Disable under unsupported platforms.