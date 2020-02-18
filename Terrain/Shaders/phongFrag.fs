#version 330 core
out vec4 FragColor;

in vec3 gTeNormal;
//in vec3 gNormals ; --> Surface normal
in vec3 gWorldPos_FS_in ;

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

//uniform sampler2D texture1;
uniform DirLight dirLight;
uniform Material mat ;
uniform vec3 viewPos ;


void main()
{       
    // ambient
    vec3 ambient = dirLight.ambient * mat.ambient;
  	
    // diffuse 
    vec3 norm = normalize(gTeNormal); //normalize vertices' normals length
    vec3 lightDir = normalize(dirLight.direction - gWorldPos_FS_in); //calculate light pos

    float diff = max(dot(lightDir, norm), 0.0f); //calculate diffuse factor
    vec3 diffuse  = dirLight.diffuse  * (diff * mat.diffuse);
    
    // specular
    vec3 viewDir = normalize(viewPos - gWorldPos_FS_in);  //normalize camera direction
	vec3 halfway = normalize(lightDir + viewDir); //halfway vector between camera position and the light source

    float spec = pow(max(dot(norm, halfway), 0.0),  mat.shininess); //calculate specular factor
    vec3 specular = dirLight.specular * (spec * mat.specular);

    vec3 result = ambient + diffuse + specular;
    FragColor = vec4(result, 1.0); //final result --> light + texture

}

