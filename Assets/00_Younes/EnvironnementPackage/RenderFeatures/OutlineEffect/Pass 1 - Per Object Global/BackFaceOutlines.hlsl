#ifndef BACKFACEOUTLINES_INCLUDED
#define BACKFACEOUTLINES_INCLUDED

//Urp Core Library
#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

struct Attributes{
    float4 positionOS   : POSITION;
    float3 normalOS     : NORMAL;

#ifdef USE_PRECALCULATED_OUTLINE_NORMALS
    float3 smoothNormalOS  : TEXCOORD1;
#endif
};

struct VertexOuput{
    float4 positionCS   : SV_POSITION; //Position in clip space.
};

float _Thickness;
float4 _Color;
float _DepthOffset;

VertexOuput Vertex (Attributes input){
    VertexOuput output = (VertexOuput)0;

    float3 normalOS = input.normalOS;

#ifdef USE_PRECALCULATED_OUTLINE_NORMALS
    normalOS = input.smoothNormalOS;
#else
    normalOS = input.normalOS;
#endif

    float3 posOS = input.positionOS.xyz + normalOS * _Thickness;
    output.positionCS = GetVertexPositionInputs(posOS).positionCS;

    float depthOffset = _DepthOffset;

#ifdef UNITY_REVERSED_Z
    depthOffset = -depthOffset;
#endif

    output.positionCS.z += depthOffset;

    return output;
}

float4 Fragment(VertexOuput input) : SV_TARGET {
    return _Color;  
}
#endif