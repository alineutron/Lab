using Domain;

namespace AT
{
	public interface ILoanRepository
	{
		void RegisterPayout(LoanPayoutModel payoutModel);
		void RegisterRepayment(LoanRePaymentModel rePaymentModel);
		LoanPayoutModel GetLoanPayoutModel(string pNum);
		LoanRePaymentModel GetRepaymentDetails(string pNum);
		FileStorageModel AllData { get; }
	}
}