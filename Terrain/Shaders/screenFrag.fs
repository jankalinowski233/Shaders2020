#version 330 core
out vec4 FragColor ;

in vec2 texCoords ;
uniform sampler2D sceneTex;

void main()
{
    FragColor = texture2D(sceneTex, texCoords);
}
	