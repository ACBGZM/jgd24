#if UNITY_ANDROID && ! UNITY_EDITOR
//------------------------------------------------------------------------------
// <auto-generated />
//
// This file was automatically generated by SWIG (https://www.swig.org).
// Version 4.2.1
//
// Do not make changes to this file unless you know what you are doing - modify
// the SWIG interface file instead.
//------------------------------------------------------------------------------


public enum AkAudioAPI {
  AkAudioAPI_AAudio = 1 << 0,
  AkAudioAPI_OpenSL_ES = 1 << 1,
  AkAudioAPI_Default = AkAudioAPI_AAudio|AkAudioAPI_OpenSL_ES
}
#endif // #if UNITY_ANDROID && ! UNITY_EDITOR