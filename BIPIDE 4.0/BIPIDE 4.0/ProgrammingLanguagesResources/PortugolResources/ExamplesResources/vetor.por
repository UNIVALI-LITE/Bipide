programa
{
	/*
	 * Vetor
	 */
	funcao inicio() 
	{
		inteiro vetor[5], i, j, aux 
		
		vetor[0] = 5 
		vetor[1] = 3 
		vetor[2] = 4 
		vetor[3] = 2 
		vetor[4] = 1 
		
		para(i = 0; i <=4; i++)
		{
			para(j = i+1; j <= 4; j++)
			{
				se (vetor[i] > vetor[j]) 
				{ 
					aux = vetor[i] 
					vetor[i] = vetor[j] 
					vetor[j] = aux 
				}
			}
		} 
	}
}