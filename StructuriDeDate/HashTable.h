#pragma once
# define _CRT_NO_SECURE_WARNINGS
#include <list>
#include <iterator>
#include <cstring>


class hashtable {
private: int dim,size;
	   std::list<std::pair<int, int>>* vect;
public:
	int get_dim()
	{
		return this->dim;
	}
	hashtable(int dim = 11)
	{
		this->size = 0;
		this->dim = dim;
		this->vect = new std::list<std::pair<int, int>>[dim];
	}
	int getsize()
	{
		return this->size;
	}
	int hashfunction(int key, int dim)
	{
		return key % dim;
	}
	int hashfunction_string(char a[256]);
	void insereaza(std::pair<int,int> pair);
	int cautare(int val);
	void sterge(int val);
	void rehashing();
	void afisare();
	~hashtable()
	{
		delete[] vect;
	}
};