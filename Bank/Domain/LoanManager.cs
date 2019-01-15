using System;
using Domain;

namespace AT
{
	public class LoanManager
	{
		private readonly ILoanRepository _loanRepository;
		private readonly INow _now;

		public LoanManager(ILoanRepository loanRepository, INow now)
		{
			_loanRepository = loanRepository;
			_now = now;
		}

		public void RegisterCustomerPayout(string pNumber, LoanProduct loanProduct, DateTime payoutdate)
		{
			if (payoutdate < _now.UtcNow())
			{
				throw new RegistrationFailedException() { Reason = "Payout date should be in the future" };
			}

			
			var loanPayoutModel = new LoanPayoutModel()
			{
				LoanProduct = loanProduct,
				//AdministrtionFee = administrationFee,
				PayoutDate = payoutdate,
				PersonNumber = pNumber
			};

			_loanRepository.RegisterPayout(loanPayoutModel);
		}

		public double RegisterCustomerRepayment(string pNumber,  
			DateTime paymentDate
			) //same loan product as the one used in registration
		{
			var repayment = new LoanRePaymentModel()
			{
				//LoanProduct = loanProduct,
				PersonNumber = pNumber,
				//RepaymentAmount = repaymentAmount,
				RepaymentDate = paymentDate
			};

			

			var loanPayoutModel =  _loanRepository.GetLoanPayoutModel(repayment.PersonNumber);
			var amountToPay = TotalAmountCalculator.Calculate( loanPayoutModel.PayoutDate, paymentDate,
				loanPayoutModel.AdministrtionFee);
			if (paymentDate < loanPayoutModel.PayoutDate)
			{
				throw new RegistrationFailedException() {Reason = "Repayment date should be later than payout date"};
			}
			repayment.RepaymentAmount = amountToPay;
			repayment.LoanProduct = loanPayoutModel.LoanProduct;
			_loanRepository.RegisterRepayment(repayment);
			return amountToPay;
		}
		
	}

	public interface INow
	{
		DateTime UtcNow();
	}

	public class Now : INow
	{
		private readonly DateTime _now;

		public Now()
		{
			_now = DateTime.UtcNow;
		}
		public DateTime UtcNow()
		{
			return _now;
		}
	}
}