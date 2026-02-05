Shader "Exkee/NoTexture_TransColor" {
Properties {
 _Color ("Main Color, Alpha", Color) = (1,1,1,1)
}
SubShader { 
 Tags { "QUEUE"="Transparent" }
 Pass {
  Tags { "QUEUE"="Transparent" }
  Color [_Color]
  ZWrite Off
  Blend SrcAlpha OneMinusSrcAlpha
 }
}
}