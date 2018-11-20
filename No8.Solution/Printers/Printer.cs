namespace No8.Solution
{
    using System;
    using System.IO;

    public abstract class Printer : IEquatable<Printer>
    {
        public string Name { get; set; }

        public string Model { get; set; }

        public Printer(string name, string model)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Model = model ?? throw new ArgumentNullException(nameof(model));
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            if (this.GetType() != obj.GetType())
                return false;

            if (ReferenceEquals(this, obj))
                return true;

            return this.Equals((Printer)obj);
        }

        public bool Equals(Printer other)
        {
            if (other == null)
                return false;

            if (this.GetHashCode() != other.GetHashCode())
                return false;

            return this.Name == other.Name && this.Model == other.Model;
        }

        internal abstract void Print(FileStream fs);

        public override int GetHashCode()
        {
            return this.Name.GetHashCode() + this.Model.GetHashCode();
        }
    }
}