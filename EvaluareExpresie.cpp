#include <iostream>
#include <fstream>
#include <stdlib.h>
#include <queue>
#include <stack>
#include <cstring>
#include <vector>

struct arbore {
	struct nod {
		char inf;
		nod* st, * dr;
	}; 
	nod* rad;
	arbore()
	{
		rad = nullptr;
	}
	double putere(double x, int n)
	{
		double p = 1;
		for (int i = 0; i < n; i++)
				p *= x;
		return p;
	}
	int prec(char a)
	{
		if (a == '(')
			return 0;
		if (a == '+' || a == '-')
			return 1;
		if (a == '/' || a == '*')
			return 2;
		if (a == '^')
			return 3;
	}
	void expresie(char a[256], int& ok, std::vector<char>& fp)
	{
		std::stack<char> op;
		int l = strlen(a);
		for (int i = 0; i < l;)
		{
			if (a[i] == ' ')
			{
				i++;
				continue;
			}
			else
			{
				if (strchr("abcdefghijklmnopqrstuvxwyz", a[i]))
				{
					fp.push_back(a[i]);
					i++;
				}
				else if (a[i] >= '0' && a[i] <= '9')
				{
					fp.push_back(a[i]);
					i++;
				}
				else {
					if (a[i] == '(')
					{
						op.push(a[i]);
						i++;
					}
					else
						if (a[i] == ')')
						{
							while (op.empty() == false && op.top() != '(')
							{
								fp.push_back(op.top());
								op.pop();
							}
							if (op.empty() == false)
								op.pop();
							else {
								ok = 0;
								return;
							}
							i++;
						}
						else {
							if (strchr("+-/*^", a[i]))
							{
								while (op.empty() == false && prec(a[i]) <= prec(op.top()))
								{
									while (strchr("+-/*^ ", a[i - 1]))
										if (strchr("+-/*^", a[i - 1]))
										{
											ok = 0;
											return;
										}
										else i++;
									fp.push_back(op.top());
									op.pop();
								}
								op.push(a[i]);
								i++;
							}
							else {
								ok = 0;
								return;
							}
						}
				}
			}
		}
		while (op.empty() == false)
		{
			if (op.top() == '(')
			{
				ok = 0;
				return;
			}
			fp.push_back(op.top());
			op.pop();
		}
	}
	void creare(nod*& r, std::vector<char> v, int& i)
	{
		char x;
		x = v[i--];
		if (strchr("+-/*^", x) == NULL)
		{
			r = new nod;
			r->inf = x;
			r->st = nullptr;
			r->dr = nullptr;
		}
		else
		{
			r = new nod;
			r->inf = x;
			creare(r->dr,v,i);
			creare(r->st,v,i);
		}
	}
	double valoare(char variabila)
	{
		double x;
		std::cout << "Dati valoare pentru variabila " << variabila << "\n";
		std::cin >> x; 
		return x;
	}
	double operatie(nod* rad, double st, double dr)
	{
		if (rad->inf == '+')
			return st + dr;
		if (rad->inf == '-')
			return st - dr; 
		if (rad->inf == '/')
			return st / dr;
		if (rad->inf == '*')
			return st * dr;
		if (rad->inf == '^')
			return putere(st,dr);
	}
	double evaluare(nod* rad) 
	{
		if (rad->st || rad->dr)
		{
			double rez1 = evaluare(rad->st);
			double rez2 = evaluare(rad->dr);
			return operatie(rad, rez1, rez2);
		}
		else {
			if (strchr("abcdefghijklmnopqrstuvwxyz", rad->inf))
				return valoare(rad->inf);
			else
			{
				int x = rad->inf - '0';
				return (double)x;
			}
		}
	}
	void afisare_niveluri(nod* rad)
	{
		std::queue<nod*> q;
		q.push(rad);
		while (q.empty() == false)
		{
			nod* nod = q.front();
			q.pop();
			std::cout << nod->inf << " ";
			if (nod->st)q.push(nod->st);
			if (nod->dr)q.push(nod->dr);
		}
	}
};


int main()
{
	std::ifstream f("Text.txt");
	char a[256];
	f.getline(a, 256);
	arbore t;
	int ok = 1;
	std::vector<char> v;
	t.expresie(a, ok, v);
	if (ok == 1)
	{
		int i = v.size() - 1;
		t.creare(t.rad, v, i);
		t.afisare_niveluri(t.rad);
		std::cout << "\nevaluare expresie -> 1 \nSTOP -> 0 \n";
		int a;
		while (std::cin >> a)
		{
			switch (a)
			{
			case 1:
			{
                std::cout << t.evaluare(t.rad)<< "\n";
				break;
			}
			case 0:
			{
				return 0;
			}
			}
		}
	}
	else std::cout << "Expresie incorecta";
	return 0;
}