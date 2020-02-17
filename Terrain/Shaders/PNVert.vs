#version 450 core
layout (location = 0) in vec3 aPos;
layout (location = 1) in vec3 aNormals;
layout (location = 2) in vec2 texCoords;

uniform mat4 model;
uniform mat4 view;
uniform mat4 projection;
uniform vec3 eyePos ;

out vec3 eye;
out vec3 WorldPos_CS_in; 
out vec3 normals;
out vec2 vsTexCoords;

void main()
{
	vsTexCoords = texCoords;
    normals = normalize((model * vec4(aNormals, 0.0)).xyz) ;
	eye = eyePos ;
	WorldPos_CS_in = (model * vec4(aPos, 1.0)).xyz; 
}