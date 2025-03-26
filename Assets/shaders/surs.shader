// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/surs"
{
    SubShader
    {
        pass{
            CGPROGRAM
            #pragma vertex vert

            #pragma fragment frag

            void MyFunc(out float4 c);

            float4 vert(in float4 objPos:POSITION,out float4 pos:POSITION):COLOR
            {
                //pos = float4(objPos,0,1);
                pos = UnityObjectToClipPos(objPos);
                return objPos;
            }
            //void frag(in float4 pos:POSITION,inout float4 col:COLOR){
            //    MyFunc(col);
            //}

            void MyFunc(out float4 c){
                c = float4(1,0,0,1);
            }
            
            float4 frag(in float4 col:COLOR):COLOR
            {
                return col;
            }
            ENDCG
        }
    }
    //FallBack "Diffuse"
}
