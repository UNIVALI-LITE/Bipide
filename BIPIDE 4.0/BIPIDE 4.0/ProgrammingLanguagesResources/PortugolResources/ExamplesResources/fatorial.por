programa
{
	/*
	 * Fatorial
	 */
	funcao inicio()  
	{ 
		inteiro fat,temp, i, j, num
		fat 		= 1 
		temp	= 0 
		i 		= 0 
		j		= 0 
		num 	= 0
		
		leia(num)
		
		para(i = 2; i <= num; i++)
		{
			temp = fat 
			para(j = 1; j <= i-1; j++)
			{
				fat = fat + temp 
			} 
		} 
		escreva(fat)
	}
}