Shader "Exkee/Texture_Modulate+2" {
Properties {
 _Color ("Color", Color) = (1,1,1,0.5)
 _MainTex ("Texture", 2D) = "" {}
}
SubShader { 
 Tags { "QUEUE"="Transparent+2" }
 Pass {
  Tags { "QUEUE"="Transparent+2" }
  ZWrite Off
  Blend SrcAlpha OneMinusSrcAlpha
  SetTexture [_MainTex] { ConstantColor [_Color] combine texture * constant }
 }
}
}