Shader "Exkee/TextureOnly_TransNoCullingAlphaTest" {
Properties {
 _MainTex ("Texture", 2D) = "" {}
}
SubShader { 
 Tags { "QUEUE"="Transparent" }
 Pass {
  Tags { "QUEUE"="Transparent" }
  Cull Off
  Blend SrcAlpha OneMinusSrcAlpha
  AlphaTest Greater 0.5
  SetTexture [_MainTex] { combine texture, texture alpha }
 }
}
}