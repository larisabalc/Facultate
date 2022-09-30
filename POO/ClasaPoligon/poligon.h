#pragma once

#include <cmath>
#include <iostream>
#include <fstream>

class poligon {
private:  
	struct pair
    {
	   int x, y;
    };
    pair* vector;
    int dim;
public:
	poligon(){}
	poligon(int n)
	{
		dim = n;
		vector = new pair[n];
	}
	double operator!()
	{
		double perimetru = 0;
		for (int i = 0; i < dim - 1; i++)
		{
			double l = sqrt((vector[i + 1].y - vector[i].y) * (vector[i + 1].y - vector[i].y) + (vector[i + 1].x - vector[i].x) * (vector[i + 1].x - vector[i].x));
			perimetru += l;
		}
		double l = sqrt((vector[dim-1].y - vector[0].y) * (vector[dim-1].y - vector[0].y) + (vector[dim-1].x - vector[0].x) * (vector[dim-1].x - vector[0].x));
		perimetru += l;
		return perimetru;
	}
	bool operator<(poligon b)
	{
		if (!this < !b)
			return true;
		return false;
	}
	void operator=(poligon b)
	{
		dim = b.dim;
		vector = new pair[dim];
		for (int i = 0; i < b.dim; i++)
		{
			vector[i].x = b.vector[i].x;
			vector[i].y = b.vector[i].y;
		}
	}
	friend std::ostream& operator<<(std::ostream& f, poligon b)
	{
		for (int i = 0; i < b.dim; i++)
			std::cout << "(" << b.vector[i].x << ", " << b.vector[i].y << ") ";
		return f;
	}
	friend std::ifstream& operator>>(std::ifstream& f, poligon& b)
	{
		for (int i = 0; i < b.dim; i++)
			f >> b.vector[i].x >> b.vector[i].y;
		return f;
	}
	~poligon()
	{
		delete[] vector;
	}
};

