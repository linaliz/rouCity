using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RCITYWEB.Models
{
    public class CodigoDescripcion<T> : IEqualityComparer<CodigoDescripcion<T>>
    {
        public CodigoDescripcion() { }
        public CodigoDescripcion(T key, string value)
        {
            this.Key = key;
            this.Value = value;
        }

        public T Key { get; set; }
        public string Value { get; set; }





        #region IEqualityComparer
        public bool Equals(CodigoDescripcion<T> x, CodigoDescripcion<T> y)
        {
            //Check whether the compared objects reference the same data.
            if (Object.ReferenceEquals(x, y)) return true;

            //Check whether any of the compared objects is null.
            if (Object.ReferenceEquals(x, null) || Object.ReferenceEquals(y, null))
                return false;

            //Check whether the products' properties are equal.
            return
                x.Key.Equals(y.Key)
                && x.Value == y.Value;
        }

        public int GetHashCode(CodigoDescripcion<T> obj)
        {
            int hashKey = obj.Key == null ? 0 : obj.Key.GetHashCode();
            int hashValue = obj.Value == null ? 0 : obj.Value.GetHashCode();

            return hashKey ^ hashValue;
        }

        public override bool Equals(object obj)
        {
            if (obj is CodigoDescripcion<T>)
                return this.Equals(this, (CodigoDescripcion<T>)obj);
            else
                return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return this.GetHashCode(this);
        }
        #endregion
    }

}