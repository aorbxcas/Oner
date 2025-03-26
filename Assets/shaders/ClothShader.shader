Shader "Hidden/ClothShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
    }
    SubShader
    {
        // No culling or depth
        Cull Off ZWrite Off ZTest Always

        Pass
        {
            CGPROGRAM
            
float hash1( ivec2 p ) { float n = dot(vec2(p),vec2(127.1,311.7)); return fract(sin(n)*43758.5453); }

vec4 getParticle( ivec2 id )
{
    return texelFetch( iChannel0, id, 0 );
}

vec4 react( in vec4 p, in ivec2 qid, float rl )
{
    vec4 q = getParticle( qid );
    
    vec2 di = q.xy - p.xy;
    
    float l = length(di);
    
    p.xy += 0.1*(l-rl)*(di/l);
    
    return p;
}

vec4 solveContrainsts( in ivec2 id, in vec4 p )
{
    if( id.x > 0 )  p = react( p, id + ivec2(-1, 0), 0.1 );
    if( id.x < 9 )  p = react( p, id + ivec2( 1, 0), 0.1 );
    if( id.y > 0 )  p = react( p, id + ivec2( 0,-1), 0.1 );
    if( id.y < 9 )  p = react( p, id + ivec2( 0, 1), 0.1 );

    if( id.x > 0 && id.y > 0)  p = react( p, id + ivec2(-1, -1), 0.14142 );
    if( id.x > 0 && id.y < 9)  p = react( p, id + ivec2(-1,  1), 0.14142 );
    if( id.x < 9 && id.y > 0)  p = react( p, id + ivec2( 1, -1), 0.14142 );
    if( id.x < 9 && id.y < 9)  p = react( p, id + ivec2( 1,  1), 0.14142 );

    return p;
}    

vec4 move( in vec4 p, in ivec2 id )
{
    const float g = 0.6;

    // acceleration
    p.xy += iTimeDelta*iTimeDelta*vec2(0.0,-g);
    
    // collide screen
    if( p.x< 0.00 ) p.x = 0.00;
    if( p.x> 1.77 ) p.x = 1.77;
    if( p.y< 0.00 ) p.y = 0.00;        
    if( p.y> 1.00 ) p.y = 1.00;

    // constrains
    p = solveContrainsts( id, p );
        
    #if 1
    if( id.y > 8 ) p.xy = 0.05 + 0.1*vec2(id);
    #endif
    
    // innertia
    vec2 np = 2.0*p.xy - p.zw;
    p.zw = p.xy;
    p.xy = np;

    return p;
}

void mainImage( out vec4 fragColor, in vec2 fragCoord )
{
    ivec2 id = ivec2(fragCoord-0.4 );
    
    if( id.x>9 || id.y>9 ) discard;
    
    vec4 p = getParticle(id);
    
    if( iFrame==0 )
    {
        p.xy = 0.05 + vec2(id)*0.1;
        p.zw = p.xy - 0.01*vec2(0.5+0.5*hash1(id),0.0);
    }
    else
    {
    	p = move( p, id );
    }

    fragColor = p;
}
            ENDCG
        }
    }
}
