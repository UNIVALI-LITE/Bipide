
/*
 * Vetor
 */
int main() 
{
	int vetor[5], i, j, aux ;
	
	vetor[0] = 5 ;
	vetor[1] = 3 ;
	vetor[2] = 4 ;
	vetor[3] = 2 ;
	vetor[4] = 1 ;
	
	for(i = 0; i <=4; i++)
	{
		for(j = i+1; j <= 4; j++)
		{
			if (vetor[i] > vetor[j]) 
			{ 
				aux = vetor[i] ;
				vetor[i] = vetor[j] ;
				vetor[j] = aux ;
			}
		}
	} 
	return 0;
}
