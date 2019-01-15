using System.Collections.Generic;
using System.Linq;
using Domain;

namespace AT
{
	public class MemoryLoanRepository : ILoanRepository
	{
		private readonly List<LoanPayoutModel> _loanPayouts = new List<LoanPayoutModel>();
		private readonly List<LoanRePaymentModel> _loanRePayments = new List<LoanRePaymentModel>();
		private readonly Dictionary<LoanProduct,double> _adminAmount = new Dictionary<LoanProduct, double>();
		//private readonly List<string> _customers = new List<string>();

		public MemoryLoanRepository()
		{
			_adminAmount.Add(LoanProduct.SmallLoan,0);
			_adminAmount.Add(LoanProduct.LargeLoan,100);
			_adminAmount.Add(LoanProduct.FastLoan,500);
		}

		public void RegisterPayout(LoanPayoutModel payoutModel)
		{
			var adminFee = _adminAmount[payoutModel.LoanProduct];
			payoutModel.AdministrtionFee = adminFee;
			_loanPayouts.Add(payoutModel);
		}

		public void RegisterRepayment(LoanRePaymentModel rePaymentModel)
		{
			_loanRePayments.Add(rePaymentModel);
		}

		public LoanPayoutModel GetLoanPayoutModel(string pNum)
		{
			return _loanPayouts.FirstOrDefault(x => x.PersonNumber == pNum);
		}

		public LoanRePaymentModel GetRepaymentDetails(string pNum)
		{
			return _loanRePayments.FirstOrDefault(x => x.PersonNumber == pNum);
		}

		public FileStorageModel AllData => new FileStorageModel()
		{
			LoanrePayments = _loanRePayments,
			LoanPayouts = _loanPayouts,
			AdminAmount = _adminAmount
		};
	}
}