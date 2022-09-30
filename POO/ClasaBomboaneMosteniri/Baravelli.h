#pragma once

#include "cutiePraline.h"
#define pi 3.14

class baravelli : public cutiePraline
{
private:
	float radius, height;
public:
	baravelli(std::string aroma, std::string origine, float radius, float height) :cutiePraline(aroma, origine)
	{
		this->radius = radius;
		this->height = height;
	}
	baravelli() :radius(0), height(0)
	{}
	float getVolume()
	{
		return radius * radius * pi * height;
	}
	void toString()
	{
		std::cout << "Cutia " << origine << " are aroma " << aroma << " si volumul " << getVolume();
	}
};