#version 330 core
in vec3 fNormal;
in vec3 fPos;
in vec2 fTexCoords;

struct Material {
    sampler2D diffuse;
    sampler2D diffuseNightLight;
};

struct Light {
    vec3 position;
    vec3 ambient;
    vec3 diffuse;
    vec3 diffuseNight;
};

uniform Material material;
uniform Light light;
uniform vec3 viewPos;

out vec4 FragColor;

void main()
{
    vec3 normal = normalize(fNormal);

    vec3 ambient = light.ambient * texture(material.diffuse, fTexCoords).rgb;

    vec3 lightDirection = normalize(light.position - fPos);
    float diff = max(dot(normal, lightDirection), 0.0);
    vec3 diffuse = light.diffuse * (diff * texture(material.diffuse, fTexCoords).rgb);

    diff = max(dot(normal, -lightDirection), 0.0);
    vec3 diffuseNight = light.diffuseNight * (diff * texture(material.diffuseNightLight, fTexCoords).rgb);

    vec3 result = ambient + (diffuse + diffuseNight);
    FragColor = vec4(result, 1.0);
}