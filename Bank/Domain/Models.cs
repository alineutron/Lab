using System;

namespace AT
{
	
	public enum LoanProduct
	{
		SmallLoan, LargeLoan, FastLoan
	}

	public class LoanPayoutModel
	{
		public string PersonNumber { get; set; }
		public LoanProduct LoanProduct { get; set; }
		public DateTime PayoutDate { get; set; }
		public double AdministrtionFee { get; set; }
	}

	public class LoanRePaymentModel
	{
		public string PersonNumber { get; set; }
		public LoanProduct LoanProduct { get; set; }
		public DateTime RepaymentDate { get; set; }

		public double RepaymentAmount { get; set; }
	}
}