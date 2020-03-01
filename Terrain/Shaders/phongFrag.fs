#version 330 core
out vec4 FragColor;

in vec3 gTeNormal;
in vec3 gNormals ; //--> Surface normal
in vec3 gWorldPos_FS_in;
in float gScale;
in float gVis;

struct Material {
    vec3 ambient;
    vec3 diffuse;
    vec3 specular;    
    float shininess;
};                                                                        

struct DirLight {
    vec3 direction;
    vec3 ambient;
    vec3 diffuse;
    vec3 specular;
}; 

uniform DirLight dirLight;
uniform Material mat ;
uniform vec3 viewPos ;

uniform bool showFog = true;

void main()
{       
    vec3 white = vec3(1.0f, 1.0f, 1.0f);

    vec3 lightGray = vec3(0.82f, 0.82f, 0.82f);
	vec3 gray = vec3(0.5f, 0.4f, 0.5f);
    vec3 darkGray = vec3(0.26f, 0.26f, 0.26f);

    vec3 blue = vec3(0.1f, 0.1f, 0.8f);
    vec3 green = vec3(0.1f, 0.8f, 0.1f);
    vec3 darkGreen = vec3(0.1f, 0.6f, 0.1f);

	vec3 color = white;

    // ambient
    vec3 ambient = dirLight.ambient * mat.ambient;
  	
    // diffuse 
    //vec3 norm = normalize(gTeNormal); //normalize vertices' normals length (VERTEX NORMALS)
    vec3 norm = normalize(gNormals); //normalize vertices' normals length (SURFACE NORMALS)
    vec3 lightDir = normalize(dirLight.direction - gWorldPos_FS_in); //calculate light pos

    float diff = max(dot(lightDir, norm), 0.0f); //calculate diffuse factor
    vec3 diffuse  = dirLight.diffuse  * (diff * mat.diffuse);
    
    // specular
    vec3 viewDir = normalize(viewPos - gWorldPos_FS_in);  //normalize camera direction
	vec3 halfway = normalize(lightDir + viewDir); //halfway vector between camera position and the light source

    float spec = pow(max(dot(norm, halfway), 0.0),  mat.shininess); //calculate specular factor
    vec3 specular = dirLight.specular * (spec * mat.specular);

    float height = gWorldPos_FS_in.y / gScale;

	if(height < 0.3f)
	{
		color = vec3(mix(blue, darkGreen, smoothstep(0.01f, 0.3f, height)).rgb);
	}
	else if(height < 0.8f)
	{
		color = vec3(mix(darkGreen, gray, smoothstep(0.5f, 0.6f, height)).rgb);
	}	
	else
	{
		color = gray;
	}

    vec3 result = (ambient + diffuse + specular) * color;
	if(showFog == true)
	{
		FragColor = mix(vec4(0.5f, 0.5f, 0.5f, 1.0f), vec4(result, 1.0), gVis); //final result
	}   
	else
	{
		FragColor = vec4(result, 1.0f); // wireframe mode - used to clearly see LOD
	}

}

