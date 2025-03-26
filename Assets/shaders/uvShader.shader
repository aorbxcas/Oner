// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Hidden/uvShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _SubTex ("Texture", 2D) = "white" {}
        _lightSpeed ("light change speed", Range(0,8)) = 1
    }
    SubShader
    {
        // No culling or depth
        Cull Off ZWrite Off ZTest Always

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };
            
            sampler2D _SubTex;
            sampler2D _MainTex;
            float4 _MainTex_ST;
            float _lightSpeed;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv,_MainTex);
                return o;
            }



            fixed4 frag (v2f i) : SV_Target
            {
                float2 uv_offset = float2(0,0);
                uv_offset.x = _Time.y * _lightSpeed * 0.1;
                uv_offset.y = _Time.y * _lightSpeed * 0.1;
                fixed4 light_color =  tex2D(_SubTex, i.uv + uv_offset);
                fixed4 col = tex2D(_MainTex, i.uv) + light_color;
                return col;
            }
            ENDCG
        }
    }
}
