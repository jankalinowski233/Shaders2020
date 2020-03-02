#ifndef TERRAIN_H
#define TERRAIN_H


#include <vector>
#include <glad/glad.h>
#include <glm/glm.hpp>
#include <glm/gtc/matrix_transform.hpp>
#include <iostream>

class Terrain
{
public:
	Terrain(int widthIn, int heightIn, int stepSizeIn);
	Terrain();
	std::vector<float>& getVertices();
	inline int getStepSize() { return stepSize; }
	bool checkBounds(float x, float y, float distance);
	void makeVertices(std::vector<float> *vertices, float xStart, float yStart);
private:
	std::vector<float> vertices;
	int width;
	int height;
	int stepSize;
	void makeVertex(int x, int y, std::vector<float> *vertices);

};
#endif












