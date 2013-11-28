/*
	 * Máximo Divisor Comum - MDC
	 */
int main()  
{ 
	
		int x, y, a, b, c;
		
		x = 10;
		y = 4;
		while (x != y) 
		{
			if(x > y) 
			{
				x = x-y;
				a = x;
			}
			else
			{
				y = y-x;
				b = y;
			}
		}
		c = x;
	
	return 0;
}