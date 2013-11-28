/*
 * Fatorial
 */
int main()  
{ 
	int fat,temp, i, j, num;
	fat 		= 1 ;
	temp	= 0 ;
	i 		= 0 ;
	j		= 0 ;
	num 	= 0;
	
	scanf(num);
	
	for(i = 2; i <= num; i++)
	{
		temp = fat ;
		for(j = 1; j <= i-1; j++)
		{
			fat = fat + temp ;
		} 
	} 
	printf(fat);
	return 0;
}
