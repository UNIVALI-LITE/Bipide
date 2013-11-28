/*
 * Multiplica dois números
 */
int multiplica(int a, int c)  
{ 
	int i, result;
	result = 0 ;
	
	for(i = 1; i <= c; i++)
	{
		result = result + a ;
	}
	return result ;
}

int main() 
{ 
	int j,k;
	k = 3 ;
	j = 2 ;
	k = multiplica(k,j) ;
	printf(k); 
	
	return 0;
}