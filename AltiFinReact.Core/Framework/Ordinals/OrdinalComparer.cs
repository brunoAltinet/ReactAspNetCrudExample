using System.Collections.Generic;
using Altinet.Ordinals;

namespace Altinet
{
    public class OrdinalComparer : IComparer<string>
    {

        #region Fields



        #endregion

        #region Constructors


        #endregion

        #region Methods

        public int Compare(string x, string y)
        {
            return OrdinalUtils.CompareOrdinals(x, y);
        }

        #endregion

    }
}
