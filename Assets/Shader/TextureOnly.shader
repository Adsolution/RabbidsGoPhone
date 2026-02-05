Shader "Exkee/TextureOnly" {
Properties {
 _MainTex ("Texture", 2D) = "" {}
}
SubShader { 
 Tags { "QUEUE"="Geometry" "RenderType"="Geometry" }
 Pass {
  Tags { "QUEUE"="Geometry" "RenderType"="Geometry" }
  SetTexture [_MainTex] { combine texture, texture alpha }
 }
}
}