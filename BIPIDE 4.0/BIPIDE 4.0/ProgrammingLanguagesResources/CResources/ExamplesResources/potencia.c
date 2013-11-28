/*
 * Eleva um número ao quadrado
 */

	int multiplica(int a, int c) 
	{ 
		int i, result ;
		result = 0 ;
		for(i = 1; i<=c; i++)
		{
			result = result+a ;
		} 
		return result ;
	}

	 int quadrado(int n) 
	{ 
		return multiplica(n, n) ;
	} 


	int main()   
	{ 
		int k;
		printf(quadrado(5)) ;
	}
}

	
	return 0;
}