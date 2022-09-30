#include <iostream>
#include <fstream>
#include <queue>

struct avltree {
	struct nod {
		int key,h;
		nod* p, * st, * dr;
	}; 
	nod* rad = nullptr;
	avltree(int key=0)
	{
		if (rad)
		{
			rad->key = key;
			rad->st = rad->dr = rad->p = nullptr;
		}
	}
	void recalculare_inaltimi(nod* prev)
	{
		while (prev != nullptr)
		{
			int h_st = 0, h_dr = 0;
			if (prev->st != nullptr)
				h_st = prev->st->h;
			if (prev->dr != nullptr)
				h_dr = prev->dr->h;
			if (prev->h != (std::max(h_dr, h_st) + 1))
				prev->h = std::max(h_dr, h_st) + 1;
			prev = prev->p;
		}
	}
	void insert(nod*& rad, int key)
	{
		int h_st = 0, h_dr = 0;
		nod* z = new nod;
		z->key = key;
		z->st = z->dr = nullptr;
		nod* x = rad;
		nod* y = nullptr;
		while (x != nullptr)
		{
			y = x;
			if (z->key < x->key)
			{
				x = x->st;
			}
			else {
				x = x->dr;
			}
		}
		z->p = y;
		if (y == nullptr)
		{
			rad = z;
		}
		else
		{
			if (z->key < y->key)
			{
				y->st = z;
			}
			else {

				y->dr = z;
			}
			recalculare_inaltimi(z);
		}
		insert_repare(z);
	}
	int factor_balansare(nod* x)
	{
		if (!x->st && !x->dr)
			return 0;
		if (!x->dr)
			return x->st->h * (-1);
		if (!x->st)
			return  x->dr->h;
		return  x->dr->h - x->st->h;
	}
	void insert_repare(nod* x)
	{
		nod* par = x->p;
		while (par != nullptr && factor_balansare(par) != 0)
		{
			if (abs(factor_balansare(par)) == 1)
			{
				x = par;
				par = x->p;
			}
			else if (factor_balansare(par) == -2 && factor_balansare(x) == -1)
			{
				rot_dr(par);
				return;
			}
			else if (factor_balansare(par) == 2 && factor_balansare(x) == 1)
			{
				rot_st(par);
				return;
			}
			else if (factor_balansare(par) == -2 && factor_balansare(x) == 1)
			{
				rot_st(x);
				rot_dr(par);
				return;
			}
			else
			{
				rot_dr(x);
				rot_st(par);
				return;
			}
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
	nod* succesor(nod* x)
	{
		if (x->dr != nullptr)
		{
			nod* y = x->dr;
			while (y->st != nullptr)
				y = y->st;
			return y;
		}
		nod* y = x->p;
		while (y != nullptr && x == y->dr)
		{
			x = y;
			y = y->p;
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
	void transplant(nod* z, nod* y)
	{
		if (z->p == nullptr)
			rad = y;
		else
		{
			if (z == z->p->st)
				z->p->st = y;
			else z->p->dr = y;
		}
		if (y != nullptr)
			y->p = z->p;
	}
	void erase(nod* x)
	{
		if (x == nullptr)
			return;
		if (x->st == nullptr && x->dr == nullptr)
			transplant(x, nullptr);
		else if (x->st == nullptr)
			transplant(x, x->dr);
		else if (x->dr == nullptr)
			transplant(x, x->st);
		else
		{
			nod* s = succesor(x);
			if (s != x->dr)
			{
				transplant(s, s->dr);
				s->dr = x->dr;
				x->dr->p = s;
			}
			transplant(x, s);
			s->st = x->st;
			x->st->p = s;
		}
		if (x->st != nullptr && x->dr != nullptr)
		{
			recalculare_inaltimi(succesor(x)->p);
			delete_repair(succesor(x)->p);
		}
		else
		{
			recalculare_inaltimi(x->p);
			delete_repair(x->p);
		}
		delete x;
	}
	void delete_(int key)
	{
		nod* sterge = rad;
		while (sterge != nullptr)
		{
			if (sterge->key == key)
				break;
			else if (sterge->key < key)
				sterge = sterge->dr;
			else
				sterge = sterge->st;
		}
		if (sterge == nullptr)
			return;
		if (sterge->st == nullptr && sterge->dr == nullptr)
			transplant(sterge, nullptr);
		else if (sterge->st == nullptr)
			transplant(sterge, sterge->dr);
		else if (sterge->dr == nullptr)
			transplant(sterge, sterge->st);
		else
		{
			nod* s = succesor(sterge);
			if (s != sterge->dr)
			{
				transplant(s, s->dr);
				s->dr = sterge->dr;
				sterge->dr->p = s;
			}
			transplant(sterge, s);
			s->st = sterge->st;
			sterge->st->p = s;
		}
		if (sterge->st != nullptr && sterge->dr != nullptr)
		{
			recalculare_inaltimi(succesor(sterge)->p);
			delete_repair(succesor(sterge)->p);
		}
		else
		{
			recalculare_inaltimi(sterge->p);
			delete_repair(sterge->p);
		}
		delete sterge;
	}
	void delete_repair(nod* x)
	{
		while (x != nullptr && abs(factor_balansare(x)) != 1)
		{
			if (factor_balansare(x) == 0)
				x = x->p;
			else if (factor_balansare(x) == -2 && factor_balansare(x->st) == -1)
				rot_dr(x);
			else if (factor_balansare(x) == 2 && factor_balansare(x->dr) == 1)
				rot_st(x);
			else if (factor_balansare(x) == -2 && factor_balansare(x->st) == 1)
			{
				rot_st(x->st);
				rot_dr(x);
			}
			else if (factor_balansare(x) == 2 && factor_balansare(x->dr) == -1)
			{
				rot_dr(x->dr);
				rot_st(x);
				return;
			}
			else if (factor_balansare(x) == 2 && factor_balansare(x->dr) == 0)
			{
				rot_st(x);
				return;
			}
			else
			{
				rot_dr(x);
				return;
			}
		}
	}
	void rot_st(nod* x)
	{
		nod* y = x->dr;
		x->dr = y->st;
		if (y->st != nullptr)
			y->st->p = x;
		y->p = x->p;
		if (x->p == nullptr)
			rad = y;
		else if (x == x->p->st)
			x->p->st = y;
		else
			x->p->dr = y;
		y->st = x;
		x->p = y;
		recalculare_inaltimi(x);
	}
	void rot_dr(nod* x)
	{
		nod* y = x->st;
		x->st = y->dr;
		if (y->dr != nullptr)
			y->dr->p = x;
		y->p = x->p;
		if (x->p == nullptr)
			rad = y;
		else if (x == x->p->st)
			x->p->st = y;
		else
			x->p->dr = y;
		y->dr = x;
		x->p = y;
		recalculare_inaltimi(x);
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
				std::cout << nod->key << " " << factor_balansare(rad) << " ";
				if (nod->st)q.push(nod->st);
				if (nod->dr)q.push(nod->dr);
			}
		}
	}
	void SDR(nod* rad)
	{
		if (rad->st != nullptr)SDR(rad->st);
		if (rad->dr != nullptr)SDR(rad->dr);
		std::cout << rad->key << " " << factor_balansare(rad) << " ";
	}
	void RSD(nod* rad)
	{
		std::cout << rad->key << " " << factor_balansare(rad) << " ";
		if (rad->st != nullptr)RSD(rad->st);
		if (rad->dr != nullptr)RSD(rad->dr);
	}
	void SRD(nod* rad)
	{
		if (rad->st != nullptr)SRD(rad->st);
		std::cout << rad->key << " " << factor_balansare(rad) << " ";
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
		{
			 insert(rad,vect[i]);
		}
	}
	bool empty(nod* rad)
	{
		if (!rad)
			return false;
		return true;
	}
	void clear(nod*& rad)
	{
		if (rad->st)clear(rad->st);
		if (rad->dr)clear(rad->dr);
		erase(rad);
	}
};
int main()
{
	avltree a;
	int n;
	std::ifstream f("Text.txt");
	f >> n;
	int* keys = new int[n];
	for (int i = 0; i < n; i++)
		f >> keys[i];
	std::cout << "0 -> STOP\n1 -> insert\n2 -> construire\n3 -> find\n4 -> stergere\n5 -> minim\n6 -> maxim\n7 -> afisare\n";
	int optiune;
	while (std::cin >> optiune)
	{
		switch (optiune)
		{
		case 2:
		{
			a.construct(a.rad, keys, n);
			break;
		}
		case 1:
		{
			int val;
			std::cout << "Dati valoare de inserat:\n";
			std::cin >> val;
			a.insert(a.rad,val);
			break;
		}
		case 3:
		{
			int val;
			std::cout << "Dati valoare de gasit:\n";
			std::cin >> val;
			if (a.find(a.rad, val))
				std::cout << "Gasit\n";
			else std::cout << "Nu a fost gasit";
			break;
		}
		case 4:
		{
			int val;
			std::cout << "Dati valoare de sters:\n";
			std::cin >> val;
			a.delete_(val);
			break;
		}
		case 5:
		{
			avltree::nod* x = a.minim(a.rad);
			std::cout << "minim: " << x->key << "\n";
			break;
		}
		case 6:
		{
			avltree::nod* x = a.maxim(a.rad);
			std::cout << "maxim: " << x->key << "\n";
			break;
		}
		case 7:
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
				if (optiune == 0) break;
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