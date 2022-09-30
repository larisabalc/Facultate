#include "Lindt.h"
#include "Baravelli.h"
#include "Raffaello.h"

int main()
{
	cutiePraline** c;
	int n;
	std::cin >> n;
	c = new cutiePraline* [n];
	for (int i = 0; i < n; i++)
	{
		int r = rand() % 3;
		switch (r)
		{
		case 0:
		{
			std::cout << "Dati datele despre cutia Lindt\n";
			std::string aroma, origine;
			std::cin >> aroma >> origine;
			float lenght, width, height;
		    std::cin >> lenght >> width >> height;
			c[i] = new lindt(aroma, origine, lenght, width, height);
			break;
		}
		case 1:
		{
			std::cout << "Dati datele despre cutia Baravelli\n";
			std::string aroma, origine;
			std::cin >> aroma >> origine;
			float radius, height;
			std::cin >> radius >> height;
			c[i] = new baravelli(aroma, origine, radius, height);
			break;
		}
		case 2:
		{
			std::cout << "Dati datele despre cutia Raffaello\n";
			std::string aroma, origine;
			std::cin >> aroma >> origine;
			float lenght;
			std::cin >> lenght;
			c[i] = new raffaello(aroma, origine, lenght);
			break;
		}
		}
	}
	srand(time(0));
	for (int i = 0; i < n; i++)
	{
		int r = rand() % 3;
	    lindt* l = dynamic_cast<lindt*>(c[i]);
	    if (l)
	    {
				std::cout << l->getVolume() << "\n";
	    }
		else 
		{
			baravelli* b = dynamic_cast<baravelli*>(c[i]);
			if (b)
			{
					b->toString();
			}
			else
				{
					raffaello* r = dynamic_cast<raffaello*>(c[i]);
			        if (r)
			        {
				      std::cout << r->getVolume() << "\n";
			        }
				}
		}
	}
	delete[] c;
	return 0;
}