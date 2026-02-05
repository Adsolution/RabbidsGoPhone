Shader "Exkee/Texture_Modulate+3" {
Properties {
 _Color ("Color", Color) = (1,1,1,0.5)
 _MainTex ("Texture", 2D) = "" {}
}
SubShader { 
 Tags { "QUEUE"="Transparent+3" }
 Pass {
  Tags { "QUEUE"="Transparent+3" }
  ZWrite Off
  Blend SrcAlpha OneMinusSrcAlpha
  SetTexture [_MainTex] { ConstantColor [_Color] combine texture * constant }
 }
}
}