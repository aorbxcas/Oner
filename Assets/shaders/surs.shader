Shader "Custom/surs"
{
    SubShader
    {
        pass{
            CGPROGRAM
            #pragma vertex vert

            #pragma fragment frag

            void MyFunc(out float4 c);

            void vert(in float2 objPos:POSITION,out float4 pos:POSITION, out float4 col:COLOR){
                pos = float4(objPos,0,1);
                col = pos;
            }
            void frag(in float4 pos:POSITION,inout float4 col:COLOR){
                MyFunc(col);
            }

            void MyFunc(out float4 c){
                c = float4(1,0,0,1);
            }
            ENDCG
        }
    }
    //FallBack "Diffuse"
}
