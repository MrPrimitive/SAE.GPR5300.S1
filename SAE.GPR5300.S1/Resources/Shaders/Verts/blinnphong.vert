#version 330 core
layout (location = 0) in vec3 vPos;
layout (location = 1) in vec3 vNormal;
layout (location = 2) in vec2 vTexCoords;

// declare an interface block; see 'Advanced GLSL' for what these are.
out VS_OUT {
    vec3 FragPos;
    vec3 Normal;
    vec2 TexCoords;
} vs_out;

uniform mat4 uModel;
uniform mat4 uProjection;
uniform mat4 uView;

void main()
{
    vs_out.FragPos = vPos;
    vs_out.Normal = vNormal;
    vs_out.TexCoords = vTexCoords;
    gl_Position = uProjection * uView * uModel * vec4(vPos, 1.0);
}