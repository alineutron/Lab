using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AT;
using NUnit.Framework;
using SharpTestsEx;

namespace Tests
{
	[TestFixture]
	public class PayoutRegistrationTest
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
	    public void ShouldRegisterLoanPayout()
	    {
			_target.RegisterCustomerPayout("123",LoanProduct.SmallLoan,new DateTime(2018,6,28,10,0,0,DateTimeKind.Utc));
		    var loanPayoutmodel =  _loanRepository.GetLoanPayoutModel("123");
		    loanPayoutmodel.Should().Not.Be.Null();
	    }

		[Test]
		public void ShouldRegisterLoanPayoutWithSmallLoan()
		{
			var payoutdate = new DateTime(2018, 6, 28, 10, 0, 0, DateTimeKind.Utc);
			_target.RegisterCustomerPayout("123", LoanProduct.SmallLoan, payoutdate);
			var loanPayoutmodel = _loanRepository.GetLoanPayoutModel("123");
			loanPayoutmodel.LoanProduct.Should().Be.EqualTo(LoanProduct.SmallLoan);
			loanPayoutmodel.AdministrtionFee.Should().Be.EqualTo(0);
			loanPayoutmodel.PayoutDate.Should().Be.EqualTo(payoutdate);
			loanPayoutmodel.PersonNumber.Should().Be.EqualTo("123");
		}

		[Test]
		public void ShouldRegisterLoanPayoutWithLargeLoan()
		{
			var payoutdate = new DateTime(2018, 6, 28, 10, 0, 0, DateTimeKind.Utc);
			_target.RegisterCustomerPayout("123", LoanProduct.LargeLoan, payoutdate);
			var loanPayoutmodel = _loanRepository.GetLoanPayoutModel("123");
			loanPayoutmodel.LoanProduct.Should().Be.EqualTo(LoanProduct.LargeLoan);
			loanPayoutmodel.AdministrtionFee.Should().Be.EqualTo(100);
			loanPayoutmodel.PayoutDate.Should().Be.EqualTo(payoutdate);
			loanPayoutmodel.PersonNumber.Should().Be.EqualTo("123");
		}

		[Test]
		public void ShouldRegisterLoanPayoutWithFastLoan()
		{
			var payoutdate = new DateTime(2018, 6, 28, 10, 0, 0, DateTimeKind.Utc);
			_target.RegisterCustomerPayout("123", LoanProduct.FastLoan, payoutdate);
			var loanPayoutmodel = _loanRepository.GetLoanPayoutModel("123");
			loanPayoutmodel.LoanProduct.Should().Be.EqualTo(LoanProduct.FastLoan);
			loanPayoutmodel.AdministrtionFee.Should().Be.EqualTo(500);
			loanPayoutmodel.PayoutDate.Should().Be.EqualTo(payoutdate);
			loanPayoutmodel.PersonNumber.Should().Be.EqualTo("123");
		}



	}
}
