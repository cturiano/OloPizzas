using System;
using System.Collections.Generic;
using System.Linq;

namespace OloPizzas
{
    public class Toppings : IComparable, IEquatable<Toppings>
    {
        #region Constructors

        public Toppings(IReadOnlyCollection<string> toppings)
        {
            if (ReferenceEquals(toppings, null))
            {
                toppings = new string[]{ };
            }

            var list = new List<string>(toppings.Count);
            list.AddRange(toppings.Select(s => s.Trim().ToLower()));
            list.Sort();
            ToppingString = string.Join(", ", list);
        }

        #endregion

        #region Properties

        public string ToppingString { get; }

        #endregion

        #region Public Methods

        /// <inheritdoc />
        public int CompareTo(object obj)
        {
            if (!(obj is Toppings other))
            {
                return 1;
            }

            return ToppingString.CompareTo(other.ToppingString);
        }

        /// <inheritdoc />
        public bool Equals(Toppings other)
        {
            if (ReferenceEquals(other, null))
            {
                return false;
            }

            return ReferenceEquals(this, other) || ToppingString.Equals(other.ToppingString);
        }

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            if (!(obj is Toppings other))
            {
                return false;
            }

            return ReferenceEquals(this, obj) || Equals(other);
        }

        /// <inheritdoc />
        public override int GetHashCode() => ToppingString != null ? ToppingString.GetHashCode() : 0;

        public static bool operator ==(Toppings left, Toppings right) => Equals(left, right);

        public static bool operator !=(Toppings left, Toppings right) => !Equals(left, right);

        /// <inheritdoc />
        public override string ToString() => ToppingString;

        #endregion
    }
}