using System;

namespace AT
{
	public static class TotalAmountCalculator
	{
		private const double DailyRate = 2; //CONST for all
		public static double Calculate( DateTime payoutDate, DateTime paymentDate, double administrtionFee)
		{
			return administrtionFee + (paymentDate.Subtract(payoutDate).Days * DailyRate);
		}
	}
}