#include "poligon.h"

poligon sortare(poligon* a, int n)
{
	for(int i = 0; i < n - 1; i++)
		for (int j = i + 1; j < n; j++)
		{
			if (a[j] < a[i])
			{
				poligon aux = a[j];
				a[i] = a[j];
				a[j] = aux;
			}
		}
	return a[n - 1];
}

int main()
{
	std::ifstream f("Text.txt");
	int n;
	f >> n;
	poligon* a = new poligon[n];
	for (int i = 0; i < n; i++)
	{
		int dim;
		f >> dim;
		poligon elem(dim);
		f >> elem;
		a[i] = elem;
	}
	std::cout << sortare(a, n)<<"\n";
	delete[] a;
	return 0;
}