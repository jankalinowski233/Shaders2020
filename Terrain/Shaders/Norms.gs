#version 330 core
layout(triangles) in ;
layout(triangle_strip, max_vertices = 3) out ;

vec3 getNormal() ;

in vec3 fragPos[] ;
in vec3 teNormal[];
in vec2 tessTex[];
in float teScale[];
in float visibility[];

out vec3 gTeNormal;
//out vec3 gNormals ; //--> Surface normal
out vec3 gWorldPos_FS_in ;
out vec2 TexCoords;
out float gScale;
out float gVis;

void main()
{
 
   for(int i = 0 ; i < 3; i++)
   {
      gl_Position = gl_in[i].gl_Position ;
      gWorldPos_FS_in = fragPos[i] ;
      //gNormals = getNormal(); //--> surface normal
	  TexCoords = tessTex[i];
	  gTeNormal = teNormal[i];
	  gScale = teScale[i];
	  gVis = visibility[i];
      EmitVertex() ;
  }
     EndPrimitive() ;

}


vec3 getNormal()
{
    vec3 a = vec3(gl_in[1].gl_Position) - vec3(gl_in[0].gl_Position);
    vec3 b = vec3(gl_in[2].gl_Position) - vec3(gl_in[0].gl_Position);
    return normalize(cross(a, b));
}