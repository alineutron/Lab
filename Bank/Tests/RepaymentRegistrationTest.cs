using System;
using AT;
using NUnit.Framework;
using SharpTestsEx;

namespace Tests
{
	[TestFixture]
	public class RepaymentRegistrationTest
	{
		private LoanManager _target;
		private ILoanRepository _loanRepository;
		private FakeNow _now;

		[SetUp]
		public void Setup()
		{
			_loanRepository = new MemoryLoanRepository();
			_now = new FakeNow();
			_now.SetFakeNow(new DateTime(2018, 6, 1, 1, 0, 0, DateTimeKind.Utc));
			_target = new LoanManager(_loanRepository, _now);

		}

		[Test]
		public void ShouldRegisterLoanRepayment()
		{
			var payoutdate = new DateTime(2018, 6, 18, 10, 0, 0, DateTimeKind.Utc);
			var repaymentdate = new DateTime(2018, 6, 28, 10, 0, 0, DateTimeKind.Utc);
			_target.RegisterCustomerPayout("123", LoanProduct.SmallLoan, payoutdate);

			_target.RegisterCustomerRepayment("123", repaymentdate);
			var repaymentModel = _loanRepository.GetRepaymentDetails("123");
			repaymentModel.Should().Not.Be.Null();
		}

		[Test]
		public void ShouldRegisterLoanRepaymentWithDetail()
		{
			var payoutdate = new DateTime(2018, 6, 18, 10, 0, 0, DateTimeKind.Utc);
			var repaymentdate = new DateTime(2018, 6, 28, 10, 0, 0, DateTimeKind.Utc);
			_target.RegisterCustomerPayout("123", LoanProduct.SmallLoan, payoutdate);

			_target.RegisterCustomerRepayment("123", repaymentdate);
			var repaymentModel = _loanRepository.GetRepaymentDetails("123");
			repaymentModel.LoanProduct.Should().Be(LoanProduct.SmallLoan);
			repaymentModel.PersonNumber.Should().Be("123");
			repaymentModel.RepaymentDate.Should().Be(repaymentdate);
		}

		[Test]
		public void ShouldRegisterLoanRepaymentForSmallLoan()
		{
			var payoutdate = new DateTime(2018, 6, 18, 10, 0, 0, DateTimeKind.Utc);
			var repaymentdate = new DateTime(2018, 6, 28, 10, 0, 0, DateTimeKind.Utc);
			_target.RegisterCustomerPayout("123", LoanProduct.SmallLoan, payoutdate);

			_target.RegisterCustomerRepayment("123", repaymentdate);
			var repaymentModel = _loanRepository.GetRepaymentDetails("123");
			repaymentModel.LoanProduct.Should().Be(LoanProduct.SmallLoan);
			repaymentModel.PersonNumber.Should().Be("123");
			repaymentModel.RepaymentDate.Should().Be(repaymentdate);
			repaymentModel.RepaymentAmount.Should().Be(20);
		}

		[Test]
		public void ShouldRegisterLoanRepaymentForLargeLoan()
		{
			var payoutdate = new DateTime(2018, 6, 18, 10, 0, 0, DateTimeKind.Utc);
			var repaymentdate = new DateTime(2018, 6, 28, 10, 0, 0, DateTimeKind.Utc);
			_target.RegisterCustomerPayout("123", LoanProduct.LargeLoan, payoutdate);

			_target.RegisterCustomerRepayment("123", repaymentdate);
			var repaymentModel = _loanRepository.GetRepaymentDetails("123");
			repaymentModel.LoanProduct.Should().Be(LoanProduct.LargeLoan);
			repaymentModel.PersonNumber.Should().Be("123");
			repaymentModel.RepaymentDate.Should().Be(repaymentdate);
			repaymentModel.RepaymentAmount.Should().Be(120);
		}

		[Test]
		public void ShouldRegisterLoanRepaymentForFastLoan()
		{
			var payoutdate = new DateTime(2018, 6, 18, 10, 0, 0, DateTimeKind.Utc);
			var repaymentdate = new DateTime(2018, 6, 28, 10, 0, 0, DateTimeKind.Utc);
			_target.RegisterCustomerPayout("123", LoanProduct.FastLoan, payoutdate);

			_target.RegisterCustomerRepayment("123", repaymentdate);
			var repaymentModel = _loanRepository.GetRepaymentDetails("123");
			repaymentModel.LoanProduct.Should().Be(LoanProduct.FastLoan);
			repaymentModel.PersonNumber.Should().Be("123");
			repaymentModel.RepaymentDate.Should().Be(repaymentdate);
			repaymentModel.RepaymentAmount.Should().Be(520);
		}
	}
}