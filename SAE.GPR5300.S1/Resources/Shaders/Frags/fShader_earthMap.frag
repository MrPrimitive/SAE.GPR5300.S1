#version 330 core
in vec3 fNormal;
in vec3 fPos;
in vec2 fTexCoords;

struct Material {
    sampler2D diffuse;
    sampler2D normalMap;
};

struct Light {
    vec3 position;
    vec3 ambient;
    vec3 diffuse;
};

uniform Material material;
uniform Light light;
uniform bool useNormalMap;

out vec4 FragColor;

vec3 normal;

void main()
{
    if (useNormalMap){
        normal = texture(material.normalMap, fTexCoords).rgb;
        normal = normalize(normal * 2.0 - 1.0);
    } else {
        normal = normalize(fNormal);
    }

    vec3 ambient = light.ambient * texture(material.diffuse, fTexCoords).rgb;

    vec3 lightDirection = normalize(light.position - fPos);
    float diff = max(dot(normal, lightDirection), 0.0);
    vec3 diffuse = light.diffuse * (diff * texture(material.diffuse, fTexCoords).rgb);

    vec3 result = ambient + diffuse;
    FragColor = vec4(result, 1.0);
}