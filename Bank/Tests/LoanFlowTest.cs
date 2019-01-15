using System;
using System.Collections.Generic;
using AT;
using Autofac;
using Domain;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using SharpTestsEx;
using IContainer = System.ComponentModel.IContainer;

namespace Tests
{
	[TestFixture]
	public class LoanFlowTest
	{
		private LoanManager _target;
		private ILoanRepository _loanRepository;
		private FakeNow _now;

		[SetUp]
		public void Setup()
		{
			_loanRepository = new MemoryLoanRepository();
			_now = new FakeNow();
			_target = new LoanManager(_loanRepository, _now);
			

		}
		[Test]
		public void ShouldNotRegisterPayoutInPast()
		{
			_now.SetFakeNow(new DateTime(2018, 6, 28, 10, 0, 0, DateTimeKind.Utc));
			var payoutdate = new DateTime(2018, 6, 27, 10, 0, 0, DateTimeKind.Utc);

			Assert.Throws<RegistrationFailedException>(()=>_target.RegisterCustomerPayout("123", LoanProduct.SmallLoan,payoutdate ));
		}

		[Test]
		public void ShouldNotRegisterRepaymentIfRepaymentDateLessThanPayout()
		{
			_now.SetFakeNow(new DateTime(2018, 6, 28, 1, 0, 0, DateTimeKind.Utc));

			var payoutdate = new DateTime(2018, 6, 28, 10, 0, 0, DateTimeKind.Utc);
			_target.RegisterCustomerPayout("123", LoanProduct.SmallLoan, payoutdate);

			var repaymentDate = new DateTime(2018, 6, 28, 8, 0, 0, DateTimeKind.Utc);
			Assert.Throws<RegistrationFailedException>(() => _target.RegisterCustomerRepayment("123", repaymentDate));
		}
	}

	[DomainTest]
	public class LoanFlowTestWithIoc
	{
		public LoanManager Target;
		public ILoanRepository LoanRepository;

		[SetUp]
		public void Setup()
		{

		}

		[Test]
		public void ShouldNotRegisterPayoutInPast()
		{
			
		}

		[Test]
		public void ShouldNotRegisterRepaymentIfRepaymentDateLessThanPayout()
		{
			
		}
	}

	public class DomainTestAttribute : Attribute , ITestAction
	{
		private Autofac.IContainer _container;
		private TestDoubles _testDoubles;

		public void BeforeTest(ITest test)
		{
			buildContainer();
			//Startup(_container);
		}

		public void AfterTest(ITest test)
		{
			
		}

		private void buildContainer()
		{
			//var builder = new ContainerBuilder();
			_testDoubles = new TestDoubles();
			////var s = new SystemImpl(builder, _testDoubles);
			////setupBuilder(s, s);
			//_container = builder.Build();

			var builder = new ContainerBuilder();

			//Console.WriteLine("Do you want to use file storage y|Y");
			//var input = Console.ReadLine();
			//if (input.Equals("y") || input.Equals("Y"))
			//{
			//	builder.RegisterType<FileLoanRepository>().As<ILoanRepository>().SingleInstance();
			//}

			//else
			//builder.RegisterType<MemoryLoanRepository>().As<ILoanRepository>().SingleInstance();
			//builder.RegisterType<LoanManager>();
			//builder.RegisterType<UserInput>();
			//builder.RegisterType<Now>().As<INow>();
			_container = builder.Build();

			//for one timesetup to initialize target
			//QueryAllExtensions<IIsolateSystem>()
			//	.ForEach(x => x.Isolate(isolate));
			

		}

		public ActionTargets Targets => ActionTargets.Test;
	}


	public class TestDoubles : IDisposable
	{
		public class Registration
		{
			public Action<ContainerBuilder> Action;
			public object Instance;
		}

		private readonly List<Registration> _registrations = new List<Registration>();

		public Registration Register(Action<ContainerBuilder> action, object instance)
		{
			var registration = new Registration
			{
				Action = action,
				Instance = instance
			};
			_registrations.Add(registration);
			return registration;
		}

		public void RegisterFromPreviousContainer(ContainerBuilder builder)
		{
			_registrations.ForEach(r =>
			{
				if (r.Action != null)
					r.Action.Invoke(builder);
			});
		}

		public void Dispose()
		{
			_registrations.ForEach(r =>
			{
				var disposable = r.Instance as IDisposable;
				if (disposable != null)
					disposable.Dispose();
			});
		}
	}
	//public class IOcTestService
	//{
	//	private ILifetimeScope _container;

	//}
}