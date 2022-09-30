#pragma once

#include "cutiePraline.h"

class lindt : public cutiePraline
{
private:
	float lenght, width, height;
public:
	lindt(std::string aroma, std::string origine, float lenght, float width, float height) :cutiePraline(aroma, origine)
	{
		this->lenght = lenght;
		this->width = width;
		this->height = height;
	}
	lindt():lenght(0), width(0), height(0)
	{}
	float getVolume()
	{
		return lenght * width * height;
	}
	void toString()
	{
		std::cout << "Cutia " << origine << " are aroma " << aroma << " si volumul " << getVolume();
	}
};