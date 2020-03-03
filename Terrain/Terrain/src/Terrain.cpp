#include "Terrain.h"


//Terrain constructors
Terrain::Terrain(int widthIn, int heightIn, int stepSizeIn)
{
	width = widthIn;
	height = heightIn;
	stepSize = stepSizeIn;
	makeVertices(&vertices, 0.0f, 0.0f);
}

Terrain::Terrain() {
	width = 50;
	height = 50;
	stepSize = 10;
	makeVertices(&vertices, 0.0f, 0.0f);

}


std::vector<float>& Terrain::getVertices() {
	return vertices;
}

void Terrain::makeVertices(std::vector<float> *vertices, float xStart, float yStart) {
	/* triangle a b c
		   b
		   | \
		   a _ c


		 triangle d f e
		   f _ e
			 \ |
			   d

		 c == d
		 b == f
		 Duplicate vertices but easier in long run! (tesselation and LOD)

		a = (x,y,z)
		b = (x, y+1)
		c = (x+1,y)

		d = (x+1,y)
		e = (x, y+1)
		f = (x+1,y+1)

		 each vertex a, b,c, etc. will have 5 data:
		 x y z u v
		  */

	vertices->clear();

	for (int y = 0; y < height - 1; y++) {
		float  offSetY = yStart + (y * stepSize);
		for (int x = 0; x < width - 1; x++) {
			float offSetX = xStart + (x * stepSize);
			makeVertex(offSetX, offSetY, vertices);  // a
			makeVertex(offSetX, offSetY + stepSize, vertices);  // b
			makeVertex(offSetX + stepSize, offSetY, vertices);   // c
			makeVertex(offSetX + stepSize, offSetY, vertices);  //d
			makeVertex(offSetX, offSetY + stepSize, vertices);  //e
			makeVertex(offSetX + stepSize, offSetY + stepSize, vertices);  //f
		}
	}
}

void Terrain::makeVertex(int x, int y, std::vector<float> *vertices) {

	//x y z position
	vertices->push_back((float)x); //xPos
	vertices->push_back((float)0.0f);
	vertices->push_back((float)y); //zPos

	// add texture coords
	vertices->push_back((float)x / (width*stepSize));
	vertices->push_back((float)y / (height*stepSize));
}

bool Terrain::checkBounds(float x, float y, float distance)
{
	float xPos = vertices.at(vertices.size() / 2); // get X position of middle vertex
	float yPos = vertices.at((vertices.size() / 2) + 2); // get Z position of middle vertex

	if (x > (xPos + distance) || (x < xPos - distance))
		return true;

	if (y > (yPos + distance) || y < (yPos - distance))
		return true;

	return false;
}

