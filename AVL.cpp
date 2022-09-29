#include <iostream>
#include <fstream>
#include <queue>

struct searchtree {
	struct nod {
		int key;
		nod* p, * st, * dr;
	};
	nod* rad = nullptr;
	searchtree(int key = 0)
	{
		if (rad)
		{
			rad->key = key;
			rad->p = rad->st = rad->dr = nullptr;
		}
	}
	void insert(nod*& rad, int key)
	{
		nod* z = new nod;
		z->key = key;
		z->st = z->dr = nullptr;
		nod* x = rad;
		nod* y = nullptr;
		while (x != nullptr)
		{
			y = x;
			if (z->key < x->key)
				x = x->st;
			else x = x->dr;
		}
		z->p = y;
		if (y == nullptr)
			rad = z;
		else
		{
			if (z->key < y->key)
				y->st = z;
			else y->dr = z;
		}
	}
	nod* maxim(nod* rad)
	{
		if (!rad->dr)
			return rad;
		while (rad->dr)
			if (rad->dr) return maxim(rad->dr);
	}
	nod* minim(nod* rad)
	{
		if (!rad->dr)
			return rad;
		while (rad->st)
			if (rad->st) return minim(rad->st);
	}
	nod* succesor(nod* rad)
	{
		nod* y;
		if (rad->dr) y = minim(rad->dr);
		else {
			y = rad->p;
			while (y != nullptr && rad == y->dr)
			{
				rad = y;
				y = y->p;
			}
		}
		return y;
	}
	nod* predecesor(nod* rad)
	{
		nod* y;
		if (rad->st) y = maxim(rad->st);
		else {
			y = rad->p;
			while (y != nullptr && rad == y->st)
			{
				rad = y;
				y = y->p;
			}
		}
		return y;
	}
	nod* find(nod* rad, int key)
	{
		if (key == rad->key)
			return rad;
		if (key < rad->key)
		{
			if (rad->st)return find(rad->st, key);
		}
		else if (rad->dr)return find(rad->dr, key);
		return nullptr;
	}
	void transplant(nod*& z, nod*& y)
	{
		if (z->p == nullptr)
			rad = y;
		else
		{
			if(z == z->p->st)
				z->p->st = y;
			else z->p->dr = y;
		}
		if(y != nullptr)
			y->p = z->p;
	}
	void sterge(nod*& z, int key)
	{
		if (z->key == key)
		{
			if (!z->st)
				transplant(z, z->dr);
			else
			{
				if (!z->dr)
					transplant(z, z->st);
				else
				{
					nod* y = succesor(z);
					if (y != z->dr)
					{
						transplant(y, y->dr);
						y->dr = z->dr;
						z->dr->p = y;
					}
					transplant(z, y);
					y->st = z->st;
					z->st->p = y;
				}
			}
			delete z;
		}
		else return;
	}
	void erase(nod*& z)
	{
		if (z)
		{
			if (!z->st)
				transplant(z, z->dr);
			else
			{
				if (!z->dr)
					transplant(z, z->st);
				else
				{
					nod* y = succesor(z);
					if (y != z->dr)
					{
						transplant(y, y->dr);
						y->dr = z->dr;
						z->dr->p = y;
					}
					transplant(z, y);
					y->st = z->st;
					z->st->p = y;
				}
			}
			delete z;
		}
		else return;
	}
	void afisare_niveluri(nod* rad)
	{
		if (rad)
		{
			std::queue<nod*> q;
			q.push(rad);
			while (q.empty() == false)
			{
				nod* nod = q.front();
				q.pop();
				std::cout << nod->key << " ";
				if (nod->st)q.push(nod->st);
				if (nod->dr)q.push(nod->dr);
			}
		}
	}
	void SDR(nod* rad)
	{
		if (rad->st != nullptr)SDR(rad->st);
		if (rad->dr != nullptr)SDR(rad->dr);
		std::cout << rad->key << " ";
	}
	void RSD(nod* rad)
	{
		std::cout << rad->key << " ";
		if (rad->st != nullptr)RSD(rad->st);
		if (rad->dr != nullptr)RSD(rad->dr);
	}
	void SRD(nod* rad)
	{
		if (rad->st != nullptr)SRD(rad->st);
		std::cout << rad->key << " ";
		if (rad->dr != nullptr)SRD(rad->dr);
	}
	void print_tree(int opt)
	{
		switch (opt)
		{
		case 4:
		{
			afisare_niveluri(rad);
			break;
		}
		case 3:
		{
			//postordine
			SDR(rad);
			break;
		}
		case 1:
		{
			//preordine
			RSD(rad);
			break;
		}
		case 2:
		{
			//inordine
			SRD(rad);
			break;
		}
		}
	}
	void construct(nod*& rad, int* vect, int n)
	{
		for (int i = 0; i < n; i++)
			insert(rad, vect[i]);
	}
	bool empty(nod* rad)
	{
		if (!rad)
			return false;
		return true;
	}
	void clear(nod*& rad)
	{
		if(rad->st)clear(rad->st);
		if(rad->dr)clear(rad->dr);
		erase(rad);
	}
};
int main()
{
	searchtree a;
	int n;
	std::ifstream f("Text.txt");
	f >> n;
	int* keys = new int[n];
	for (int i = 0; i < n; i++)
		f >> keys[i];
	a.construct(a.rad, keys, n);
	std::cout << "0 -> STOP\n1 -> insert\n2 -> find\n3 -> delete\n4 -> minim\n5 -> maxim\n6 -> succesor\n7 -> predecesor\n8 -> afisare\n";
	int optiune;
	while (std::cin >> optiune)
	{
		switch (optiune)
		{
		case 1:
		{
			int val;
			std::cout << "Dati valoare de inserat:\n";
			std::cin >> val;
			a.insert(a.rad, val);
			break;
		}
		case 2:
		{
			int val;
			std::cout << "Dati valoare de gasit:\n";
			std::cin >> val;
			if (a.find(a.rad, val))
				std::cout << "Gasit\n";
			else std::cout << "Nu a fost gasit";
			break;
		}
		case 3:
		{
			int val;
			std::cout << "Dati valoare de sters:\n";
			std::cin >> val;
			a.sterge(a.rad, val);
			break;
		}
		case 4:
		{
			searchtree::nod* x = a.minim(a.rad);
			std::cout << "minim: " << x->key << "\n";
			break;
		}
		case 5:
		{
			searchtree::nod* x = a.maxim(a.rad);
			std::cout << "maxim: " << x->key << "\n";
			break;
		}
		case 6:
		{
			searchtree::nod* x = a.succesor(a.rad);
			if (x) std::cout << "succesor: " << x->key << "\n";
			break;
		}
		case 7:
		{
			searchtree::nod* x = a.predecesor(a.rad);
			if(x) std::cout << "predecesor: " << x->key << "\n";
			break;
		}
		case 8:
		{
			std::cout << "1 -> preordine\n2 -> inordine\n3 -> postordine\n4 -> niveluri\n0 -> STOP\n";
			int optiune;
			while (std::cin >> optiune)
			{
				if (optiune > 0 && optiune <= 4)
				{
					a.print_tree(optiune);
					std::cout << "\n";
				}
				if(optiune==0) break;
			}
			break;
		}
		case 0:
		{
			delete[] keys;
			return 0;
		}
		}
	}
	delete[] keys;
	return 0;
}