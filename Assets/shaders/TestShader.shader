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
        Tags {"Queue"="Transparent"} // ��Ⱦ����
        pass{
            Blend SrcAlpha OneMinusSrcAlpha // ���ģʽ����͸�����
            material{
                diffuse[_Color]  // ������
                ambient[_Ambient]  // ������
                specular[_Spectular] // ���淴��
                shininess[_Shininess]  //  �߹�
                emission[_Emission] // �Է���
                }
            lighting on // ���տ���
            separatespecular on // ���淴�俪��
            settexture[_MainTex]{ // ����
                combine texture * primary double
                }
            settexture[_SecondTex]{
                constantcolor[_Constant] // �Ĵ����洢������������
                combine texture * previous double, texture * constant 
                }
            }

    }
}
