#version 450 core
layout (location = 0) in vec3 aPos;
layout (location = 1) in vec2 aTexCoords;

uniform mat4 model;
uniform mat4 lightSpaceMatrix;

out vec3 fragPos; 
out vec2 texCoords;
out vec4 lightPosVS;

void main()
{
		texCoords = aTexCoords; 
		fragPos = vec3(model * vec4(aPos, 1.0)); 
		lightPosVS = lightSpaceMatrix * vec4(fragPos, 1.0f);
}

