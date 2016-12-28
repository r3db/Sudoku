using System;
using System.Collections.Generic;
using System.Text;

namespace Sudoku
{
    [Serializable]
    public class SudokuPiece
    {
        #region Internal Data

        public int          Value           { get; set; }
        public IList<int>   PossibleValues  { get; set; }

        #endregion

        #region .Ctor

        public SudokuPiece(int value, IList<int> possibleValues)
        {
            this.Value = value;
            this.PossibleValues = possibleValues;
        }

        public SudokuPiece(int value) 
            : this(value, null)
        { }

        #endregion

        #region Base Override Methods

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder(Value.ToString());

            if(PossibleValues != null && PossibleValues.Count > 0)
            {
                sb.Append(" : ");
                for (int i = 0; i < PossibleValues.Count; ++i)
                {
                    sb.Append(PossibleValues[i] + " ");
                }
                sb.Remove(sb.Length - 1, 1);
            }

            return sb.ToString();
        }

        #endregion
    }
}
