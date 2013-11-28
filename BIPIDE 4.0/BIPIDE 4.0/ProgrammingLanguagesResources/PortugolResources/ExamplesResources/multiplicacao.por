programa
{
	/*
	 * Multiplica dois números
	 */
	 
	funcao inteiro multiplica(inteiro a, inteiro c) 
	{
		inteiro i, result 
		result = 0 
		para(i = 1; i <= c; i++)
		{
			result = result + a 
		}
		retorne result 
	}

	funcao inicio() 
	{ 
		inteiro j,k
		k = 3 
		j = 2 
		k = multiplica(k,j) 
		escreva(k) 
	}
}