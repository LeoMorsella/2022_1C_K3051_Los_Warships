#if OPENGL
	#define SV_POSITION POSITION
	#define VS_SHADERMODEL vs_3_0
	#define PS_SHADERMODEL ps_3_0
#else
    #define VS_SHADERMODEL vs_4_0_level_9_1
    #define PS_SHADERMODEL ps_4_0_level_9_1
#endif

float4x4 World;
float4x4 View;
float4x4 Projection;

/* float3 DiffuseColor; */

float alphaValue = 1;
/* float3 lightPosition = float3(1000, 1000, 1000); */
float Time = 0;

texture oceanTexture;
sampler2D TextureSampler = sampler_state
{
    Texture = (oceanTexture);
    ADDRESSU = MIRROR;
    ADDRESSV = MIRROR;
    MINFILTER = LINEAR;
    MAGFILTER = LINEAR;
    MIPFILTER = LINEAR;
};

struct VS_INPUT
{
    float4 Position : POSITION0;
    float2 Texcoord : TEXCOORD0;
    float3 Normal : NORMAL0;
};

struct VS_OUTPUT
{
    float4 Position : POSITION0;
    float2 Texcoord : TEXCOORD0;
    float3 WorldPos : TEXCOORD1;
    float3 WorldNormal : TEXCOORD2;
};

VS_OUTPUT vs_RenderOcean(VS_INPUT input)
{
    VS_OUTPUT output;

    float amplitud = 5;
    float frecuencia = 1;

    input.Position.y += sin(Time * frecuencia + input.Position.x * 5) * amplitud;
    input.Position.y += sin(Time * frecuencia + input.Position.z * -5) * amplitud;

    //Proyectar posicion
    float4 worldPosition = mul(input.Position, World);
    float4 viewPosition = mul(worldPosition, View);
    output.Position = mul(viewPosition, Projection);

    //Enviar Texcoord directamente
    output.Texcoord = input.Texcoord;

    //todo: que le pase el inv trasp. word
    float4x4 matInverseTransposeWorld = World;
    output.WorldPos = worldPosition.xyz;
    output.WorldNormal = mul(input.Normal, matInverseTransposeWorld).xyz;

    return output;
}

struct PS_INPUT
{
    float2 Texcoord : TEXCOORD0;
    float3 WorldPos : TEXCOORD1;
    float3 WorldNormal : TEXCOORD2;
};

//Pixel Shader
float4 ps_RenderOcean(PS_INPUT input) : COLOR0
{
    /* float2 coord = (input.Texcoord * 100) % 1; */
    /* return float4(coord, 0.0, 1.0); */
    float4 tex2 = tex2D(TextureSampler, input.Texcoord * 35);
    return tex2;
}

technique RenderOcean
{
    pass Pass_0
    {
        VertexShader = compile VS_SHADERMODEL vs_RenderOcean();
        PixelShader = compile PS_SHADERMODEL ps_RenderOcean();
    }
}
