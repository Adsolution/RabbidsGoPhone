Shader "Exkee/TextureOnly_NoZTest" {
Properties {
 _MainTex ("Texture", 2D) = "" {}
}
SubShader { 
 Tags { "QUEUE"="Geometry" "RenderType"="Geometry" }
 Pass {
  Tags { "QUEUE"="Geometry" "RenderType"="Geometry" }
  ZTest Always
  SetTexture [_MainTex] { combine texture, texture alpha }
 }
}
}