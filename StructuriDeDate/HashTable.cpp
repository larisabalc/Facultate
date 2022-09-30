#include "1.h"
#include <iostream>
#include <fstream>
#include <string>
#include <algorithm>


int hashtable::hashfunction_string(char a[256])
{
	int h = 0;
	for (int i = 0; i <= strlen(a); i++)
	{ 
		h ^= a[i];
	}
	return h;
}

void hashtable::insereaza(std::pair<int, int> pair)
{
		int index = hashfunction(pair.first, dim);
		if (std::find(vect[index].begin(), vect[index].end(), pair) != vect[index].end())
			vect[index].push_front(pair);
		else return;
		size++;
}

int hashtable::cautare(int val)
{
	for (int i = 0; i < dim; i++)
	{
		std::list<std::pair<int, int>>::iterator it = vect[i].begin();
		for (; it != vect[i].end(); it++)
			if ((*it).second == val)
				return (*it).first;
	}
}

void hashtable::sterge(int val)
{
	for (int i = 0; i < dim; i++)
	{
		if (vect[i].empty() == false)
		{
			std::list<std::pair<int, int>>::iterator it = vect[i].begin();
			for (; it != vect[i].end(); it++)
				if (it->second == val)
					break;
			if (it != vect[i].end())
			{
				vect[i].erase(it);
				size--;
			}
		}
		else i++;
	}
}

void hashtable::rehashing()
{
	int newdim = 2 * dim + 1;
	std::list<std::pair<int, int>>* nou;
	nou = new std::list<std::pair<int, int>>[newdim];
	for (int i = 0; i < dim; i++)
	{
		for (auto it = vect[i].begin(); it != vect[i].end(); it++)
		{
			int p = hashfunction(it->first, newdim);
			nou[p].push_front(*it);
		}
	}
	delete[] vect;
	vect = nou;
	dim = newdim;
}

void hashtable::afisare()
{
	for (int i = 0; i < dim; i++)
	{
		if (!vect[i].empty())
		{
			std::list<std::pair<int, int>>::iterator it;
			for (it = vect[i].begin(); it != vect[i].end(); it++)
				std::cout << it->second << " ";
		}
		else std::cout << " * ";
	}
}

int main()
{
	int n;
	std::cout << "Dati dimensiunea tabelei" << "\n";
	std::cin >> n;
	hashtable t(n);
	for(int i = 0; i < n; i++)
	{ 
		int x,y;
		std::cout << "Dati cheia si valoarea de inserare" << "\n";
		std::cin >> x >> y;
		t.insereaza(std::make_pair(x,y));
	}
	/*
	char v[256];
	strcpy_s(v , "albastru");
	std::cout << t.hashfunction_string(v);
	*/
	std::cout << "insereaza -> 1" << "\n" << "cauta -> 2" << "\n" << "sterge -> 3" << "\n" <<  "afisare -> 4" << "\n" << "EXIT -> 0" << "\n";
	int a;
	while (std::cin >> a)
	{
		switch (a)
		{
		case 1:
		{
			int key, val;
			std::cout << "Dati cheia si valoarea" << "\n";
			std::cin >> key >> val;
			t.insereaza(std::make_pair(key,val));
			int v = t.getsize();
			if (v / n >= 1)
			{
				t.rehashing();
			}
			break;
		}
		case 2:
		{
			int val;
			std::cout << "Dati valoarea de cautare" << "\n";
			std::cin >> val;
			std::cout << t.cautare(val) << "\n";
			break;
		}
		case 3:
		{
			int val;
			std::cout << "Dati valoarea de stergere" << "\n";
			std::cin >> val;
			t.sterge(val);
			break;
		}
		case 4:
		{
			t.afisare();
			break;
		}
		case 0:
		{
			return 0;
		}
		}
	}
	return 0;
}