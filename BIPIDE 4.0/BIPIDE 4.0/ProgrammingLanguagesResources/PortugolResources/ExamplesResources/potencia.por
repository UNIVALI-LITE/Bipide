programa
{
	/*
	 * Eleva um número ao quadrado
	 */
	 
	funcao inteiro multiplica(inteiro a, inteiro c) 
	{ 
		inteiro i, result 
		result = 0 
		para(i = 1; i<c; i++)
		{
			result<-result+a 
		} 
		retorne result 
	}


	funcao inteiro quadrado(inteiro n) 
	{ 
		retorne multiplica(n, n) 
	} 


	funcao inicio()   
	{ 
		inteiro k
		escreva(quadrado(5)) 
	}
}