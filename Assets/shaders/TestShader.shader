Shader "Custom/TestShader"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _Ambient ("Ambient", Color) = (0.3,0.3,0.3,0.3)
        _Spectular ("Specular", Color) = (1,1,1,1)
        _Shininess ("Shininess", Range(0,8)) = 4
        _Emission ("Emission", Color) = (1,1,1,1)
        _Constant ("ConstantColor", Color) = (1,1,1,0.3)
        _MainTex ("Texture", 2D) = "" {}
        _SecondTex ("SecondTexture", 2D) = "" {}


    }
    SubShader
    {
        Tags {"Queue"="Transparent"} // 渲染队列
        pass{
            Blend SrcAlpha OneMinusSrcAlpha // 混合模式——透明混合
            material{
                diffuse[_Color]  // 漫反射
                ambient[_Ambient]  // 环境光
                specular[_Spectular] // 镜面反射
                shininess[_Shininess]  //  高光
                emission[_Emission] // 自发光
                }
            lighting on // 光照开关
            separatespecular on // 镜面反射开关
            settexture[_MainTex]{ // 纹理
                combine texture * primary double
                }
            settexture[_SecondTex]{
                constantcolor[_Constant] // 寄存器存储变量，已弃用
                combine texture * previous double, texture * constant 
                }
            }

    }
}
