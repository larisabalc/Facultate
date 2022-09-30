#pragma once

#include "cutiePraline.h"

class raffaello : public cutiePraline
{
private:
	float lenght;
public:
	raffaello(std::string aroma, std::string origine, float lenght) :cutiePraline(aroma, origine)
	{
		this->lenght = lenght;
	}
	raffaello() :lenght(0)
	{}
	float getVolume()
	{
		return lenght * lenght * lenght;
	}
	void toString()
	{
		std::cout << "Cutia " << origine << " are aroma " << aroma << " si volumul " << getVolume();
	}
};
