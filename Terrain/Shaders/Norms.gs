#version 330 core
layout(triangles) in ;
layout(triangle_strip, max_vertices = 3) out ;
vec3 getNormal() ;

in vec3 WorldPos_FS_in[] ;
in vec2 tessTex[];

out vec3 gNormals ;
out vec3 gWorldPos_FS_in ;
out vec2 TexCoords;

void main()
{
  
   for(int i = 0 ; i < 3; i++)
   {
      gl_Position = gl_in[i].gl_Position ;
      gWorldPos_FS_in = WorldPos_FS_in[i] ;
      gNormals = getNormal() ;
	  TexCoords = tessTex[i];
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