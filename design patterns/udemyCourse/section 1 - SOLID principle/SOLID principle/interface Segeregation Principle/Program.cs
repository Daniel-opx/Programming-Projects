using System;

namespace DotNetDesignPatternDemos.SOLID.InterfaceSegregationPrinciple
    /*the premise of this principle is as following: your interface should be small enough so if someone wants to implement the interface they wont 
     have to implement methods that they dont need
    
     so in the first part we see the machine interface that has three methods in it and its fine as long someone need all these methods 
    but if someone needs to use only one of the methods and he uses the Imachcine interface he has to implement methods he doesnt need so wee need to create smaller interface*/
{
    public class Document
    {
    }

    public interface IMachine
    {
        void Print(Document d);
        void Fax(Document d);
        void Scan(Document d);
    }

    // ok if you need a multifunction machine
    public class MultiFunctionPrinter : IMachine
    {
        public void Print(Document d)
        {
            //
        }

        public void Fax(Document d)
        {
            //
        }

        public void Scan(Document d)
        {
            //
        }
    }

    public class OldFashionedPrinter : IMachine
    {
        public void Print(Document d)
        {
            // yep
        }

        public void Fax(Document d)
        {
            throw new System.NotImplementedException();
        }

        public void Scan(Document d)
        {
            throw new System.NotImplementedException();
        }
    }

    public interface IPrinter
    {
        void Print(Document d);
    }

    public interface IScanner
    {
        void Scan(Document d);
    }

    public class Printer : IPrinter
    {
        public void Print(Document d)
        {

        }
    }

    public class Photocopier : IPrinter, IScanner
    {
        public void Print(Document d)
        {
            throw new System.NotImplementedException();
        }

        public void Scan(Document d)
        {
            throw new System.NotImplementedException();
        }
    }

    public interface IMultiFunctionDevice : IPrinter, IScanner //
    {

    }

    public struct MultiFunctionMachine : IMultiFunctionDevice
    {
        // compose this out of several modules
        private IPrinter printer;
        private IScanner scanner;

        public MultiFunctionMachine(IPrinter printer, IScanner scanner)
        {
            if (printer == null)
            {
                throw new ArgumentNullException(paramName: nameof(printer));
            }
            if (scanner == null)
            {
                throw new ArgumentNullException(paramName: nameof(scanner));
            }
            this.printer = printer;
            this.scanner = scanner;
        }

        public void Print(Document d)
        {
            printer.Print(d);
        }

        public void Scan(Document d)
        {
            scanner.Scan(d);
        }
    }
}
