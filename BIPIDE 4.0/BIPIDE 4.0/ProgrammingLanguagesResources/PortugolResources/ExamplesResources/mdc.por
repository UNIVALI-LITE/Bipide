programa
{
	/*
	 * Máximo Divisor Comum - MDC
	 */
	 
	funcao inicio()
	{
		inteiro x
		inteiro y
		inteiro a
		inteiro b
		inteiro c
		
		x = 10
		y = 4
		enquanto (x != y) 
		{
			se(x > y) 
			{
				x = x-y
				a = x
			}
			senao
			{
				y = y-x
				b = y
			}
		}
		c = x
	}
}