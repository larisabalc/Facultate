#pragma once

#include <iostream>

class cutiePraline
{
protected:
	std::string aroma, origine;
public:
	cutiePraline(std::string aroma, std::string origine)
	{
		this->aroma = aroma;
		this->origine = origine;
	}
	cutiePraline() :aroma(""), origine("")
	{}
	virtual float getVolume() = 0;
	virtual void toString() = 0;
};