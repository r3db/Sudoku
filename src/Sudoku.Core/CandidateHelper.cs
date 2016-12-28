using System;
using System.Drawing;

namespace Sudoku
{
    internal struct CandidateHelper
    {
        #region Internal Data

        internal int    Count       { get; set; }
        internal Point  Coordinates { get; set; }

        #endregion

        #region .Ctor

        internal CandidateHelper(int count, int index1, int index2)
            : this()
        {
            this.Count = count;
            this.Coordinates = new Point(index1, index2);
        }

        internal CandidateHelper(CandidateHelper h)
            : this(h.Count + 1, h.Coordinates.X, h.Coordinates.Y)
        {
        }

        internal CandidateHelper(int count, int index)
            : this(count, index, 0)
        {
        }

        #endregion
    }
}
