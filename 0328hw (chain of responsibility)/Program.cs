using _0328hw__chain_of_responsibility_;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0328hw__chain_of_responsibility_
{

    public class Receiver
    {
        bool BankTransfer;
        bool MoneyTransfer;
        bool PayPalTransfer;
        public Receiver(bool bt, bool mt, bool ppt)
        {
            BankTransfer = bt;
            MoneyTransfer = mt;
            PayPalTransfer = ppt;
        }
        public bool GetBankTransfer()
        {
            return BankTransfer;
        }
        public void SetBankTransfer(bool BankTransfer)
        {
            this.BankTransfer = BankTransfer;
        }
        public bool GetMoneyTransfer()
        {
            return MoneyTransfer;
        }
        public void SetMoneyTransfer(bool MoneyTransfer)
        {
            this.MoneyTransfer = MoneyTransfer;
        }
        public bool GetPayPalTransfer()
        {
            return PayPalTransfer;
        }
        public void SetPayPalTransfer(bool PayPalTransfer)
        {
            this.PayPalTransfer = PayPalTransfer;
        }
    };

    public abstract class PaymentHandler
    {
        protected PaymentHandler Successor;
        public PaymentHandler GetHandler()
        {
            return Successor;
        }
        public void SetHandler(PaymentHandler Successor)
        {
            this.Successor = Successor;
        }
        public abstract void Handle(Receiver receiver);
    };


    public class BankPaymentHandler : PaymentHandler
    {
        public override void Handle(Receiver receiver)
        {
            if (receiver.GetBankTransfer())
                Console.WriteLine("Bank transfer");
            else if (Successor != null)
                Successor.Handle(receiver);
        }
    };

    public class MoneyPaymentHandler : PaymentHandler
    {
        public override void Handle(Receiver receiver)
        {
            if (receiver.GetMoneyTransfer())
                Console.WriteLine("Transfer through money transfer systems");
            else if (Successor != null)
                Successor.Handle(receiver);
        }
    };

    public class PayPalPaymentHandler : PaymentHandler
    {
        public override void Handle(Receiver receiver)
        {
            if (receiver.GetPayPalTransfer())
                Console.WriteLine("Transfer via paypal");
            else if (Successor != null)
                Successor.Handle(receiver);
        }
    };
    internal class Program
    {
        public static void Request(PaymentHandler h, Receiver receiver)
        {
            h.Handle(receiver);
        }
        static void Main(string[] args)
        {
            PaymentHandler bankPaymentHandler = new BankPaymentHandler();
            PaymentHandler moneyPaymentHandler = new MoneyPaymentHandler();
            PaymentHandler paypalPaymentHandler = new PayPalPaymentHandler();

            bankPaymentHandler.SetHandler(paypalPaymentHandler);
            paypalPaymentHandler.SetHandler(moneyPaymentHandler);

            Receiver receiver = new Receiver(false, false, true);
            Request(bankPaymentHandler, receiver);

            receiver = new Receiver(false, true, false);
            Request(bankPaymentHandler, receiver);

            receiver = new Receiver(true, false, false);
            Request(bankPaymentHandler, receiver);
        }
    }
}

